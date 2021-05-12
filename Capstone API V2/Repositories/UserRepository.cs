using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone_API_V2.Helper;
using Capstone_API_V2.Models;
using Microsoft.EntityFrameworkCore;

namespace Capstone_API_V2.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FamilyDoctorContext _context;

        public UserRepository(FamilyDoctorContext context)
        {
            _context = context;
        }

        public async Task<Account> Create(Account user)
        {
            /*if (string.IsNullOrWhiteSpace(password))
            {
                throw new AppException("Password is required");
            }*/


            if (_context.Accounts.Any(x => x.Username == user.Username))
            {
                throw new AppException("Username \"" + user.Username + "\" is duplicate");
            }

            /*byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);*/

            /*user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            user.InsBy = Constants.Roles.ROLE_ADMIN;
            user.InsDatetime = DateTime.Now;
            user.UpdBy = Constants.Roles.ROLE_ADMIN;
            user.UpdDatetime = DateTime.Now;*/

            await _context.Accounts.AddAsync(user);
            return user;

        }

        public void Update(Account user)
        {
            _context.Accounts.Attach(user);
            _context.Accounts.Update(user);
        }

        public void Delete(Account user)
        {
            user.Disabled = true;
        }

        public async Task<IEnumerable<Account>> GetAll()
        {
            return await _context.Accounts.ToListAsync();
        }

        public async Task<Account> GetByUsername(string username)
        {
            var user = await _context.Accounts.Where(x => x.Username == username).Include(x => x.Patients)
                                       .SingleOrDefaultAsync();
            if(user.Patients.Count == 0)
            {
                user = await _context.Accounts.Where(x => x.Username == username).Include(x => x.Doctor)
                                       .SingleOrDefaultAsync();
            }
            return user;
        }

        /*private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }*/
    }
}
