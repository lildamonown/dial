using AutoMapper;
using Kursa4.BLL.DTO;
using Kursa4.BLL.Models;
using Kursa4.BLL.Services.Interfaces;
using Kursa4.DAL.Repositories.Interfaces;

namespace Kursa4.BLL.Services.Implimentations
{
    public class CarBrandService : ICarBrandService
    {
        private readonly ICarBrandRepository _carBrandRepository;
        private readonly IMapper _mapper;

        public CarBrandService(ICarBrandRepository carBrandRepository, IMapper mapper)
        {
            _carBrandRepository = carBrandRepository;
            _mapper = mapper;
        }

        public async Task<Response<List<CarBrandDTO>>> GetAllAsync()
        {
            var response = new Response<List<CarBrandDTO>>();
            try
            {
                var brands = await _carBrandRepository.GetAllAsync();
                response.Value = _mapper.Map<List<CarBrandDTO>>(brands);
                response.Status = StatusCode.Ok;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = StatusCode.IternalServerError;
                response.Description = ex.Message;
                return response;
            }
        }

        public async Task<Response<CarBrandDTO>> GetByIdAsync(int id)
        {
            var response = new Response<CarBrandDTO>();
            try
            {
                var brand = await _carBrandRepository.GetByIdAsync(id);
                if (brand == null)
                {
                    response.Status = StatusCode.NotFound;
                    response.Description = "Марка не найдена";
                    return response;
                }

                response.Value = _mapper.Map<CarBrandDTO>(brand);
                response.Status = StatusCode.Ok;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = StatusCode.IternalServerError;
                response.Description = ex.Message;
                return response;
            }
        }
    }
}
