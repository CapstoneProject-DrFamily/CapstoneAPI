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
            var entity = _mapper.Map<Doctor>(dto);
            entity.Disabled = false;
            entity.InsBy = Constants.Roles.ROLE_ADMIN;
            entity.InsDatetime = ConvertTimeZone();
            entity.UpdBy = Constants.Roles.ROLE_ADMIN;
            entity.UpdDatetime = ConvertTimeZone();
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
                //entity.ProfileId = dto.ProfileId;
                entity.School = dto.School;
                entity.UpdBy = Constants.Roles.ROLE_ADMIN;
                entity.UpdDatetime = ConvertTimeZone();
                _unitOfWork.DoctorRepository.Update(entity);
                await _unitOfWork.SaveAsync();
                return dto;
            }
            return null;
        }

        public override async Task<bool> DeleteAsync(object id)
        {
            var doctor = await _unitOfWork.DoctorRepositorySep.GetDoctorByID((int) id);
            if (doctor == null || doctor.DoctorNavigation.Account.Disabled == true) throw new Exception("Not found doctor with id: " + id);
            doctor.DoctorNavigation.Account.Disabled = true;
            doctor.Disabled = true;
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
            if(doctor != null)
            {
                var ratingPoint = (from feedback in doctor.Feedbacks where doctor.Feedbacks.Count != 0 select feedback.RatingPoint).Average();
                var bookedCount = (from transaction in doctor.Transactions where doctor.Transactions.Count != 0 && transaction.Status == Constants.TransactionStatus.DONE && transaction.Disabled == false select transaction).Count();
                var result = _mapper.Map<DoctorModel>(doctor);
                result.RatingPoint = ratingPoint;
                result.BookedCount = bookedCount;
                //result.TotalDoneTransaction = totalDoneTransaction;

                return result;
            }
            return null;
        }

        public async Task<List<DoctorModel>> GetDoctorByName(string fullname)
        {
            var doctors = await _unitOfWork.DoctorRepositorySep.GetDoctorByName(fullname);
            return _mapper.Map<List<DoctorModel>>(doctors);
        }

        public async Task<List<DoctorModel>> GetWaitingDoctor()
        {
            var doctors = await _unitOfWork.DoctorRepositorySep.GetWaitingDoctor();
            return _mapper.Map<List<DoctorModel>>(doctors);
        }
    }
}
