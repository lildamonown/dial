using AutoMapper;
using Kursa4.BLL.DTO;
using Kursa4.BLL.Models;
using Kursa4.BLL.Services.Interfaces;
using Kursa4.DAL.Entities;
using Kursa4.DAL.Exceptions;
using Kursa4.DAL.Repositories.Implementation.EF;
using Kursa4.DAL.Repositories.Interfaces;

namespace Kursa4.BLL.Services.Implimentations
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;

        public ServiceService(IServiceRepository serviceRepository, IMapper mapper)
        {
            _serviceRepository = serviceRepository;
            _mapper = mapper;
        }

        public async Task<Response<ServiceDTO>> CreateAsync(ServiceDTO item)
        {
            var response = new Response<ServiceDTO>();
            try
            {
                if (item == null)
                {
                    response.Value = null;
                    response.Status = StatusCode.BadRequest;
                    response.Description = "Модель не может быть пустой.";
                    return response;
                }

                
                var serviceEntity = _mapper.Map<Service>(item);
                var createdService = await _serviceRepository.CreateAsync(serviceEntity);

                response.Value = _mapper.Map<ServiceDTO>(createdService);
                response.Status = StatusCode.Ok;
                response.Description = "Услуга успешно создана.";
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

        public async Task<Response<ServiceDTO>> GetByIdAsync(int id)
        {
            var response = new Response<ServiceDTO>();
            try
            {
                if (id <= 0)
                {
                    response.Value = null;
                    response.Status = StatusCode.BadRequest;
                    response.Description = "ID не может быть меньше или равен 0.";
                    return response;
                }

                var service = await _serviceRepository.GetByIdAsync(id);
                if (service == null)
                {
                    response.Value = null;
                    response.Status = StatusCode.NotFound;
                    response.Description = "Услуга с таким ID не найдена.";
                    return response;
                }

                response.Value = _mapper.Map<ServiceDTO>(service);
                response.Status = StatusCode.Ok;
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

        public async Task<Response<List<ServiceDTO>>> GetAllAsync()
        {
            var response = new Response<List<ServiceDTO>>();
            try
            {
                var services = await _serviceRepository.GetAllAsync();

                response.Value = _mapper.Map<List<ServiceDTO>>(services);
                response.Status = StatusCode.Ok;
                response.Description = "Список услуг успешно получен.";
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

        public async Task<Response<ServiceDTO>> UpdateAsync(ServiceDTO item)
        {
            var response = new Response<ServiceDTO>();
            try
            {
                if (item == null)
                {
                    response.Value = null;
                    response.Status = StatusCode.BadRequest;
                    response.Description = "Модель не может быть пустой.";
                    return response;
                }

                var serviceEntity = _mapper.Map<Service>(item);
                await _serviceRepository.UpdateAsync(serviceEntity);

                response.Value = item;
                response.Status = StatusCode.Ok;
                response.Description = "Услуга успешно обновлена.";
                return response;
            }
            catch (NotFoundException ex)
            {
                response.Value = null;
                response.Status = StatusCode.NotFound;
                response.Description = ex.Message;
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
    }
}
