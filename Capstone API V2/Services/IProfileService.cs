using Capstone_API_V2.Models;
using Capstone_API_V2.ViewModels;
using Capstone_API_V2.ViewModels.SimpleModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone_API_V2.Services
{
    public interface IProfileService : IBaseService<Profile, ProfileModel>
    {
        Task<ProfileSimpModel> CreateProfile(ProfileSimpModel dto);
        Task<ProfileSimpModel> UpdateProfile(ProfileSimpModel dto);
    }
}
