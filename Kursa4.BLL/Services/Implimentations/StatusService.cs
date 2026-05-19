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
    public class StatusService : IStatusService
    {
        private readonly IStatusRepository _statusRepository;
        private readonly IMapper _mapper;

        public StatusService(IStatusRepository statusRepository, IMapper mapper)
        {
            _statusRepository = statusRepository;
            _mapper = mapper;
        }

        public async Task<Response<StatusDTO>> CreateAsync(StatusDTO item)
        {
            var response = new Response<StatusDTO>();
            try
            {
                if (item is null)
                {
                    response.Value = null;
                    response.Status = StatusCode.BadRequest;
                    response.Description = "Модель не может быть пустой";
                    return response;
                }

                var clientMapper = _mapper.Map<Status>(item);
                var clientCreate = await _statusRepository.CreateAsync(clientMapper);

                response.Value = _mapper.Map<StatusDTO>(clientCreate);
                response.Status = StatusCode.Ok;
                response.Description = "Все четка";
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

        public async Task<Response<StatusDTO>> GetByIdAsync(int id)
        {
            var response = new Response<StatusDTO>();
            try
            {
                if (id <= 0)
                {
                    response.Value = null;
                    response.Status = StatusCode.BadRequest;
                    response.Description = "ID не может быть меньше или равен 0";
                    return response;
                }

                var status = await _statusRepository.GetByIdAsync(id);
                if (status == null)
                {
                    response.Value = null;
                    response.Status = StatusCode.NotFound;
                    response.Description = "Статус с таким ID не найден.";
                    return response;
                }

                response.Value = _mapper.Map<StatusDTO>(status);
                response.Status = StatusCode.Ok;
                response.Description = "Статус найден.";
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

        public async Task<Response<List<StatusDTO>>> GetAllAsync()
        {
            var response = new Response<List<StatusDTO>>();
            try
            {
                var statuses = await _statusRepository.GetAllAsync();
                if (statuses == null || statuses.Count == 0)
                {
                    response.Value = null;
                    response.Status = StatusCode.NotFound;
                    response.Description = "Список статусов пуст.";
                    return response;
                }

                response.Value = _mapper.Map<List<StatusDTO>>(statuses);
                response.Status = StatusCode.Ok;
                response.Description = "Статусы получены.";
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

        public async Task<Response<StatusDTO>> UpdateAsync(StatusDTO item)
        {
            var response = new Response<StatusDTO>();
            try
            {
                if (item == null)
                {
                    response.Value = null;
                    response.Status = StatusCode.BadRequest;
                    response.Description = "Модель не может быть пустой";
                    return response;
                }

                var statusEntity = _mapper.Map<Status>(item);
                await _statusRepository.UpdateAsync(statusEntity);

                response.Value = item;
                response.Status = StatusCode.Ok;
                response.Description = "Статус успешно обновлен.";
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

        public async Task<Response<StatusDTO>> GetByNameAsync(string name)
        {
            var response = new Response<StatusDTO>();
            try
            {
                if (string.IsNullOrEmpty(name))
                {
                    response.Value = null;
                    response.Status = StatusCode.BadRequest;
                    response.Description = "Name не может быть путым";
                    return response;
                }

                var status = await _statusRepository.GetByNameAsync(name);

                if (status == null)
                {
                    response.Value = null;
                    response.Status = StatusCode.NotFound;
                    response.Description = "Статус с таким Name не найден.";
                    return response;
                }

                response.Value = _mapper.Map<StatusDTO>(status);
                response.Status = StatusCode.Ok;
                response.Description = "Статус найден.";
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
