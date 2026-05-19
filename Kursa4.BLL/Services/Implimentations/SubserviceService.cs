using AutoMapper;
using Kursa4.BLL.DTO;
using Kursa4.BLL.Models;
using Kursa4.BLL.Services.Interfaces;
using Kursa4.DAL.Entities;
using Kursa4.DAL.Repositories.Interfaces;

namespace Kursa4.BLL.Services.Implimentations
{
    public class SubserviceService : ISubserviceService
    {
        private readonly ISubserviceRepository _subserviceRepository;
        private readonly IMapper _mapper;

        public SubserviceService(ISubserviceRepository subserviceRepository, IMapper mapper)
        {
            _subserviceRepository = subserviceRepository;
            _mapper = mapper;
        }

        public async Task<Response<SubserviceDTO>> CreateAsync(SubserviceDTO item)
        {
            var response = new Response<SubserviceDTO>();

            try
            {
                if (item == null)
                {
                    response.Status = StatusCode.BadRequest;
                    response.Description = "Передан пустой объект.";
                    return response;
                }

                var subserviceEntity = _mapper.Map<Subservice>(item);
                var createdSubservice = await _subserviceRepository.CreateAsync(subserviceEntity);

                response.Value = _mapper.Map<SubserviceDTO>(createdSubservice);
                response.Status = StatusCode.Ok;
                response.Description = "Подуслуга успешно создана.";

                return response;
            }
            catch (Exception ex)
            {
                response.Status = StatusCode.IternalServerError;
                response.Description = ex.Message;
                return response;
            }
        }

        public async Task<Response<SubserviceDTO>> GetByIdAsync(int id)
        {
            var response = new Response<SubserviceDTO>();

            try
            {
                if (id <= 0)
                {
                    response.Status = StatusCode.BadRequest;
                    response.Description = "ID не может быть меньше или равен 0";
                    return response;
                }

                var subservice = await _subserviceRepository.GetByIdAsync(id);

                if (subservice == null)
                {
                    response.Status = StatusCode.NotFound;
                    response.Description = "Подуслуга с таким ID не найдена.";
                    return response;
                }

                response.Value = _mapper.Map<SubserviceDTO>(subservice);
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

        public async Task<Response<List<SubserviceDTO>>> GetAllAsync()
        {
            var response = new Response<List<SubserviceDTO>>();

            try
            {
                var subservices = await _subserviceRepository.GetAllAsync();

                if (subservices == null || subservices.Count == 0)
                {
                    response.Status = StatusCode.NotFound;
                    response.Description = "Список подуслуг пуст.";
                    return response;
                }

                response.Value = _mapper.Map<List<SubserviceDTO>>(subservices);
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

        public async Task<Response<List<SubserviceDTO>>> GetAllByIdServiceAsync(int id)
        {
            var response = new Response<List<SubserviceDTO>>();

            try
            {
                if (id <= 0)
                {
                    response.Value = null;
                    response.Status = StatusCode.BadRequest;
                    response.Description = "ID не может быть меньше или равен 0";
                    return response;
                }

                var subservice = await _subserviceRepository.GetAllByIdServiceAsync(id);

                if (subservice == null)
                {
                    response.Value = null;
                    response.Status = StatusCode.NotFound;
                    response.Description = "Подуслуги с таким Id услуги не найдена.";
                    return response;
                }

                response.Value = _mapper.Map<List<SubserviceDTO>>(subservice);
                response.Status = StatusCode.Ok;
                response.Description = "Всё гуд";
                return response;
            }
            catch (Exception ex)
            {
                response.Value = null;
                response.Status = StatusCode.IternalServerError;
                response.Description = ex.Message;
                return response;
            }
        }

        public async Task<Response<SubserviceDTO>> UpdateAsync(SubserviceDTO item)
        {
            var response = new Response<SubserviceDTO>();

            try
            {
                if (item == null)
                {
                    response.Status = StatusCode.BadRequest;
                    response.Description = "Передан пустой объект.";
                    return response;
                }

                var subserviceEntity = _mapper.Map<Subservice>(item);
                await _subserviceRepository.UpdateAsync(subserviceEntity);

                response.Value = item;
                response.Status = StatusCode.Ok;
                response.Description = "Подуслуга успешно обновлена.";

                return response;
            }
            catch (Exception ex)
            {
                response.Status = StatusCode.IternalServerError;
                response.Description = ex.Message;
                return response;
            }
        }

        public async Task<Response<List<SubserviceDTO>>> GetAllByIdOrderAsync(int id)
        {
            var response = new Response<List<SubserviceDTO>>();
            try
            {
                if (id < 0)
                {
                    response.Value = null;
                    response.Status = StatusCode.BadRequest;
                    response.Description = "Айди не может быть меньше нуля";
                    return response;
                }

                var orderByID = await _subserviceRepository.GetAllByIdOrderAsync(id);
                 
                response.Value = _mapper.Map<List<SubserviceDTO>>(orderByID);
                response.Status = StatusCode.Ok;
                response.Description = "все четка";
                return response;
            }
            catch(Exception ex)
            {
                response.Value = null;
                response.Status = StatusCode.IternalServerError;
                response.Description = ex.Message;
                return response;
            }
        }
    }
}
