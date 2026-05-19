using Kursa4.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kursa4.UI.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetById(int carId)
        {
            var result = await _carService.GetByIdAsync(carId);
            if (result.Status != BLL.Models.StatusCode.Ok)
            {
                return Json(new { success = false, message = result.Description });
            }

            return Json(new
            {
                success = true,
                car = result.Value
            });
        }
    }
}
