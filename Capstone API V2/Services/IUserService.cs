using Capstone_API_V2.Models;
using Capstone_API_V2.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Capstone_API_V2.Services
{
    public interface IUserService : IBaseService<Account, UserModel>
    {
        Task<IEnumerable<Account>> GetAllUsers();
        Task<Account> GetByUserName(string username, string action = "");
        Task<Account> CreateUser(UserModel user);
        Task<bool> CheckPassWord(string username, string password);
        Task<Account> UpdateUser(UserModel user);
        Task<bool> DeleteUser(int accountId);
        Task SendEmailAsync(string toEmail, string toName, bool waiting, bool disabled, string reason);
    }
}
