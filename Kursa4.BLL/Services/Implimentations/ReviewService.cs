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
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public ReviewService(IReviewRepository reviewRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        public async Task<Response<ReviewDTO>> CreateAsync(ReviewDTO item)
        {
            var response = new Response<ReviewDTO>();
            try
            {
                if (item == null)
                {
                    response.Value = null;
                    response.Status = StatusCode.BadRequest;
                    response.Description = "Модель не может быть пустой.";
                    return response;
                }

                if (string.IsNullOrEmpty(item.UserId) || string.IsNullOrWhiteSpace(item.Text))
                {
                    response.Value = null;
                    response.Status = StatusCode.BadRequest;
                    response.Description = "Некорректные данные. Проверьте UserId и текст отзыва.";
                    return response;
                }

                var reviewEntity = _mapper.Map<Review>(item);
                var createdReview = await _reviewRepository.CreateAsync(reviewEntity);

                response.Value = _mapper.Map<ReviewDTO>(createdReview);
                response.Status = StatusCode.Ok;
                response.Description = "Отзыв успешно создан.";
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

        public async Task<Response<ReviewDTO>> GetByIdAsync(int id)
        {
            var response = new Response<ReviewDTO>();
            try
            {
                if (id <= 0)
                {
                    response.Value = null;
                    response.Status = StatusCode.BadRequest;
                    response.Description = "ID не может быть меньше или равен 0.";
                    return response;
                }

                var review = await _reviewRepository.GetByIdAsync(id);

                response.Value = _mapper.Map<ReviewDTO>(review);
                response.Status = StatusCode.Ok;
                response.Description = "Отзыв успешно найден.";
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

        public async Task<Response<List<ReviewDTO>>> GetAllAsync()
        {
            var response = new Response<List<ReviewDTO>>();
            try
            {
                var reviews = await _reviewRepository.GetAllAsync();

                response.Value = _mapper.Map<List<ReviewDTO>>(reviews);
                response.Status = StatusCode.Ok;
                response.Description = "Все отзывы успешно получены.";
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

        public async Task<Response<ReviewDTO>> UpdateAsync(ReviewDTO item)
        {
            var response = new Response<ReviewDTO>();
            try
            {
                if (item == null)
                {
                    response.Value = null;
                    response.Status = StatusCode.BadRequest;
                    response.Description = "Модель не может быть пустой.";
                    return response;
                }

                var reviewEntity = _mapper.Map<Review>(item);
                await _reviewRepository.UpdateAsync(reviewEntity);

                response.Value = item;
                response.Status = StatusCode.Ok;
                response.Description = "Отзыв успешно обновлен.";
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

        public async Task<Response<bool>> DeleteAsync(int id)
        {
            var response = new Response<bool>();
            try
            {
                if (id <= 0)
                {
                    response.Value = false;
                    response.Status = StatusCode.BadRequest;
                    response.Description = "Некорректный идентификатор.";
                    return response;
                }

                var existingReview = await _reviewRepository.GetByIdAsync(id);

                if (existingReview == null)
                {
                    response.Value = false;
                    response.Status = StatusCode.NotFound;
                    response.Description = $"Отзыв с Id {id} не найден.";
                    return response;
                }

                await _reviewRepository.DeleteAsync(id);

                response.Value = true;
                response.Status = StatusCode.Ok;
                response.Description = "Отзыв успешно удалён.";
                return response;
            }
            catch (NotFoundException ex)
            {
                response.Value = false;
                response.Status = StatusCode.NotFound;
                response.Description = ex.Message;
                return response;
            }
            catch (Exception ex)
            {
                response.Value = false;
                response.Status = StatusCode.IternalServerError;
                response.Description = ex.Message;
                return response;
            }
        }

        public async Task<Response<float>> GetAverageRatingAsync()
        {
            var response = new Response<float>();

            try
            {
                var aver = await _reviewRepository.GetAverageRatingAsync();

                response.Value = aver;
                response.Status = StatusCode.Ok;
                response.Description = "Всё пучком";

                return response;
            }
            catch(Exception ex)
            {
                response.Value = 0;
                response.Status = StatusCode.IternalServerError;
                response.Description = ex.Message;
                return response;
            }
        }
    }
}
