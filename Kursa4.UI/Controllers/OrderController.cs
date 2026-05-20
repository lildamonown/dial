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
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Kursa4.UI.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ICarService _carService;
        private readonly IStatusService _statusService;
        private readonly ICarBrandService _carBrandService;
        private readonly ICarSeriesService _carSeriesService;

        private readonly UserManager<User> _userManager;

        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IMapper mapper, ICarService carService,
            IStatusService statusService, UserManager<User> userManager, ICarBrandService carBrandService,
            ICarSeriesService carSeriesService)
        {
            _orderService = orderService;
            _mapper = mapper;
            _carService = carService;
            _statusService = statusService;
            _userManager = userManager;
            _carBrandService = carBrandService;
            _carSeriesService = carSeriesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSeriesByBrand(int brandId)
        {
            var result = await _carSeriesService.GetAllByBrandIdAsync(brandId);
            if (result.Status != BLL.Models.StatusCode.Ok)
            {
                return Json(new { success = false, message = result.Description });
            }
            return Json(new { success = true, series = result.Value });
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create()
        {
            var order = new OrderForCreate()
            {
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
            };

            var brandsResult = await _carBrandService.GetAllAsync();
            if (brandsResult.Status == BLL.Models.StatusCode.Ok)
            {
                ViewBag.CarBrands = new SelectList(brandsResult.Value, "Id", "Name");
            }

            ViewBag.EngineTypes = new SelectList(CarReferences.EngineTypes);
            ViewBag.BodyTypes = new SelectList(CarReferences.BodyTypes);
            ViewBag.Drives = new SelectList(CarReferences.Drives);

            return View(order);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(OrderForCreate order)
        {
            if (!ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(order.Car.Mark))
                    ModelState.AddModelError("Car", "Добавьте автомобиль");

                var brandsResult = await _carBrandService.GetAllAsync();
                if (brandsResult.Status == BLL.Models.StatusCode.Ok)
                {
                    ViewBag.CarBrands = new SelectList(brandsResult.Value, "Id", "Name");
                }

                ViewBag.EngineTypes = new SelectList(CarReferences.EngineTypes);
                ViewBag.BodyTypes = new SelectList(CarReferences.BodyTypes);
                ViewBag.Drives = new SelectList(CarReferences.Drives);

                return View(order);
            }

            var mappCar = _mapper.Map<CarDTO>(order.Car);
            var resultCarCreate = await _carService.CreateAsync(mappCar);

            if(resultCarCreate.Status != BLL.Models.StatusCode.Ok)
            {
                return View("Error",
                    new ErrorViewModel
                    {
                        Controller = "Order",
                        Description = resultCarCreate.Description
                    });
            }

            var result = await _statusService.GetByNameAsync(EStatus.Processing.GetValue());

            if (result.Status != BLL.Models.StatusCode.Ok)
            {
                return View("Error",
                    new ErrorViewModel
                    {
                        Controller = "Order",
                        Description = result.Description
                    });
            }

            order.StatusId = result.Value.Id;
            order.Car.Id = resultCarCreate.Value.Id;
            order.CreateAt = DateTime.Now;

            var mappOrder = _mapper.Map<OrderDTO>(order);

            var resultOrderCreate = await _orderService.CreateAsync(mappOrder);

            if (resultOrderCreate.Status != BLL.Models.StatusCode.Ok)
            {
                return View("Error",
                    new ErrorViewModel
                    {
                        Controller = "Order",
                        Description = resultOrderCreate.Description
                    });
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize(Roles = "Master,Admin,Owner,Economist")]
        public async Task<IActionResult> ShowAll()
        {
            ViewData["Title"] = "Все заказы";

            var result = await _orderService.GetAllAsync();

            if (result.Status != BLL.Models.StatusCode.Ok)
            {
                return View("Error",
                    new ErrorViewModel
                    {
                        Controller = "Order/ShowAll",
                        Description = result.Description
                    });
            }

            return View(await Mapp(result.Value));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ShowByUserId()
        {
            ViewData["Title"] = "Все ваши заказы";

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _orderService.GetAllByIdUserAsync(userId);

            if (result.Status != BLL.Models.StatusCode.Ok)
            {
                return View("Error",
                    new ErrorViewModel
                    {
                        Controller = "Order/ShowAllByUserId",
                        Description = result.Description
                    });
            }

            return View("ShowAll", await Mapp(result.Value));
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int orderId)
        {
            var result = await _orderService.GetByIdAsync(orderId);

            if (result.Status != BLL.Models.StatusCode.Ok)
            {
                return Json(new { success = false, message = result.Description });
            }

            var mapp = _mapper.Map<OrderModel>(result.Value);

            var resultUser = await _userManager.FindByIdAsync(result.Value.UserId);

            if (resultUser == null)
            {
                return Json(new { success = false, message = "Пользователь не найден" });
            }

            mapp.UserName = resultUser.Name;
            mapp.UserSurname = resultUser.Surname;
            mapp.UserPhoneNumber = resultUser.PhoneNumber;

            var statusResult = await _statusService.GetByIdAsync(result.Value.StatusId);
            if (statusResult.Status == BLL.Models.StatusCode.Ok)
                mapp.StatusName = statusResult.Value.Name;

            return Json(new { success = true, order = mapp });
        }

        [HttpGet]
        [Authorize(Roles = "Master,Admin,Owner,Economist")]
        public async Task<IActionResult> Edit(int orderId)
        {
            var result = await _orderService.GetByIdAsync(orderId);

            if (result.Status != BLL.Models.StatusCode.Ok)
            {
                return View("Error",
                    new ErrorViewModel
                    {
                        Controller = $"Order/Edit/{orderId}",
                        Description = result.Description
                    });
            }

            var resultStatuses = await _statusService.GetAllAsync();

            ViewBag.Statuses = new SelectList(_mapper.Map<List<StatusDTO>>(resultStatuses.Value), "Id", "Name");

            var mapp = _mapper.Map<OrderForEdit>(result.Value);

            return View(mapp);
        }

        [HttpPost]
        [Authorize(Roles = "Master,Admin,Owner,Economist")]
        public async Task<IActionResult> Edit(OrderForEdit order)
        {
            if (!ModelState.IsValid)
            {
                var resultStatuses = await _statusService.GetAllAsync();
                ViewBag.Statuses = new SelectList(_mapper.Map<List<StatusDTO>>(resultStatuses.Value), "Id", "Name");
                return View(order);
            }

            var resultStatus = await _statusService.GetByNameAsync(EStatus.Completed.GetValue());

            if (resultStatus.Status != BLL.Models.StatusCode.Ok)
            {
                return View("Error",
                    new ErrorViewModel
                    {
                        Controller = "Order/Edit",
                        Description = resultStatus.Description
                    });
            }

            if(order.StatusId == resultStatus.Value.Id)
            {
                if (order.Subservices == null || !order.Subservices.Any())
                {
                    ModelState.AddModelError("Subservices", "Добавьте хотя бы одну подуслугу перед завершением заказа");
                    var resultStatuses = await _statusService.GetAllAsync();
                    ViewBag.Statuses = new SelectList(_mapper.Map<List<StatusDTO>>(resultStatuses.Value), "Id", "Name");
                    return View(order);
                }
                return RedirectToAction("Create", "Report", new { orderId = order.Id, price = order.Subservices.Sum(s => s.Price) });
            }

            var mapp = _mapper.Map<OrderDTO>(order);
            var resultUpdate = await _orderService.UpdateAsync(mapp);

            if(resultUpdate.Status != BLL.Models.StatusCode.Ok)
            {
                return View("Error",
                    new ErrorViewModel
                    {
                        Controller = $"Order/Edit",
                        Description = resultUpdate.Description
                    });
            }

            return RedirectToAction("ShowAll");
        }

        private async Task<List<OrderModel>> Mapp(List<OrderDTO> orders)
        {
            var ordersModel = new List<OrderModel>();
            foreach (var orderDto in orders)
            {
                var orderModel = _mapper.Map<OrderModel>(orderDto);

                var user = await _userManager.FindByIdAsync(orderDto.UserId);
                if (user != null)
                {
                    orderModel.UserName = user.Name;
                    orderModel.UserSurname = user.Surname;
                    orderModel.UserPhoneNumber = user.PhoneNumber;
                }

                var statusResult = await _statusService.GetByIdAsync(orderDto.StatusId);
                if (statusResult.Status == BLL.Models.StatusCode.Ok)
                {
                    orderModel.StatusName = statusResult.Value.Name;
                }

                ordersModel.Add(orderModel);
            }

            return ordersModel;
        }
    }
}
