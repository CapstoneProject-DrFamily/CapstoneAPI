using Capstone_API_V2.Helper;
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

        public async Task<DoctorRequestModel> GetRequestDoctorInfo(int doctorId)
        {
            var feedbacks = _context.Feedbacks.Where(f => f.IdNavigation.DoctorId == doctorId);
            var lstRatingPoint = (from feedback in feedbacks where feedbacks.Count() > 0 select feedback.RatingPoint).ToList();
            lstRatingPoint.Add(5);
            var avgRatingPoint = lstRatingPoint.Average();

            var doctorInfo = await _context.Doctors.Where(x => x.Id == doctorId && x.Disabled == false).Include(x => x.Treatments)
                                       .Select(x => new DoctorRequestModel
                                       {
                                           DoctorId = x.Id,
                                           DoctorImage = x.Image,
                                           DoctorName = x.Fullname,
                                           DoctorSpecialty = x.Specialty.Name,
                                           DoctorServiceId = x.SpecialtyId,
                                           //RatingPoint = (from treatment in x.Treatments where x.Treatments.Count != 0 && treatment.Feedback != null select treatment.Feedback.RatingPoint).Average(),
                                           RatingPoint = avgRatingPoint,
                                           BookedCount = (from transaction in x.Treatments where x.Treatments.Count != 0 && transaction.Status == Constants.TransactionStatus.DONE && transaction.Disabled == false select transaction).Count(),
                                           //FeedbackCount = (from treatment in x.Treatments where x.Treatments.Count != 0 && treatment.Feedback != null select treatment.Feedback).Count()
                                           FeedbackCount = feedbacks.Count()
                                       }).SingleOrDefaultAsync();
            if(doctorInfo.RatingPoint == null)
            {
                //doctorInfo.RatingPoint = 0;
                doctorInfo.RatingPoint = 5;
            }
            return doctorInfo;
        }

        public async Task<List<Doctor>> GetAllDoctor()
        {
            var result = await _context.Doctors
                .Include(specialty => specialty.Specialty)
                .Include(s => s.IdNavigation)
                .Where(user => user.IdNavigation.Disabled == false && user.Disabled == false)
                .ToListAsync();

            return result;
        }

        public async Task<Doctor> GetDoctorByID(int doctorId)
        {
            var result = await _context.Doctors.Where(doctor => doctor.Id == doctorId)
                .Include(specialty => specialty.Specialty)
                .Include(s => s.IdNavigation)
                .Where(user => user.IdNavigation.Disabled == false && user.Disabled == false)
                .SingleOrDefaultAsync();

            var transactions = await _context.Treatments.Where(transaction => transaction.DoctorId == result.Id).Include(x => x.Feedback).ToListAsync();
            //var feedbacks = await _context.Feedbacks.Where(feedback => feedback.IdNavigation.DoctorId == result.Id).ToListAsync();

            result.Treatments = transactions;

            return result;
        }

        public async Task<List<Doctor>> GetDoctorByName(string fullname)
        {
            var result =  await _context.Doctors
                .Include(specialty => specialty.Specialty)
                .Include(s => s.IdNavigation)
                .Where(t => t.Fullname.Contains(fullname) && t.IdNavigation.Disabled == false && t.Disabled == false)
                .ToListAsync();

            return result;
        }

        public async Task<List<Doctor>> GetWaitingDoctor()
        {
            var result = await _context.Doctors
                .Include(specialty => specialty.Specialty)
                .Include(s => s.IdNavigation)
                .Where(user => user.IdNavigation.Disabled == false && user.Disabled == false && user.IdNavigation.Waiting == true)
                .ToListAsync();

            return result;
        }

        public Task<List<Doctor>> GetBySpecialtyId(int specialtyId)
        {
            if(specialtyId == -1)
            {
                var lstAllDoctor = _context.Doctors
                .Include(specialty => specialty.Specialty)
                .Include(schedule => schedule.Schedules)
                .Include(s => s.IdNavigation)
                .Where(user => user.IdNavigation.Disabled == false && user.Disabled == false && user.Schedules.Count > 0)
                .OrderBy(s => s.Schedules.SingleOrDefault().AppointmentTime).ToListAsync();

                return lstAllDoctor;
            }

            var result =  _context.Doctors.Where(doctor => doctor.SpecialtyId.Equals(specialtyId))
                .Include(specialty => specialty.Specialty)
                .Include(schedule => schedule.Schedules)
                .Include(s => s.IdNavigation)
                .Where(user => user.IdNavigation.Disabled == false && user.Disabled == false && user.Schedules.Count > 0)
                .OrderBy(s => s.Schedules.SingleOrDefault().AppointmentTime).ToListAsync();

            return result;
        }
    }
}
