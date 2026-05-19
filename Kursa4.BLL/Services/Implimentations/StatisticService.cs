using AutoMapper;
using Kursa4.BLL.DTO;
using Kursa4.BLL.Models;
using Kursa4.BLL.Services.Interfaces;
using Kursa4.DAL.Entities;
using Kursa4.DAL.Exceptions;
using Kursa4.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Kursa4.BLL.Services.Implimentations
{
    public class StatisticService : IStatisticService
    {
        private readonly IStatisticRepository _statisticRepository;
        private readonly IMapper _mapper;

        public StatisticService(IStatisticRepository statisticRepository, IMapper mapper)
        {
            _statisticRepository = statisticRepository;
            _mapper = mapper;
        }

        public async Task<Response<List<StatisticDTO>>> GetStatisticsAsync(DateTime date)
        {
            var response = new Response<List<StatisticDTO>>();
            try
            {
                if (date == null)
                {
                    response.Value = null;
                    response.Status = StatusCode.BadRequest;
                    response.Description = "Проблема, пустая дата";
                    return response;
                }

                var statistic = await _statisticRepository.GetStatisticsAsync(date);

                response.Value = _mapper.Map<List<StatisticDTO>>(statistic);
                response.Status = StatusCode.Ok;
                response.Description = "все четка";
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
