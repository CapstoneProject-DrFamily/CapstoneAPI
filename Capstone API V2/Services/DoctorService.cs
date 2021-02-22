using AutoMapper;
using Capstone_API_V2.Helper;
using Capstone_API_V2.Models;
using Capstone_API_V2.Repositories;
using Capstone_API_V2.UnitOfWork;
using Capstone_API_V2.ViewModels;
using Capstone_API_V2.ViewModels.SimpleModel;
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

        public async Task<DoctorSimpModel> CreateDoctor(DoctorSimpModel dto)
        {
            //Increment index when inserted
            dto.DoctorId = 0;

            var entity = _mapper.Map<Doctor>(dto);
            entity.InsBy = Constants.Roles.ROLE_ADMIN;
            entity.InsDatetime = DateTime.Now;
            entity.UpdBy = Constants.Roles.ROLE_ADMIN;
            entity.UpdDatetime = DateTime.Now;
            _repository.Add(entity);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<DoctorSimpModel>(entity);
        }

        public async Task<DoctorSimpModel> UpdateDoctor(DoctorSimpModel dto)
        {
            var entity = await _unitOfWork.DoctorRepository.GetById(dto.DoctorId);
            if (entity != null)
            {
                entity.Description = dto.Description;
                entity.Degree = dto.Degree;
                entity.Experience = dto.Experience;
                entity.SpecialtyId = dto.SpecialtyId;
                entity.ProfileId = dto.ProfileId;
                entity.School = dto.School;
                entity.UpdBy = Constants.Roles.ROLE_ADMIN;
                entity.UpdDatetime = DateTime.Now;
                _unitOfWork.DoctorRepository.Update(entity);
                await _unitOfWork.SaveAsync();
                return dto;
            }
            return null;
        }

        public override async Task<bool> DeleteAsync(object id)
        {
            var doctor = _unitOfWork.DoctorRepositorySep.GetDoctorByID((int) id).Result;
            if (doctor == null || doctor.Profile.Users.SingleOrDefault().Disabled == true) throw new Exception("Not found doctor with id: " + id);
            doctor.Profile.Users.SingleOrDefault().Disabled = true;
            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<DoctorRequestModel> GetRequestDoctorInfo(int profileID)
        {
            var users = await _unitOfWork.DoctorRepositorySep.GetRequestDoctorInfo(profileID);
            return users;
        }

        public async Task<List<DoctorModel>> GetAllDoctor()
        {
            var doctors = await _unitOfWork.DoctorRepositorySep.GetAllDoctor();
            return _mapper.Map<List<DoctorModel>>(doctors);
        }

        public async Task<DoctorModel> GetDoctorByID(int doctorId)
        {
            var doctor = await _unitOfWork.DoctorRepositorySep.GetDoctorByID(doctorId);
            return _mapper.Map<DoctorModel>(doctor);
        }

        public async Task<List<DoctorModel>> GetDoctorByName(string fullname)
        {
            var doctors = await _unitOfWork.DoctorRepositorySep.GetDoctorByName(fullname);
            return _mapper.Map<List<DoctorModel>>(doctors);
        }
    }
}
