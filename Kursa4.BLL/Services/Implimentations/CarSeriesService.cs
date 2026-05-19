using AutoMapper;
using Kursa4.BLL.DTO;
using Kursa4.BLL.Models;
using Kursa4.BLL.Services.Interfaces;
using Kursa4.DAL.Repositories.Interfaces;

namespace Kursa4.BLL.Services.Implimentations
{
    public class CarSeriesService : ICarSeriesService
    {
        private readonly ICarSeriesRepository _carSeriesRepository;
        private readonly IMapper _mapper;

        public CarSeriesService(ICarSeriesRepository carSeriesRepository, IMapper mapper)
        {
            _carSeriesRepository = carSeriesRepository;
            _mapper = mapper;
        }

        public async Task<Response<List<CarSeriesDTO>>> GetAllByBrandIdAsync(int brandId)
        {
            var response = new Response<List<CarSeriesDTO>>();
            try
            {
                var series = await _carSeriesRepository.GetAllByBrandIdAsync(brandId);
                response.Value = _mapper.Map<List<CarSeriesDTO>>(series);
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

        public async Task<Response<CarSeriesDTO>> GetByIdAsync(int id)
        {
            var response = new Response<CarSeriesDTO>();
            try
            {
                var series = await _carSeriesRepository.GetByIdAsync(id);
                if (series == null)
                {
                    response.Status = StatusCode.NotFound;
                    response.Description = "Серия не найдена";
                    return response;
                }
                response.Value = _mapper.Map<CarSeriesDTO>(series);
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
