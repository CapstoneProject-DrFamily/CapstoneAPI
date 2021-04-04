﻿using Capstone_API_V2.Helper;
using Capstone_API_V2.Models;
using Capstone_API_V2.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone_API_V2.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly FamilyDoctorContext _context;

        public DoctorRepository(FamilyDoctorContext context)
        {
            _context = context;
        }

        public async Task<DoctorRequestModel> GetRequestDoctorInfo(int profileID)
        {
            var doctorInfo = await _context.Doctors.Where(x => x.DoctorNavigation.ProfileId == profileID && x.Disabled == false).Include(x => x.Transactions).Include(x => x.Feedbacks)
                                       .Select(x => new DoctorRequestModel
                                       {
                                           DoctorId = x.DoctorId,
                                           DoctorImage = x.DoctorNavigation.Image,
                                           DoctorName = x.DoctorNavigation.FullName,
                                           DoctorSpecialty = x.Specialty.Name,
                                           DoctorServiceId = x.SpecialtyId,
                                           RatingPoint = (from feedback in x.Feedbacks where x.Feedbacks.Count != 0 select feedback.RatingPoint).Average(),
                                           BookedCount = (from transaction in x.Transactions where x.Transactions.Count != 0 && transaction.Status == Constants.TransactionStatus.DONE && transaction.Disabled == false select transaction).Count(),
                                           FeedbackCount = x.Feedbacks.Count()
                                       }).SingleOrDefaultAsync();
            return doctorInfo;
        }

        public async Task<List<Doctor>> GetAllDoctor()
        {
            var result = await _context.Doctors
                .Include(specialty => specialty.Specialty)
                .Include(profile => profile.DoctorNavigation)
                .ThenInclude(user => user.Account)
                .Where(user => user.DoctorNavigation.Account.Disabled == false && user.Disabled == false)
                .ToListAsync();

            return result;
        }

        public async Task<Doctor> GetDoctorByID(int doctorId)
        {
            var result = await _context.Doctors.Where(doctor => doctor.DoctorId.Equals(doctorId))
                .Include(specialty => specialty.Specialty)
                .Include(feedback => feedback.Feedbacks)
                .Include(transaction => transaction.Transactions)
                .Include(profile => profile.DoctorNavigation)
                .ThenInclude(user => user.Account)
                .Where(user => user.DoctorNavigation.Account.Disabled == false && user.Disabled == false)
                .SingleOrDefaultAsync();

            return result;
        }

        public async Task<List<Doctor>> GetDoctorByName(string fullname)
        {
            var result =  await _context.Doctors
                .Include(specialty => specialty.Specialty)
                .Include(profile => profile.DoctorNavigation)
                .ThenInclude(user => user.Account)
                .Where(t => t.DoctorNavigation.FullName.Contains(fullname) && t.DoctorNavigation.Account.Disabled == false && t.Disabled == false)
                .ToListAsync();

            return result;
        }

        public async Task<List<Doctor>> GetWaitingDoctor()
        {
            var result = await _context.Doctors
                .Include(specialty => specialty.Specialty)
                .Include(profile => profile.DoctorNavigation)
                .ThenInclude(user => user.Account)
                .Where(user => user.DoctorNavigation.Account.Disabled == false && user.Disabled == false && user.DoctorNavigation.Account.Waiting == true)
                .ToListAsync();

            return result;
        }

        public Task<List<Doctor>> GetBySpecialtyId(int specialtyId)
        {
            var result =  _context.Doctors.Where(doctor => doctor.SpecialtyId.Equals(specialtyId))
                .Include(specialty => specialty.Specialty)
                .Include(schedule => schedule.Schedules)
                .Include(profile => profile.DoctorNavigation)
                .ThenInclude(user => user.Account)
                .Where(user => user.DoctorNavigation.Account.Disabled == false && user.Disabled == false && user.Schedules.Count > 0)
                .OrderBy(s => s.Schedules.SingleOrDefault().AppointmentTime).ToListAsync();

            return result;
        }
    }
}
