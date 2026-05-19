using Kursa4.DAL.Entities;

namespace Kursa4.DAL.Repositories.Interfaces
{
    public interface ICarSeriesRepository : IBaseRepository<CarSeries>
    {
        Task<List<CarSeries>> GetAllByBrandIdAsync(int brandId);
    }
}
