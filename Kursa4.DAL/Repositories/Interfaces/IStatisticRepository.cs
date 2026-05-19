using Kursa4.DAL.Entities;

namespace Kursa4.DAL.Repositories.Interfaces
{
    public interface IStatisticRepository
    {
        Task<List<Statistic>> GetStatisticsAsync(DateTime date);
    }
}
