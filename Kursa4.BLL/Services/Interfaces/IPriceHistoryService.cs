using Kursa4.BLL.DTO;
using Kursa4.BLL.Models;

namespace Kursa4.BLL.Services.Interfaces
{
    public interface IPriceHistoryService
    {
        Task<Response<PriceHistoryDTO>> CreateAsync(PriceHistoryDTO item);

        Task<Response<List<PriceHistoryDTO>>> GetBySubserviceIdAsync(int subserviceId);
    }
}
