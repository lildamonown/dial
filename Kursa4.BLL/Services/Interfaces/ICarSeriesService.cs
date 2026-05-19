using Kursa4.BLL.DTO;
using Kursa4.BLL.Models;

namespace Kursa4.BLL.Services.Interfaces
{
    public interface ICarSeriesService
    {
        Task<Response<List<CarSeriesDTO>>> GetAllByBrandIdAsync(int brandId);
        Task<Response<CarSeriesDTO>> GetByIdAsync(int id);
    }
}
