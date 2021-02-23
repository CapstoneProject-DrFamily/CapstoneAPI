using AutoMapper;
using Capstone_API_V2.Models;
using Capstone_API_V2.Repositories;
using Capstone_API_V2.UnitOfWork;
using Capstone_API_V2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Profile = Capstone_API_V2.Models.Profile;

namespace Capstone_API_V2.Services
{
    public class ProfileService : BaseService<Profile, ProfileModel>, IProfileService
    {
        public ProfileService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        protected override IGenericRepository<Profile> _repository => _unitOfWork.ProfileRepository;

        public override async Task<ProfileModel> CreateAsync(ProfileModel dto)
        {
            //Increment index when inserted
            dto.ProfileId = 0;

            var entity = _mapper.Map<Profile>(dto);
            _repository.Add(entity);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<ProfileModel>(entity);
        }
    }
}
