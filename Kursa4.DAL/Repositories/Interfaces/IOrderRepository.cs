using Kursa4.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursa4.DAL.Repositories.Interfaces
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Task<List<Order>> GetAllByIdUserAsync(string userId);

        Task<List<Order>> GetAllNotConfirmedAsync();

        Task<List<Order>> GetAllByVisiteDateAsync(DateTime date);

        Task<List<Order>> GetAllByStatusIdAsync(int id);
    }
}
