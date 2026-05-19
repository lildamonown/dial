using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Kursa4.UI.Models;
using Kursa4.BLL.Services.Interfaces;

namespace Kursa4.UI.Controllers;

public class HomeController : Controller
{
    private readonly IReviewService _reviewService;

    public HomeController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    public async Task<IActionResult> Index()
    {
        var resultA = await _reviewService.GetAverageRatingAsync();

        if(resultA.Status != BLL.Models.StatusCode.Ok)
        {
            return View("Error",
                new ErrorViewModel
                {
                    Controller = "Home",
                    Description = resultA.Description
                });
        }

        var resultC = await _reviewService.GetAllAsync();

        if (resultC.Status != BLL.Models.StatusCode.Ok)
        {
            return View("Error",
                new ErrorViewModel
                {
                    Controller = "Home",
                    Description = resultC.Description
                });
        }

        ViewBag.AverGrade = resultA.Value;
        ViewBag.CoutReviews = resultC.Value.Count;

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
}
