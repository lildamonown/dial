using Kursa4.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursa4.BLL.Services.Interfaces
{
    public interface IBaseService<T>
    {
        Task<Response<T>> CreateAsync(T item);

        Task<Response<T>> UpdateAsync(T item);

        Task<Response<T>> GetByIdAsync(int id);

        Task<Response<List<T>>> GetAllAsync();
    }
}
