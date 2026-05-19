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
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<Response<OrderDTO>> CreateAsync(OrderDTO item)
        {
            var response = new Response<OrderDTO>();
            try
            {
                if (item == null)
                {
                    response.Value = null;
                    response.Status = StatusCode.BadRequest;
                    response.Description = "Модель не может быть пустой.";
                    return response;
                }

                var orderMapper = _mapper.Map<Order>(item);
                var createdOrder = await _orderRepository.CreateAsync(orderMapper);

                response.Value = _mapper.Map<OrderDTO>(createdOrder);
                response.Status = StatusCode.Ok;
                response.Description = "Заказ успешно создан.";
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

        public async Task<Response<OrderDTO>> GetByIdAsync(int id)
        {
            var response = new Response<OrderDTO>();
            try
            {
                if (id <= 0)
                {
                    response.Value = null;
                    response.Status = StatusCode.BadRequest;
                    response.Description = "ID не может быть меньше или равен 0.";
                    return response;
                }

                var order = await _orderRepository.GetByIdAsync(id);

                response.Value = _mapper.Map<OrderDTO>(order);
                response.Status = StatusCode.Ok;
                response.Description = "Все четка.";
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

        public async Task<Response<List<OrderDTO>>> GetAllAsync()
        {
            var response = new Response<List<OrderDTO>>();
            try
            {
                var orders = await _orderRepository.GetAllAsync();

                response.Value = _mapper.Map<List<OrderDTO>>(orders);
                response.Status = StatusCode.Ok;
                response.Description = "Все четка.";
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

        public async Task<Response<OrderDTO>> UpdateAsync(OrderDTO item)
        {
            var response = new Response<OrderDTO>();
            try
            {
                if (item == null)
                {
                    response.Value = null;
                    response.Status = StatusCode.BadRequest;
                    response.Description = "Модель не может быть пустой.";
                    return response;
                }

                var orderMapper = _mapper.Map<Order>(item);
                await _orderRepository.UpdateAsync(orderMapper);

                response.Value = item;
                response.Status = StatusCode.Ok;
                response.Description = "Заказ успешно обновлен.";
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

        public async Task<Response<List<OrderDTO>>> GetAllByIdUserAsync(string userId)
        {
            var response = new Response<List<OrderDTO>>();
            try
            {
                if (string.IsNullOrEmpty(userId))
                {
                    response.Value = null;
                    response.Status = StatusCode.BadRequest;
                    response.Description = "Айди не может быть меньше 1";
                    return response;
                }

                var orders = await _orderRepository.GetAllByIdUserAsync(userId);

                response.Value = _mapper.Map<List<OrderDTO>>(orders);
                response.Status = StatusCode.Ok;
                response.Description = "Все четка.";
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
