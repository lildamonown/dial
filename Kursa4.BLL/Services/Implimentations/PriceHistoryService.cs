using AutoMapper;
using Kursa4.BLL.DTO;
using Kursa4.BLL.Models;
using Kursa4.BLL.Services.Interfaces;
using Kursa4.DAL.Entities;
using Kursa4.DAL.Repositories.Interfaces;

namespace Kursa4.BLL.Services.Implimentations
{
    public class PriceHistoryService : IPriceHistoryService
    {
        private readonly IPriceHistoryRepository _priceHistoryRepository;
        private readonly IMapper _mapper;

        public PriceHistoryService(IPriceHistoryRepository priceHistoryRepository, IMapper mapper)
        {
            _priceHistoryRepository = priceHistoryRepository;
            _mapper = mapper;
        }

        public async Task<Response<PriceHistoryDTO>> CreateAsync(PriceHistoryDTO item)
        {
            var response = new Response<PriceHistoryDTO>();

            try
            {
                if (item == null)
                {
                    response.Status = StatusCode.BadRequest;
                    response.Description = "Передан пустой объект.";
                    return response;
                }

                var entity = _mapper.Map<PriceHistory>(item);
                var created = await _priceHistoryRepository.CreateAsync(entity);

                response.Value = _mapper.Map<PriceHistoryDTO>(created);
                response.Status = StatusCode.Ok;
                response.Description = "История изменения цены сохранена.";

                return response;
            }
            catch (Exception ex)
            {
                response.Status = StatusCode.IternalServerError;
                response.Description = ex.Message;
                return response;
            }
        }

        public async Task<Response<List<PriceHistoryDTO>>> GetBySubserviceIdAsync(int subserviceId)
        {
            var response = new Response<List<PriceHistoryDTO>>();

            try
            {
                if (subserviceId <= 0)
                {
                    response.Status = StatusCode.BadRequest;
                    response.Description = "ID не может быть меньше или равен 0";
                    return response;
                }

                var histories = await _priceHistoryRepository.GetBySubserviceIdAsync(subserviceId);

                response.Value = _mapper.Map<List<PriceHistoryDTO>>(histories);
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
