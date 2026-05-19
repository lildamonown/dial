using Kursa4.BLL.DTO;
using Kursa4.BLL.Models;

namespace Kursa4.BLL.Services.Interfaces
{
    public interface ICarBrandService
    {
        Task<Response<List<CarBrandDTO>>> GetAllAsync();
        Task<Response<CarBrandDTO>> GetByIdAsync(int id);
    }
}
