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
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;
        private readonly IMapper _mapper;

        public ReportService(IReportRepository reportRepository, IMapper mapper)
        {
            _reportRepository = reportRepository;
            _mapper = mapper;
        }

        public async Task<Response<ReportDTO>> CreateAsync(ReportDTO item)
        {
            var response = new Response<ReportDTO>();
            try
            {
                if (item == null)
                {
                    response.Value = null;
                    response.Status = StatusCode.BadRequest;
                    response.Description = "Модель не может быть пустой.";
                    return response;
                }

                var reportEntity = _mapper.Map<Report>(item);
                var createdReport = await _reportRepository.CreateAsync(reportEntity);

                response.Value = _mapper.Map<ReportDTO>(createdReport);
                response.Status = StatusCode.Ok;
                response.Description = "Отчет успешно создан.";
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

        public async Task<Response<ReportDTO>> GetByIdAsync(int id)
        {
            var response = new Response<ReportDTO>();
            try
            {
                if (id <= 0)
                {
                    response.Value = null;
                    response.Status = StatusCode.BadRequest;
                    response.Description = "ID не может быть меньше или равен 0.";
                    return response;
                }

                var report = await _reportRepository.GetByIdAsync(id);

                response.Value = _mapper.Map<ReportDTO>(report);
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

        public async Task<Response<List<ReportDTO>>> GetAllAsync()
        {
            var response = new Response<List<ReportDTO>>();
            try
            {
                var reports = await _reportRepository.GetAllAsync();

                response.Value = _mapper.Map<List<ReportDTO>>(reports);
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

        public async Task<Response<ReportDTO>> UpdateAsync(ReportDTO item)
        {
            var response = new Response<ReportDTO>();
            try
            {
                if (item == null)
                {
                    response.Value = null;
                    response.Status = StatusCode.BadRequest;
                    response.Description = "Модель не может быть пустой.";
                    return response;
                }

                var reportEntity = _mapper.Map<Report>(item);
                await _reportRepository.UpdateAsync(reportEntity);

                response.Value = item;
                response.Status = StatusCode.Ok;
                response.Description = "Отчет успешно обновлен.";
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

        public async Task<Response<List<ReportDTO>>> GetAllByDateAsync(DateTime date)
        {
            var response = new Response<List<ReportDTO>>();
            try
            {
                if (date == null)
                {
                    response.Value = null;
                    response.Status = StatusCode.BadRequest;
                    response.Description = "Дата не может быть пустой.";
                    return response;
                }

                var report = await _reportRepository.GetAllByDateAsync(date);

                response.Value = _mapper.Map<List<ReportDTO>>(report);
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
