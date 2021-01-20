using Capstone_API_V2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Capstone_API_V2.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetByUserName(string username, string action = "");
        Task<User> CreateUser(string username, string password);
        Task<bool> CheckPassWord(string username, string password);
        //Task<bool> DeleteUser(string username);
    }
}
