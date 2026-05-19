using Kursa4.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursa4.DAL.Repositories.Interfaces
{
    public interface IStatusRepository : IBaseRepository<Status>
    {
        Task<Status> GetByNameAsync(string name);
    }
}
