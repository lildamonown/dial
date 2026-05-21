using AutoMapper;
using Kursa4.BLL.DTO;
using Kursa4.BLL.Models;
using Kursa4.BLL.Services.Interfaces;
using Kursa4.DAL.Entities;
using Kursa4.UI.Models;
using Kursa4.UI.Models.Inputs;
using Kursa4.UI.Models.Outputs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Kursa4.UI.Controllers
{
    [Authorize(Roles = "Admin,Owner,Economist,Master")]
    public class ReportController : Controller
    {
        private readonly IReportService _reportService;
        private readonly IOrderService _orderService;
        private readonly IStatusService _statusService;
        private readonly ICarService _carService;
        private readonly UserManager<User> _userManager;

        private IMapper _mapper;

        public ReportController(IReportService reportService, IOrderService orderService, IMapper mapper, 
            UserManager<User> userManager, IStatusService statusService, ICarService carService)
        {
            _reportService = reportService;
            _orderService = orderService;
            _mapper = mapper;
            _userManager = userManager;
            _statusService = statusService;
            _carService = carService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? year, int? month)
        {
            var targetYear = year ?? DateTime.Now.Year;
            var targetMonth = month ?? DateTime.Now.Month;
            ViewBag.SelectedYear = targetYear;
            ViewBag.SelectedMonth = targetMonth;

            var result = await _reportService.GetAllAsync();

            if(result.Status != BLL.Models.StatusCode.Ok)
            {
                return View("Error",
                    new ErrorViewModel
                    {
                        Controller = $"Report/Index",
                        Description = result.Description
                    });
            }

            var filtered = result.Value
                .Where(r => r.DateCompleted.Year == targetYear && r.DateCompleted.Month == targetMonth)
                .ToList();

            var mapp = _mapper.Map<List<ReportModel>>(filtered);

            foreach (var report in mapp)
            {
                var orderResult = await _orderService.GetByIdAsync(report.OrderId);
                if (orderResult.Status == BLL.Models.StatusCode.Ok)
                {
                    var order = orderResult.Value;
                    report.OrderSubservices = order.Subservices?.Select(s => s.Name).ToList() ?? [];
                    report.OrderSumPrice = order.Subservices?.Sum(s => s.Price) ?? 0;

                    var statusResult = await _statusService.GetByIdAsync(order.StatusId);
                    if (statusResult.Status == BLL.Models.StatusCode.Ok)
                        report.StatusName = statusResult.Value.Name;

                    var carResult = await _carService.GetByIdAsync(order.CarId);
                    if (carResult.Status == BLL.Models.StatusCode.Ok)
                        report.CarMark = carResult.Value.Mark;

                    var user = await _userManager.FindByIdAsync(order.UserId);
                    if (user != null)
                    {
                        report.ClientName = $"{user.Name} {user.Surname}";
                        report.ClientPhone = user.PhoneNumber ?? "";
                    }
                }
            }

            return View(mapp);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int orderId, double price)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return View("Error",
                    new ErrorViewModel
                    {
                        Controller = "Report/Create",
                        Description = "Пользователь не найден"
                    });
            }

            var masters = await _userManager.FindByIdAsync(userId);
            if (masters == null)
            {
                return View("Error",
                    new ErrorViewModel
                    {
                        Controller = "Report/Create",
                        Description = "Пользователь не найден"
                    });
            }

            var report = new ReportForCreate()
            {
                OrderId = orderId,
                FinitePrice = price,
                NameMaster = masters.Name ?? "",
                SurnameMaster = masters.Surname ?? ""
            };

            ViewBag.OriginalPrice = price;

            return View(report);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ReportForCreate report)
        {
            report.DateCompleted = DateTime.Now;

            var result = await _orderService.GetByIdAsync(report.OrderId);

            if(result.Status != BLL.Models.StatusCode.Ok)
            {
                return View("Error",
                    new ErrorViewModel
                    {
                        Controller = $"Report/Create",
                        Description = result.Description
                    });
            }

            var order = result.Value;
            var completedStatus = await _statusService.GetByNameAsync(EStatus.Completed.GetValue());
            if (completedStatus.Status != BLL.Models.StatusCode.Ok)
            {
                return View("Error",
                    new ErrorViewModel
                    {
                        Controller = "Report/Create",
                        Description = completedStatus.Description
                    });
            }
            order.StatusId = completedStatus.Value.Id;

            var resultUpdate = await _orderService.UpdateAsync(order);

            if (resultUpdate.Status != BLL.Models.StatusCode.Ok)
            {
                return View("Error",
                    new ErrorViewModel
                    {
                        Controller = $"Report/Create",
                        Description = resultUpdate.Description
                    });
            }

            var mapp = _mapper.Map<ReportDTO>(report);

            var resulCreate = await _reportService.CreateAsync(mapp);

            if (resulCreate.Status != BLL.Models.StatusCode.Ok)
            {
                return View("Error",
                    new ErrorViewModel
                    {
                        Controller = $"Report/Create",
                        Description = resulCreate.Description
                    });
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
