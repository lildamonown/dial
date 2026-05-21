using AutoMapper;
using Kursa4.BLL.DTO;
using Kursa4.BLL.Services.Interfaces;
using Kursa4.DAL.Entities;
using Kursa4.UI.Models;
using Kursa4.UI.Models.Inputs;
using Kursa4.UI.Models.Outputs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace Kursa4.UI.Controllers
{
    public class SubserviceController : Controller
    {
        private readonly IServiceService _serviceService;
        private readonly ISubserviceService _subserviceService;
        private readonly IPriceHistoryService _priceHistoryService;
        private readonly UserManager<User> _userManager;

        private readonly IMapper _mapper;

        public SubserviceController(IServiceService serviceService, ISubserviceService subserviceService,
            IPriceHistoryService priceHistoryService, UserManager<User> userManager, IMapper mapper)
        {
            _serviceService = serviceService;
            _subserviceService = subserviceService;
            _priceHistoryService = priceHistoryService;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Economist,Owner")]
        public async Task<IActionResult> Create()
        {
            var resultServices = await _serviceService.GetAllAsync();

            if (resultServices.Status != BLL.Models.StatusCode.Ok)
            {
                return View("Error",
                    new ErrorViewModel
                    {
                        Controller = "Subservice",
                        Description = resultServices.Description
                    });
            }

            ViewBag.Services = new SelectList(_mapper.Map<List<ServiceModel>>(resultServices.Value), "Id", "Name");

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Economist,Owner")]
        public async Task<IActionResult> Create(SubserviceForCreate subservice)
        {
            if (!ModelState.IsValid)
            {
                var resultServices = await _serviceService.GetAllAsync();

                if (resultServices.Status != BLL.Models.StatusCode.Ok)
                {
                    return View("Error",
                        new ErrorViewModel
                        {
                            Controller = "Subservice",
                            Description = resultServices.Description
                        });
                }

                ViewBag.Services = new SelectList(_mapper.Map<List<ServiceModel>>(resultServices.Value), "Id", "Name");
                return View(subservice);
            }

            var mapp = _mapper.Map<SubserviceDTO>(subservice);

            var resulCreate = await _subserviceService.CreateAsync(mapp);

            if (resulCreate.Status != BLL.Models.StatusCode.Ok)
            {
                return View("Error",
                    new ErrorViewModel
                    {
                        Controller = "Subservice",
                        Description = resulCreate.Description
                    });
            }

            return RedirectToAction("Index", "Service");
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Economist,Owner")]
        public async Task<IActionResult> Edit(int subserviceId)
        {
            var result = await _subserviceService.GetByIdAsync(subserviceId);

            if(result.Status != BLL.Models.StatusCode.Ok)
            {
                return View("Error",
                    new ErrorViewModel
                    {
                        Controller = "Subservice",
                        Description = result.Description
                    });
            }

            var mapp = _mapper.Map<SubserviceForEdit>(result.Value);

            var resultServices = await _serviceService.GetAllAsync();

            if (resultServices.Status != BLL.Models.StatusCode.Ok)
            {
                return View("Error",
                    new ErrorViewModel
                    {
                        Controller = "Subservice",
                        Description = resultServices.Description
                    });
            }

            ViewBag.Services = new SelectList(_mapper.Map<List<ServiceModel>>(resultServices.Value), "Id", "Name");

            return View(mapp);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Economist,Owner")]
        public async Task<IActionResult> Edit(SubserviceForEdit subservice)
        {
            if (!ModelState.IsValid)
            {
                var resultServices = await _serviceService.GetAllAsync();
                if (resultServices.Status != BLL.Models.StatusCode.Ok)
                {
                    return View("Error", new ErrorViewModel
                    {
                        Controller = "Subservice",
                        Description = resultServices.Description
                    });
                }

                ViewBag.Services = resultServices.Value;
                return View(subservice);
            }

            var oldSubserviceResult = await _subserviceService.GetByIdAsync(subservice.Id);
            if (oldSubserviceResult.Status != BLL.Models.StatusCode.Ok)
            {
                return View("Error",
                    new ErrorViewModel
                    {
                        Controller = "Subservice",
                        Description = oldSubserviceResult.Description
                    });
            }

            var oldPrice = oldSubserviceResult.Value.Price;

            var mapp = _mapper.Map<SubserviceDTO>(subservice);

            var resultUpdate = await _subserviceService.UpdateAsync(mapp);

            if(resultUpdate.Status != BLL.Models.StatusCode.Ok)
            {
                return View("Error",
                    new ErrorViewModel
                    {
                        Controller = "Subservice",
                        Description = resultUpdate.Description
                    });
            }

            if (oldPrice != subservice.Price)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = await _userManager.FindByIdAsync(userId);

                var priceHistory = new PriceHistoryDTO
                {
                    SubserviceId = subservice.Id,
                    OldPrice = oldPrice,
                    NewPrice = subservice.Price,
                    Comment = subservice.PriceComment ?? "",
                    ChangedAt = DateTime.Now,
                    MasterName = user?.Name ?? "",
                    MasterSurname = user?.Surname ?? ""
                };

                await _priceHistoryService.CreateAsync(priceHistory);
            }

            return RedirectToAction("Index", "Service");
        }

        [HttpGet]
        public async Task<IActionResult> GetPriceHistory(int subserviceId)
        {
            var result = await _priceHistoryService.GetBySubserviceIdAsync(subserviceId);

            if (result.Status != BLL.Models.StatusCode.Ok)
            {
                return Json(new { success = false, message = result.Description });
            }

            return Json(new { success = true, history = result.Value });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _subserviceService.GetAllAsync();

            if(result.Status != BLL.Models.StatusCode.Ok)
            {
                return View("Error",
                    new ErrorViewModel
                    {
                        Controller = "Subservice",
                        Description = result.Description
                    });
            }

            result.Value.RemoveAll(s => !s.Visible);
            var mapp = _mapper.Map<List<SubserviceModel>>(result.Value);

            return Json(new { success = true, subservices = mapp });
        }
    }
}
