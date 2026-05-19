using AutoMapper;
using Kursa4.BLL.DTO;
using Kursa4.BLL.Services.Interfaces;
using Kursa4.DAL.Entities;
using Kursa4.UI.Models;
using Kursa4.UI.Models.Outputs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Kursa4.UI.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;
        private readonly UserManager<User> _userManager;

        private readonly IMapper _mapper;

        public ReviewController(IReviewService reviewService, UserManager<User> userManager, IMapper mapper)
        {
            _reviewService = reviewService;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _reviewService.GetAllAsync();

            if (result.Status != BLL.Models.StatusCode.Ok)
            {
                return View("Error",
                    new ErrorViewModel
                    {
                        Controller = "Review",
                        Description = result.Description
                    });
            }

            var reviews = new List<ReviewModel>();

            foreach (var review in result.Value)
            {
                reviews.Add(new ReviewModel()
                {
                    Id = review.Id,
                    UserName = (await _userManager.FindByIdAsync(review.UserId)).Name,
                    Text = review.Text,
                    Grade = review.Grade,
                    CreateAt = review.CreateAt
                });
            }

            reviews.Reverse();

            return View(reviews);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(int rating, string userId, string text)
        {
            var review = new ReviewDTO()
            {
                UserId = userId,
                Text = text,
                Grade = rating,
                CreateAt = DateTime.Now
            };

            var result = await _reviewService.CreateAsync(review);

            if (result.Status != BLL.Models.StatusCode.Ok)
            {
                return View("Error",
                    new ErrorViewModel
                    {
                        Controller = "Review",
                        Description = result.Description
                    });
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Owner")]
        public async Task<IActionResult> Delete(int reviewId)
        {
            var resultDelete = await _reviewService.DeleteAsync(reviewId);

            if (resultDelete.Status != BLL.Models.StatusCode.Ok)
            {
                return Json(new { success = false, message = resultDelete.Description });
            }

            return Json(new { success = true });
        }

    }
}
