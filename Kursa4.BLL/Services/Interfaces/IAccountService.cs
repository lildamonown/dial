using Kursa4.BLL.DTO;
using Kursa4.BLL.Models;
using Kursa4.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursa4.BLL.Services.Interfaces
{
    public interface IAccountService
    {
        Task<Response<bool>> RegisterAsync(User user, string password);

        Task<Response<bool>> LoginAsync(string email, string password);

        Task<Response<bool>> LogoutAsync();

        Task<Response<User>> GetById(string id);

        Task<Response<bool>> ChangeUserRoleAsync(string userId, string newRole);

        Task<Response<IEnumerable<User>>> GetAllAsync();

        Task<Response<bool>> UpdateUserAsync(string userId, string name, string surname, string phoneNumber);

        Task<Response<bool>> DeleteUserAsync(string userId);
    }
}
