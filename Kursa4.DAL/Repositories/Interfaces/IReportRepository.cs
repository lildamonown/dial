using Kursa4.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursa4.DAL.Repositories.Interfaces
{
    public interface IReportRepository : IBaseRepository<Report>
    {
        Task<List<Report>> GetAllByDateAsync(DateTime date);
    }
}
