using Kursa4.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursa4.DAL.Repositories.Interfaces
{
    public interface ISubserviceRepository : IBaseRepository<Subservice>
    {
        Task<List<Subservice>> GetAllByIdOrderAsync(int id);

        Task<List<Subservice>> GetAllVisibleAsync();

        Task<List<Subservice>> GetAllByIdServiceAsync(int id);
    }
}
