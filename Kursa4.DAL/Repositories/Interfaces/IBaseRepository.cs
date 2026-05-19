using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursa4.DAL.Repositories.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<T> CreateAsync(T entity);

        Task<T> UpdateAsync(T entity);

        Task<T> GetByIdAsync(int id);

        Task<List<T>> GetAllAsync();
    }
}
