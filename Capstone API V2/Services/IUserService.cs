using Capstone_API_V2.Models;
using Capstone_API_V2.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Capstone_API_V2.Services
{
    public interface IUserService : IBaseService<User, UserModel>
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetByUserName(string username, string action = "");
        Task<User> CreateUser(UserModel user);
        Task<bool> CheckPassWord(string username, string password);
        Task<User> UpdateUser(UserModel user);
        Task<bool> DeleteUser(int accountId);
    }
}
