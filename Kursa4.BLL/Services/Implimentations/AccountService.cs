using AutoMapper;
using Kursa4.BLL.DTO;
using Kursa4.BLL.Models;
using Kursa4.BLL.Services.Interfaces;
using Kursa4.DAL.Entities;
using Kursa4.DAL.Exceptions;
using Kursa4.DAL.Repositories.Implementation.EF;
using Kursa4.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text;


namespace Kursa4.BLL.Services.Implimentations
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task<Response<User>> GetById(string id)
        {
            var response = new Response<User>();

            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    response.Value = null;
                    response.Description = "Неверный id пользователя";
                    response.Status = StatusCode.BadRequest;

                    return response;
                }

                var result = await _userManager.FindByIdAsync(id);

                response.Value = result;
                response.Description = "Пользователь успешно получен";
                response.Status = StatusCode.Ok;

                return response;
            }
            catch (Exception ex)
            {
                response.Value = null;
                response.Description = ex.Message;
                response.Status = StatusCode.IternalServerError;

                return response;
            }
        }

        public async Task<Response<bool>> LoginAsync(string email, string password)
        {
            var response = new Response<bool>();

            try
            {
                var result = await _signInManager.PasswordSignInAsync(email, password, false, false);

                if (!result.Succeeded)
                {
                    response.Value = false;
                    response.Description = "Неправильный адрес электронной почты или пароль";
                    response.Status = StatusCode.NotFound;
                    return response;
                }

                response.Value = true;
                response.Description = "Пользователь успешно авторизовался";
                response.Status = StatusCode.Ok;

                return response;
            }
            catch (Exception ex)
            {
                response.Value = false;
                response.Description = ex.Message;
                response.Status = StatusCode.IternalServerError;

                return response;
            }
        }

        public async Task<Response<bool>> LogoutAsync()
        {
            var response = new Response<bool>();

            try
            {
                await _signInManager.SignOutAsync();

                response.Value = true;
                response.Description = "Пользователь успешно вышел из системы";
                response.Status = StatusCode.Ok;

                return response;
            }
            catch (Exception ex)
            {
                response.Value = false;
                response.Description = ex.Message;
                response.Status = StatusCode.IternalServerError;

                return response;
            }
        }

        public async Task<Response<bool>> RegisterAsync(User user, string password)
        {
            var response = new Response<bool>();

            try
            {
                if (user is null)
                {
                    response.Value = false;
                    response.Description = "Данные не могут быть нулевые";
                    response.Status = StatusCode.BadRequest;

                    return response;
                }

                var result = await _userManager.CreateAsync(user, password);

                if (!result.Succeeded)
                {
                    var errors = new StringBuilder(16);

                    result.Errors.ToList().ForEach(i => errors.Append(i.Description));

                    response.Value = false;
                    response.Description = errors.ToString();
                    response.Status = StatusCode.BadRequest;
                    return response;
                }

                await _userManager.AddToRoleAsync(user, Role.Client);
                await _signInManager.SignInAsync(user, false);

                response.Value = true;
                response.Description = "Пользователь успешно создан";
                response.Status = StatusCode.Ok;

                return response;
            }
            catch (Exception ex)
            {
                response.Value = false;
                response.Description = ex.Message;
                response.Status = StatusCode.IternalServerError;

                return response;
            }
        }

        public async Task<Response<bool>> ChangeUserRoleAsync(string userId, string newRole)
        {
            var response = new Response<bool>();
            try
            {
                if (string.IsNullOrEmpty(userId))
                {
                    response.Value = false;
                    response.Description = "Неверный ID пользователя";
                    response.Status = StatusCode.BadRequest;
                    return response;
                }

                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    response.Value = false;
                    response.Description = "Пользователь не найден";
                    response.Status = StatusCode.NotFound;
                    return response;
                }

                if (!await _roleManager.RoleExistsAsync(newRole))
                {
                    response.Value = false;
                    response.Description = "Роль не существует";
                    response.Status = StatusCode.BadRequest;
                    return response;
                }

                var currentRoles = await _userManager.GetRolesAsync(user);

                var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
                if (!removeResult.Succeeded)
                {
                    response.Value = false;
                    response.Description = "Не удалось удалить текущие роли пользователя";
                    response.Status = StatusCode.IternalServerError;
                    return response;
                }

                var addResult = await _userManager.AddToRoleAsync(user, newRole);
                if (!addResult.Succeeded)
                {
                    response.Value = false;
                    response.Description = "Не удалось добавить новую роль пользователю";
                    response.Status = StatusCode.IternalServerError;
                    return response;
                }

                response.Value = true;
                response.Description = "Роль пользователя успешно изменена";
                response.Status = StatusCode.Ok;
                return response;
            }
            catch (Exception ex)
            {
                response.Value = false;
                response.Description = ex.Message;
                response.Status = StatusCode.IternalServerError;
                return response;
            }
        }

        public async Task<Response<bool>> UpdateUserAsync(string userId, string name, string surname, string phoneNumber)
        {
            var response = new Response<bool>();
            try
            {
                if (string.IsNullOrEmpty(userId))
                {
                    response.Value = false;
                    response.Description = "Неверный ID пользователя";
                    response.Status = StatusCode.BadRequest;
                    return response;
                }

                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    response.Value = false;
                    response.Description = "Пользователь не найден";
                    response.Status = StatusCode.NotFound;
                    return response;
                }

                user.Name = name;
                user.Surname = surname;
                user.PhoneNumber = phoneNumber;

                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    var errors = new StringBuilder(16);
                    result.Errors.ToList().ForEach(i => errors.Append(i.Description));

                    response.Value = false;
                    response.Description = errors.ToString();
                    response.Status = StatusCode.BadRequest;
                    return response;
                }

                response.Value = true;
                response.Description = "Данные пользователя успешно обновлены";
                response.Status = StatusCode.Ok;
                return response;
            }
            catch (Exception ex)
            {
                response.Value = false;
                response.Description = ex.Message;
                response.Status = StatusCode.IternalServerError;
                return response;
            }
        }

        public async Task<Response<bool>> DeleteUserAsync(string userId)
        {
            var response = new Response<bool>();
            try
            {
                if (string.IsNullOrEmpty(userId))
                {
                    response.Value = false;
                    response.Description = "Неверный ID пользователя";
                    response.Status = StatusCode.BadRequest;
                    return response;
                }

                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
                if (user == null)
                {
                    response.Value = false;
                    response.Description = "Пользователь не найден";
                    response.Status = StatusCode.NotFound;
                    return response;
                }

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                response.Value = true;
                response.Description = "Пользователь успешно удалён";
                response.Status = StatusCode.Ok;
                return response;
            }
            catch (Exception ex)
            {
                response.Value = false;
                response.Description = ex.Message;
                response.Status = StatusCode.IternalServerError;
                return response;
            }
        }

        public async Task<Response<IEnumerable<User>>> GetAllAsync()
        {
            var response = new Response<IEnumerable<User>>();
            try
            {
                var users = await _userManager.Users.ToListAsync();

                response.Value = users;
                response.Description = "Users успешно получены";
                response.Status = StatusCode.Ok;

                return response;
            }
            catch (Exception ex)
            {
                response.Value = null;
                response.Description = ex.Message;
                response.Status = StatusCode.IternalServerError;

                return response;
            }
        }
    }
}
