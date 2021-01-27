﻿using System;
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

        public async Task<UserModel> LoginOTP(LoginModel user)
        {
            //UserRecord user_firebase = await FirebaseAuth.DefaultInstance.GetUserAsync(uid);
            int role_id = int.Parse(user.RoleID);

            var currentUser = await _uow.UserRepository.GetByUsername(user.PhoneNumber);

            if (currentUser == null)
            {
                var user_info = new User()
                {
                    Username = user.PhoneNumber,
                    RoleId = Constants.Roles.ROLE_PATIENT_ID,
                    Disabled = false
                    /*Photo = user_firebase.PhotoUrl,
                    InsBy = Constants.Roles.ROLE_ADMIN,
                    InsDatetime = DateTime.Now,
                    UpdBy = Constants.Roles.ROLE_ADMIN,
                    UpdDatetime = DateTime.Now*/
                };

                await _uow.UserRepository.Create(user_info, "123");

                if (await _uow.SaveAsync() > 0)
                {
                    return _mapper.Map<UserModel>(user_info);
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
            return _mapper.Map<UserModel>(currentUser);
        }
    }
}