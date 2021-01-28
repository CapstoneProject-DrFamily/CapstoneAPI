using Capstone_API_V2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Capstone_API_V2.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> GetByUsername(string username);
        Task<User> Create(User user);
        void Delete(User user);
    }
}
