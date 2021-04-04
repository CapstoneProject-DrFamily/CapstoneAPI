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
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Capstone_API_V2.Services
{
    public class DoctorService : BaseService<Doctor, DoctorModel>, IDoctorService
    {
        private double? ratingPoint;
        private int bookedCount, feedbackCount;

        public DoctorService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        protected override IGenericRepository<Doctor> _repository => _unitOfWork.DoctorRepository;

        public async Task<PaginatedList<DoctorModel>> GetBySpecialtyAsync(int specialtyId, ResourceParameter model)
        {
            //var entity = await _repository.Get(pageIndex, pageSize, filter, orderBy, includeProperties);

            var lstDoctor = await _unitOfWork.DoctorRepositorySep.GetBySpecialtyId(specialtyId);
            var doctors = _mapper.Map<List<DoctorModel>>(lstDoctor);
            foreach (var doctor in doctors)
            {
                var schedules = doctor.Schedules;
                var querySchedules = from schedule in schedules where schedule.Disabled == false && schedule.AppointmentTime >= ConvertTimeZone() && schedule.Status == false select schedule;
                doctor.Schedules = querySchedules.OrderBy(o => o.AppointmentTime).ToList();

                /*var ratingPoint = (from feedback in doctor.Feedbacks where doctor.Feedbacks.Count != 0 select feedback.RatingPoint).Average();
                var bookedCount = (from transaction in doctor.Transactions where doctor.Transactions.Count != 0 && transaction.Status == Constants.TransactionStatus.DONE && transaction.Disabled == false select transaction).Count();*/
                CalculateRatingDoctor(doctorId: doctor.DoctorId);

                doctor.RatingPoint = ratingPoint;
                doctor.BookedCount = bookedCount;
                doctor.FeedbackCount = feedbackCount;
            }
            return PaginatedList<DoctorModel>.GetPage(doctors, model.PageIndex, model.PageSize).Result;
            //return await PaginatedList<DoctorModel>.CreateAsync(doctors, model.PageIndex, model.PageSize); ;
        }

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

        private void CalculateRatingDoctor(int doctorId)
        {
            var feedbacks = _unitOfWork.FeedbackRepository.GetAll(filter: f => f.DoctorId == doctorId);
            var avgRatingPoint = (from feedback in feedbacks where feedbacks.Count() > 0 select feedback.RatingPoint).Average();
            var transactions = _unitOfWork.TransactionRepository.GetAll(filter: f => f.DoctorId == doctorId && f.Disabled == false && f.Status == Constants.TransactionStatus.DONE);
            
            ratingPoint = avgRatingPoint;
            bookedCount = transactions.Count();
            feedbackCount = feedbacks.Count();
        }
    }
}
