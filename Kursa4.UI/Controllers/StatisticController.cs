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

        public async Task<IActionResult> Index(int? year, int? month)
        {
            var targetDate = new DateTime(year ?? DateTime.Now.Year, month ?? DateTime.Now.Month, 1);

            var statistics = (await _statisticService.GetStatisticsAsync(targetDate)).Value;

            ViewBag.SelectedYear = targetDate.Year;
            ViewBag.SelectedMonth = targetDate.Month;

            return View(statistics);
        }
    }
}