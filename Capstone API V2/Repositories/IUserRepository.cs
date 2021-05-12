using Capstone_API_V2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Capstone_API_V2.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<Account>> GetAll();
        Task<Account> GetByUsername(string username);
        Task<Account> Create(Account user);
        void Update(Account user);
        void Delete(Account user);
    }
}
