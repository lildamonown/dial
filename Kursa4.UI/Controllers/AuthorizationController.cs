using AutoMapper;
using Kursa4.BLL.Services.Interfaces;
using Kursa4.DAL.Entities;
using Kursa4.UI.Models.Inputs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Kursa4.BLL.Models;
using Kursa4.UI.Models;
using Microsoft.EntityFrameworkCore;
using Kursa4.UI.Models.Outputs;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Kursa4.UI.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        private readonly IMapper _mapper;

        public AuthorizationController(IAccountService accountService, SignInManager<User> signInManager, UserManager<User> userManager, IMapper mapper)
        {
            _accountService = accountService;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLogin model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _accountService.LoginAsync(model.Email, model.Password);

            if (result.Status == BLL.Models.StatusCode.Ok)
                return RedirectToAction("Index", "Home");

            ModelState.AddModelError(string.Empty, "Неверные данные для входа");

            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegister model)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<User>(model);
                var result = await _accountService.RegisterAsync(user, model.Password);

                if (result.Value == true)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError(string.Empty, result.Description);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            var result = await _accountService.LogoutAsync();

            if (result.Status != BLL.Models.StatusCode.Ok)
                return View("Index", "Home");

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> GetUserById(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return Json(new { success = false, message = "User ID is required" });

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return Json(new { success = false, message = "User not found" });
            

            var userInfo = new
            {
                user.Name,
                user.Surname
            };

            return Json(new { success = true, user = userInfo });
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Owner")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            users.Remove(users.Where(u => u.Id == currentUserId).First());

            var usersForDisplay = _mapper.Map<List<UserForDisplay>>(users);

            foreach (var user in usersForDisplay)
            {
                var identityUser = users.FirstOrDefault(u => u.Id == user.Id);
                if (identityUser != null)
                {
                    var roles = await _userManager.GetRolesAsync(identityUser);
                    user.Role = string.Join(", ", roles);
                }
            }

            return View("ShowUsers", usersForDisplay);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRole(string userId, string newRole)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return Json(new { success = false, message = "Пользователь не найден" });
                }

                var userRoles = await _userManager.GetRolesAsync(user);

                var removeResult = await _userManager.RemoveFromRolesAsync(user, userRoles);
                if (!removeResult.Succeeded)
                {
                    return Json(new { success = false, message = "Ошибка при удалении текущих ролей" });
                }

                var addResult = await _userManager.AddToRoleAsync(user, newRole);
                if (!addResult.Succeeded)
                {
                    return Json(new { success = false, message = "Ошибка при добавлении новой роли" });
                }

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
