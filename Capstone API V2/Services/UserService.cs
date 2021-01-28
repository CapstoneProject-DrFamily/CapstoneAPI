using AutoMapper;
using Capstone_API_V2.Helper;
using Capstone_API_V2.Models;
using Capstone_API_V2.UnitOfWork;
using Capstone_API_V2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone_API_V2.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<bool> CheckPassWord(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return false;
            }

            var user = await _unitOfWork.UserRepository.GetByUsername(username);

            if (user != null)
            {
                /*if (VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                {
                    return true;
                }*/
                if (password == null) throw new ArgumentNullException("password");
                if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
                if (user.Password.Equals(password)) return true;
            }

            return false;
        }

        public async Task<User> CreateUser(UserModel model)
        {
            var user = _mapper.Map<User>(model);
            await _unitOfWork.UserRepository.Create(user);
            await _unitOfWork.SaveAsync();
            return user;
        }

        public async Task<bool> DeleteUser(string username)
        {
            var entity = await _unitOfWork.UserRepository.GetByUsername(username);
            if (entity != null && entity.Disabled == false)
            {
                if (entity.RoleId == Constants.Roles.ROLE_DOCTOR_ID || entity.RoleId == Constants.Roles.ROLE_PATIENT_ID)
                {
                    _unitOfWork.UserRepository.Delete(entity);
                    await _unitOfWork.SaveAsync();
                    return true;
                }
            }
            return false;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var users = await _unitOfWork.UserRepository.GetAll();
            return users;
        }

        public async Task<User> GetByUserName(string username, string action)
        {
            var entity = await _unitOfWork.UserRepository.GetByUsername(username);
            if (entity == null)
            {
                if (action == "Login")
                {
                    return null;
                }
                throw new AppException("Cannot find " + username);
            }
            return entity;
        }


        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }

    }
}
