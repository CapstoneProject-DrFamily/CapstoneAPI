using AutoMapper;
using Capstone_API_V2.Models;
using Capstone_API_V2.Repositories;
using Capstone_API_V2.UnitOfWork;
using Capstone_API_V2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone_API_V2.Services
{
    public class DoctorService : BaseService<Doctor, DoctorModel>, IDoctorService
    {
        public DoctorService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        protected override IGenericRepository<Doctor> _repository => _unitOfWork.DoctorRepository;

        public override async Task<DoctorModel> CreateAsync(DoctorModel dto)
        {
            var entity = _mapper.Map<Doctor>(dto);
            /*entity.InsBy = Constants.Roles.ROLE_ADMIN;
            entity.InsDatetime = DateTime.Now;
            entity.UpdBy = Constants.Roles.ROLE_ADMIN;
            entity.UpdDatetime = DateTime.Now;*/

            _repository.Add(entity);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<DoctorModel>(entity);
        }

        public async Task<DoctorRequestModel> GetRequestDoctorInfo(int profileID)
        {
            var users = await _unitOfWork.DoctorRepositorySep.GetRequestDoctorInfo(profileID);
            return users;
        }
    }
}
