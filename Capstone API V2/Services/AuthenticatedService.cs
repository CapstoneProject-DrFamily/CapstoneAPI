using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Capstone_API_V2.Helper;
using Capstone_API_V2.Models;
using Capstone_API_V2.UnitOfWork;
using Capstone_API_V2.ViewModels;
using FirebaseAdmin.Auth;

namespace Capstone_API_V2.Services
{
    public class AuthenticatedService : IAuthenticatedService
    {
        private IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public AuthenticatedService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Account> LoginOTP(LoginModel user)
        {
            //UserRecord user_firebase = await FirebaseAuth.DefaultInstance.GetUserAsync(uid);
            int role_id = int.Parse(user.RoleID);

            var currentUser = await _uow.UserRepository.GetByUsername(user.PhoneNumber);

            if (currentUser == null)
            {
                var user_info = new Account()
                {
                    Username = user.PhoneNumber,
                    RoleId = role_id,
                    Disabled = false,
                    Waiting = true,
                    //Photo = user_firebase.PhotoUrl,
                    InsBy = user.PhoneNumber,
                    InsDatetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(),
                    TimeZoneInfo.FindSystemTimeZoneById(Constants.Format.VN_TIMEZONE_ID)),
                    UpdBy = user.PhoneNumber,
                    UpdDatetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(),
                    TimeZoneInfo.FindSystemTimeZoneById(Constants.Format.VN_TIMEZONE_ID))
                };

                await _uow.UserRepository.Create(user_info);

                if (await _uow.SaveAsync() > 0)
                {
                    return user_info;
                }
                else
                {
                    throw new Exception("Create new account failed");
                }
            }
            else
            {
                if(currentUser.Disabled || currentUser.RoleId != role_id)
                {
                    return null;
                }
            }
            return currentUser;
        }

        public int GetUserProfile(int roleId, int accountId)
        {
            if (roleId == Constants.Roles.ROLE_DOCTOR_ID)
            {
                return _uow.DoctorRepository.GetById(accountId).Result != null ? _uow.DoctorRepository.GetById(accountId).Result.Id : 0;
            }
            return _uow.PatientRepository.GetAll(filter: f => f.AccountId == accountId && Constants.Relationship.OWNER.Equals(f.Relationship)).SingleOrDefault() != null ? 
                _uow.PatientRepository.GetAll(filter: f => f.AccountId == accountId && Constants.Relationship.OWNER.Equals(f.Relationship)).SingleOrDefault().Id : 0;
        }
    }
}
