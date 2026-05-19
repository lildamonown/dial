using AutoMapper;
using Kursa4.BLL.DTO;
using Kursa4.BLL.Services.Implimentations;
using Kursa4.BLL.Services.Interfaces;
using Kursa4.DAL.Entities;
using Kursa4.UI.Models;
using Kursa4.UI.Models.Inputs;
using Kursa4.UI.Models.Outputs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kursa4.UI.Controllers
{
    public class ServiceController : Controller
    {
        private readonly IServiceService _serviceService;
        private readonly ISubserviceService _subserviceService;

        private readonly IMapper _mapper;

        public ServiceController(IServiceService serviceService, ISubserviceService subserviceService, IMapper mapper)
        {
            _serviceService = serviceService;
            _subserviceService = subserviceService;

            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var resultService = await _serviceService.GetAllAsync();

            if (resultService.Status != BLL.Models.StatusCode.Ok)
            {
                return View("Error",
                    new ErrorViewModel
                    {
                        Controller = "Service",
                        Description = resultService.Description
                    });
            }

            var serviceM = new List<ServiceModel>();

            foreach (var service in resultService.Value)
            {
                var mapService = _mapper.Map<ServiceModel>(service);

                var resultSubservices = await _subserviceService.GetAllByIdServiceAsync(mapService.Id);

                if (resultSubservices.Status != BLL.Models.StatusCode.Ok)
                {
                    return View("Error",
                        new ErrorViewModel
                        {
                            Controller = "Service",
                            Description = resultSubservices.Description
                        });
                }

                mapService.Subservices = _mapper.Map<List<SubserviceModel>>(resultSubservices.Value);

                serviceM.Add(mapService);
            }

            serviceM.Sort((x, y) => x.Name.CompareTo(y.Name));

            return View(serviceM);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Economist,Owner")]
        public async Task<IActionResult> Edit(int serviceId)
        {
            var result = await _serviceService.GetByIdAsync(serviceId);

            if (result.Status != BLL.Models.StatusCode.Ok)
            {
                return View("Error",
                    new ErrorViewModel
                    {
                        Controller = "Service",
                        Description = result.Description
                    });
            }

            var mapp = _mapper.Map<ServiceForEdit>(result.Value);

            return View(mapp);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Economist,Owner")]
        public async Task<IActionResult> Edit(ServiceForEdit service)
        {
            if (!ModelState.IsValid)
                return View(service);

            var mapp = _mapper.Map<ServiceDTO>(service);

            var resultUpdate = await _serviceService.UpdateAsync(mapp);

            if (resultUpdate.Status != BLL.Models.StatusCode.Ok)
            {
                return View("Error",
                    new ErrorViewModel
                    {
                        Controller = "Service",
                        Description = resultUpdate.Description
                    });
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Economist,Owner")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Economist,Owner")]
        public async Task<IActionResult> Create(ServiceForCreate service)
        {
            if (!ModelState.IsValid)
                return View(service);

            var mapp = _mapper.Map<ServiceDTO>(service);

            var resultCreate = await _serviceService.CreateAsync(mapp);

            if (resultCreate.Status != BLL.Models.StatusCode.Ok)
            {
                return View("Error",
                    new ErrorViewModel
                    {
                        Controller = "Service",
                        Description = resultCreate.Description
                    });
            }

            return RedirectToAction("Index");
        }
    }
}
