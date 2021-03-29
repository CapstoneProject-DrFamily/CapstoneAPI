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
            var doctorInfo = await _context.Doctors.Where(x => x.DoctorNavigation.ProfileId == profileID && x.Disabled == false)
                                       .Select(x => new DoctorRequestModel
                                       {
                                           DoctorId = x.DoctorId,
                                           DoctorImage = x.DoctorNavigation.Image,
                                           DoctorName = x.DoctorNavigation.FullName,
                                           DoctorSpecialty = x.Specialty.Name,
                                           DoctorServiceId = x.SpecialtyId,
                                       })
                                       .SingleOrDefaultAsync();
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
    }
}
