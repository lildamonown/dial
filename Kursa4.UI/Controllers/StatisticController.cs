using Kursa4.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Kursa4.UI.Controllers
{
    public class StatisticController : Controller
    {
        private readonly IStatisticService _statisticService;

        public StatisticController(IStatisticService statisticService)
        {
            _statisticService = statisticService;
        }

        public async Task<IActionResult> Index(DateTime? date)
        {
            var targetDate = date ?? DateTime.Now;

            var statistics = (await _statisticService.GetStatisticsAsync(targetDate)).Value;

            ViewBag.SelectedDate = targetDate.ToString("yyyy-MM");

            return View(statistics);
        }
    }
}