﻿using AutoMapper;
using Capstone_API_V2.Helper;
using Capstone_API_V2.Models;
using Capstone_API_V2.Repositories;
using Capstone_API_V2.UnitOfWork;
using Capstone_API_V2.ViewModels;
using Capstone_API_V2.ViewModels.SimpleModel;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone_API_V2.Services
{
    public class ScheduleService : BaseService<Schedule, ScheduleModel>, IScheduleService
    {
        public ScheduleService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        protected override IGenericRepository<Schedule> _repository => _unitOfWork.ScheduleRepository;

        public async Task<List<ScheduleSimpModel>> CreateScheduleAsync(List<ScheduleSimpModel> dto)
        {
            var appConfigModel = await _unitOfWork.AppConfigRepository.GetAll(filter: f => f.AppId == Constants.Roles.DOCTOR_APP_ID).SingleOrDefaultAsync();
            var jObject = JsonConvert.DeserializeObject<JObject>(appConfigModel.ConfigValue);
            var examinationHour = jObject.GetValue("examinationHour").ToObject<double>();

            List<ScheduleSimpModel> lstInvalidSchedule = new List<ScheduleSimpModel>(); 
            foreach(var schedule in dto)
            {
                if (!_unitOfWork.ScheduleRepositorySep.checkInvalidSchedule(schedule, examinationHour))
                {
                    //Auto increment index
                    schedule.ScheduleId = 0;

                    var entity = _mapper.Map<Schedule>(schedule);
                    entity.Disabled = false;
                    entity.InsBy = schedule.InsBy;
                    entity.InsDatetime = ConvertTimeZone();
                    entity.UpdDatetime = ConvertTimeZone();

                    schedule.Disabled = false;
                    schedule.InsDatetime = entity.InsDatetime;
                    schedule.UpdDatetime = entity.UpdDatetime;

                    _repository.Add(entity);
                }
                else
                {
                    lstInvalidSchedule.Add(schedule);
                }
            }
            await _unitOfWork.SaveAsync();

            return lstInvalidSchedule;
        }

        public async Task<ScheduleSimpModel> UpdateScheduleAsync(ScheduleSimpModel dto)
        {
            var entity = await _unitOfWork.ScheduleRepository.GetById(dto.ScheduleId);
            if (entity != null)
            {
                entity.AppointmentTime = dto.AppointmentTime;
                entity.Status = dto.Status;
                entity.UpdBy = dto.UpdBy;
                entity.UpdDatetime = dto.UpdDatetime;
                _repository.Update(entity);
                await _unitOfWork.SaveAsync();
                return dto;
            }
            return null;
        }

        public override async Task<bool> DeleteAsync(object id)
        {
            var entity = await _repository.GetById(id);

            if (entity == null) throw new Exception("Not found schedule object with id: " + id);

            entity.Disabled = true;

            return await _unitOfWork.SaveAsync() > 0;
        }

        public bool checkIsOldPatient(int doctorId, int patientId)
        {
            return _unitOfWork.TransactionRepositorySep.CheckOldPatient(patientId, doctorId);
        }

        public Task<CheckingSchedule> isCheckingTransaction(int doctorId)
        {
            CheckingSchedule checkingSchedule = new CheckingSchedule();
            checkingSchedule.isCheckingStatus =  _unitOfWork.TransactionRepository.GetAll(filter: f => (f.DoctorId == doctorId && f.Status == Constants.TransactionStatus.ONGOING) || (f.DoctorId == doctorId && f.Status == Constants.TransactionStatus.CHECKING)).Any();
            var schedule = _unitOfWork.ScheduleRepository.GetAll(filter: f => f.DoctorId == doctorId && f.Status == true && ConvertTimeZone() < f.AppointmentTime, includeProperties: "Treatments").OrderBy(o => o.AppointmentTime).FirstOrDefault();
            if(schedule != null)
            {
                checkingSchedule.isOvertime = false;
                if (!schedule.Treatments.Any(t => Constants.TransactionStatus.DONE == t.Status || Constants.TransactionStatus.AWAIT_PAYMENT == t.Status))
                {
                    TimeSpan ts = schedule.AppointmentTime.GetValueOrDefault(ConvertTimeZone()) - ConvertTimeZone();
                    checkingSchedule.isOvertime = ts.TotalMinutes <= 30;
                }
            }

            return Task.FromResult(checkingSchedule);
        }

        public string GetPhoneNumber(int? doctorId)
        {
            var accountId = _unitOfWork.ProfileRepository.GetById(doctorId).Result.AccountId;
            var username = _unitOfWork.UserGenRepository.GetById(accountId).Result.Username;
            var phoneNumber = "0" + username.Substring(2);
            return phoneNumber;
        }
    }
}
