using Capstone_API_V2.Models;
using Capstone_API_V2.ViewModels;
using System.Threading.Tasks;

namespace Capstone_API_V2.Services
{
    public interface IAuthenticatedService
    {
        Task<Account> LoginOTP(LoginModel user);
    }
}
