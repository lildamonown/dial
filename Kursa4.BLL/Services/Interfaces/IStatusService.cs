using Kursa4.BLL.DTO;
using Kursa4.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursa4.BLL.Services.Interfaces
{
    public interface IStatusService : IBaseService<StatusDTO>
    {
        Task<Response<StatusDTO>> GetByNameAsync(string name);
    }
}
