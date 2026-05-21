using Kursa4.DAL.Entities;

namespace Kursa4.DAL.Repositories.Interfaces
{
    public interface IPriceHistoryRepository : IBaseRepository<PriceHistory>
    {
        Task<List<PriceHistory>> GetBySubserviceIdAsync(int subserviceId);
    }
}
