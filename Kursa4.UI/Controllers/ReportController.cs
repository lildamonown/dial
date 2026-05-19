using AutoMapper;
using Kursa4.BLL.DTO;
using Kursa4.BLL.Models;
using Kursa4.BLL.Services.Interfaces;
using Kursa4.DAL.Entities;
using Kursa4.UI.Models;
using Kursa4.UI.Models.Inputs;
using Kursa4.UI.Models.Outputs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Kursa4.UI.Controllers
{
    public class ReportController : Controller
    {
        private readonly IReportService _reportService;
        private readonly IOrderService _orderService;
        private readonly IStatusService _statusService;
        private readonly UserManager<User> _userManager;

        private IMapper _mapper;

        public ReportController(IReportService reportService, IOrderService orderService, IMapper mapper, 
            UserManager<User> userManager, IStatusService statusService)
        {
            _reportService = reportService;
            _orderService = orderService;
            _mapper = mapper;
            _userManager = userManager;
            _statusService = statusService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
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

            var mapp = _mapper.Map<List<ReportModel>>(result.Value);

            return View(mapp);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int orderId, double price)
        {
            var masters = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var report = new ReportForCreate()
            {
                OrderId = orderId,
                FinitePrice = price,
                NameMaster = masters.Name,
                SurnameMaster = masters.Surname
            };

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
            order.StatusId = (await _statusService.GetByNameAsync(EStatus.Completed.GetValue())).Value.Id;

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
