using AutoMapper;
using Capstone_API_V2.Helper;
using Capstone_API_V2.Models;
using Capstone_API_V2.Repositories;
using Capstone_API_V2.UnitOfWork;
using Capstone_API_V2.ViewModels;
using Capstone_API_V2.ViewModels.SimpleModel;
using Microsoft.EntityFrameworkCore;
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
                CalculateRatingDoctor(doctorId: doctor.Id);

                doctor.RatingPoint = ratingPoint;
                doctor.BookedCount = bookedCount;
                doctor.FeedbackCount = feedbackCount;
            }
            return PaginatedList<DoctorModel>.GetPage(doctors, model.PageIndex, model.PageSize).Result;
            //return await PaginatedList<DoctorModel>.CreateAsync(doctors, model.PageIndex, model.PageSize); ;
        }

        /*public async Task<PaginatedList<DoctorModel>> GetOldDoctorForAppointment(int accountId, ResourceParameter model)
        {
            var specialtyId = int.Parse(model.SearchValue);
            var lstPatientId = _unitOfWork.PatientRepository.GetAll(f => f.AccountId == accountId).Select(p => p.Id).ToList();

            if (specialtyId != -1)
            {
                var lstDoctor = await _unitOfWork.DoctorRepositorySep.GetBySpecialtyId(specialtyId);
                var doctorModels = _mapper.Map<List<DoctorModel>>(lstDoctor);
                foreach (var doctor in doctorModels)
                {
                    var schedules = doctor.Schedules;
                    var querySchedules = from schedule in schedules where schedule.Disabled == false && schedule.AppointmentTime >= ConvertTimeZone() && schedule.Status == false select schedule;
                    doctor.Schedules = querySchedules.OrderBy(o => o.AppointmentTime).ToList();

                    CalculateRatingDoctor(doctorId: doctor.Id);

                    doctor.RatingPoint = ratingPoint;
                    doctor.BookedCount = bookedCount;
                    doctor.FeedbackCount = feedbackCount;
                }
                return PaginatedList<DoctorModel>.GetPage(doctorModels, model.PageIndex, model.PageSize).Result;
            }

            List<DoctorModel> doctors = new List<DoctorModel>();
            foreach (var patientId in lstPatientId)
            {
                var entity = await _unitOfWork.TransactionRepository.GetAll(f => f.PatientId == patientId && f.Status == Constants.TransactionStatus.DONE).Select(d => d.DoctorId).ToListAsync();
                foreach (var doctorId in entity)
                {
                    doctors = _mapper.Map<List<DoctorModel>>(_unitOfWork.DoctorRepository.GetAll(f => f.Id == doctorId).ToList());
                }
            }
            return PaginatedList<DoctorModel>.GetPage(doctors, model.PageIndex, model.PageSize).Result;
        }*/

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
            var entity = await _unitOfWork.DoctorRepository.GetById(dto.Id);
            if (entity != null)
            {
                entity.Birthday = dto.Birthday;
                entity.Email = dto.Email;
                entity.Fullname = dto.Fullname;
                entity.Gender = dto.Gender;
                entity.IdCard = dto.IdCard;
                entity.Image = dto.Image;
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
                return _mapper.Map<DoctorSimpModel>(await _unitOfWork.DoctorRepository.GetById(dto.Id));
            }
            return null;
        }

        public override async Task<bool> DeleteAsync(object id)
        {
            var doctor = await _unitOfWork.DoctorRepositorySep.GetDoctorByID((int) id);
            if (doctor == null || doctor.IdNavigation.Disabled == true) throw new Exception("Not found doctor with id: " + id);
            doctor.IdNavigation.Disabled = true;
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
                CalculateRatingDoctor(doctor.Id);
                //var lstRatingPoint = (from treatment in doctor.Treatments where doctor.Treatments.Count != 0 && treatment.Feedback != null select treatment.Feedback.RatingPoint).Average();
                var bookedCount = (from transaction in doctor.Treatments where doctor.Treatments.Count != 0 && transaction.Status == Constants.TransactionStatus.DONE && transaction.Disabled == false select transaction).Count();
                var result = _mapper.Map<DoctorModel>(doctor);
                result.RatingPoint = ratingPoint;
                result.BookedCount = bookedCount;

                return result;
            }
            return null;
        }

        public async Task<List<DoctorModel>> GetDoctorByName(string fullname)
        {
            var doctors = await _unitOfWork.DoctorRepositorySep.GetDoctorByName(fullname);
            return _mapper.Map<List<DoctorModel>>(doctors);
        }

        public async Task<List<Account>> GetWaitingDoctor()
        {
            var doctors = await _unitOfWork.DoctorRepositorySep.GetWaitingDoctor();
            List<Account> result = new List<Account>();
            if(doctors.Count > 0)
            {
                foreach (var d in doctors)
                {
                    result.Add(d.IdNavigation);
                }
                return result;
            }
            return null;
            //return _mapper.Map<List<DoctorModel>>(doctors);
        }

        public async Task<List<DoctorModel>> GetOldDoctor(int patientId)
        {
            var entity = await _unitOfWork.TransactionRepository.GetAll(f => f.PatientId == patientId && f.Status == Constants.TransactionStatus.DONE).Select(d => d.DoctorId).ToListAsync();
            List<DoctorModel> lstDoctor = new List<DoctorModel>();
            foreach(var doctorId in entity)
            {
                var doctorModel = _mapper.Map<DoctorModel>(_unitOfWork.DoctorRepository.GetAll(f => f.Id == doctorId && f.Disabled == false, includeProperties: "Specialty,IdNavigation,Schedules").SingleOrDefault());
                lstDoctor.Add(doctorModel);
            }
            return lstDoctor;
        }

        public async Task<List<DoctorRequestModel>> GetOldDoctorForRealtime(int accountId, int specialtyId)
        {
            var lstPatientId = _unitOfWork.PatientRepository.GetAll(f => f.AccountId == accountId).Select(p => p.Id).ToList();
            List<DoctorRequestModel> lstDoctor = new List<DoctorRequestModel>();
            foreach (var patientId in lstPatientId)
            {
                var entity = await _unitOfWork.TransactionRepository.GetAll(f => f.PatientId == patientId && f.Status == Constants.TransactionStatus.DONE).Select(d => d.DoctorId).ToListAsync();
                foreach (var doctorId in entity)
                {
                    if(specialtyId == -1)
                    {
                        var doctorModel = await _unitOfWork.DoctorRepositorySep.GetRequestDoctorInfo(doctorId.GetValueOrDefault());
                        lstDoctor.Add(doctorModel);
                    }
                    else
                    {
                        var doctorModel = await _unitOfWork.DoctorRepositorySep.GetRequestDoctorInfo(doctorId.GetValueOrDefault());
                        if (doctorModel.DoctorServiceId == specialtyId)
                        {
                            lstDoctor.Add(doctorModel);
                        }
                    }
                }
            }
            return lstDoctor;
        }

        private void CalculateRatingDoctor(int doctorId)
        {
            var feedbacks = _unitOfWork.FeedbackRepository.GetAll(filter: f => f.IdNavigation.DoctorId == doctorId);
            var lstRatingPoint = (from feedback in feedbacks where feedbacks.Count() > 0 select feedback.RatingPoint).ToList();
            lstRatingPoint.Add(5);
            var avgRatingPoint = lstRatingPoint.Average();
            var transactions = _unitOfWork.TransactionRepository.GetAll(filter: f => f.DoctorId == doctorId && f.Disabled == false && f.Status == Constants.TransactionStatus.DONE);
            
            ratingPoint = avgRatingPoint;
            bookedCount = transactions.Count();
            feedbackCount = feedbacks.Count();
        }
    }
}
