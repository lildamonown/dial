using Kursa4.BLL.DTO;
using Kursa4.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursa4.BLL.Services.Interfaces
{
    public interface IReportService:IBaseService<ReportDTO>
    {
        Task<Response<List<ReportDTO>>> GetAllByDateAsync(DateTime date);
    }
}
