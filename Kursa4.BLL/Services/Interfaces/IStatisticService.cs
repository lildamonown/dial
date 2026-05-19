using Kursa4.BLL.DTO;
using Kursa4.BLL.Models;
using Kursa4.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursa4.BLL.Services.Interfaces
{
    public interface IStatisticService
    {
        Task<Response<List<StatisticDTO>>> GetStatisticsAsync(DateTime date);
    }
}
