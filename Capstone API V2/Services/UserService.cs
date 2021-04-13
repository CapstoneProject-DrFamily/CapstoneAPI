using AutoMapper;
using Capstone_API_V2.Helper;
using Capstone_API_V2.Models;
using Capstone_API_V2.Repositories;
using Capstone_API_V2.UnitOfWork;
using Capstone_API_V2.ViewModels;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone_API_V2.Services
{
    public class UserService : BaseService<User, UserModel>, IUserService
    {

        public UserService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        protected override IGenericRepository<User> _repository => _unitOfWork.UserGenRepository;

        public async Task<bool> CheckPassWord(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return false;
            }

            var user = await _unitOfWork.UserRepository.GetByUsername(username);

            if (user != null && user.Disabled == false)
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
            //Increment index when inserted
            model.AccountId = 0; 

            var user = _mapper.Map<User>(model);
            user.InsBy = model.Username;
            user.InsDatetime = ConvertTimeZone();
            user.UpdBy = model.Username;
            user.UpdDatetime = ConvertTimeZone();
            await _unitOfWork.UserRepository.Create(user);
            await _unitOfWork.SaveAsync();
            return user;
        }

        public async Task<User> UpdateUser(UserModel model)
        {
            var entity = await _unitOfWork.UserGenRepository.GetById(model.AccountId);
            if (entity != null)
            {
                entity.Disabled = model.Disabled;
                entity.UpdBy = model.Username;
                entity.UpdDatetime = ConvertTimeZone();
                entity.Username = model.Username;
                entity.Password = model.Password;
                //entity.ProfileId = model.ProfileId;
                entity.Waiting = model.Waiting;
                entity.NotiToken = model.NotiToken;
                _unitOfWork.UserRepository.Update(entity);
                await _unitOfWork.SaveAsync(); 
                return entity;
            }
            return null;
        }

        public async Task<bool> DeleteUser(int accountId)
        {
            var entity = await _unitOfWork.UserGenRepository.GetById(accountId);
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
            if (entity == null || entity.Disabled == true)
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

        public async Task SendEmailAsync(string toEmail, string toName, bool waiting, bool disabled, string reason)
        {
            var sendGridClient = new SendGridClient(Constants.EmailConfig.API_KEY);

            var sendGridMessage = new SendGridMessage();
            sendGridMessage.SetFrom(Constants.EmailConfig.FROM_EMAIL, Constants.EmailConfig.FROM_NAME);
            sendGridMessage.AddTo(toEmail, toName);
            sendGridMessage.SetTemplateId(Constants.EmailConfig.ACCEPTED_TEMPLATE_ID);
            if (waiting == false && disabled == true)
            {
                sendGridMessage.SetTemplateId(Constants.EmailConfig.REJECTED_TEMPLATE_ID);
            }
            sendGridMessage.SetTemplateData(new EmailConfig
                {
                    Name = toName,
                    Reason = reason
                });

            await sendGridClient.SendEmailAsync(sendGridMessage);
        }

        private class EmailConfig
        {
            [JsonProperty("name")]
            public string Name { get; set; }
            [JsonProperty("reason")]
            public string Reason { get; set; }
        }
    }
}
