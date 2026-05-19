using AutoMapper;
using Kursa4.BLL.DTO;
using Kursa4.BLL.Models;
using Kursa4.BLL.Services.Interfaces;
using Kursa4.DAL.Entities;
using Kursa4.DAL.Exceptions;
using Kursa4.DAL.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Kursa4.BLL.Services.Implimentations
{
    public class CarService : ICarService
    {
        private ICarRepository _carRepository;
        private IMapper _mapper;

        public CarService(ICarRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }

        public async Task<Response<CarDTO>> CreateAsync(CarDTO item)
        {
            var response = new Response<CarDTO>();
            try
            {
                if (item is null)
                {
                    response.Value = null;
                    response.Status = StatusCode.BadRequest;
                    response.Description = "Модель не может быть пустой";
                    return response;
                }
                var carMapper = _mapper.Map<Car>(item);
                var carCreate = await _carRepository.CreateAsync(carMapper);

                response.Value = _mapper.Map<CarDTO>(carCreate);
                response.Status = StatusCode.Ok;
                response.Description = "Все гуд";
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

        public async Task<Response<CarDTO>> GetByIdAsync(int id)
        {
            var response = new Response<CarDTO>();
            try
            {
                if (id <= 0)
                {
                    response.Value = null;
                    response.Status = StatusCode.BadRequest;
                    response.Description = "ID не модет быть меньше или равн 0";
                    return response;
                }

                var car = await _carRepository.GetByIdAsync(id);

                response.Value = _mapper.Map<CarDTO>(car);
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

        public async Task<Response<List<CarDTO>>> GetAllAsync()
        {
            var response = new Response<List<CarDTO>>();
            try
            {
                var cars = await _carRepository.GetAllAsync();

                response.Value = _mapper.Map<List<CarDTO>>(cars);
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

        public async Task<Response<CarDTO>> UpdateAsync(CarDTO item)
        {
            var response = new Response<CarDTO>();
            try
            {
                if (item == null)
                {
                    response.Value = null;
                    response.Status = StatusCode.BadRequest;
                    response.Description = "Модель не может быть пустой";
                    return response;

                }

                var carMapper = _mapper.Map<Car>(item);
                await _carRepository.UpdateAsync(carMapper);

                response.Value = item;
                response.Status = StatusCode.Ok;
                response.Description = "Все четка";
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
