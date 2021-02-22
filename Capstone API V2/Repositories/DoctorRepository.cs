﻿using Capstone_API_V2.Models;
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
            var doctorInfo = await _context.Doctors.Where(x => x.ProfileId == profileID)
                                       .Select(x => new DoctorRequestModel
                                       {
                                           DoctorId = x.DoctorId,
                                           DoctorImage = x.Profile.Image,
                                           DoctorName = x.Profile.FullName,
                                           DoctorSpecialty = x.Specialty.Name,
                                       })
                                       .SingleOrDefaultAsync();
            return doctorInfo;
        }

        public async Task<List<Doctor>> GetAllDoctor(string fullname)
        {
            if (string.IsNullOrWhiteSpace(fullname))
            {
                return await _context.Doctors
                .Include(specialty => specialty.Specialty)
                .Include(profile => profile.Profile)
                .ThenInclude(user => user.Users)
                .ToListAsync();
            } else


            return await _context.Doctors
                .Include(specialty => specialty.Specialty)
                .Include(profile => profile.Profile)
                .ThenInclude(user => user.Users)
                .Where(t => t.Profile.FullName.Contains(fullname))
                .ToListAsync();
        }

        public async Task<Doctor> GetDoctorByID(int doctorId)
        {
            var result = await _context.Doctors.Where(doctor => doctor.DoctorId.Equals(doctorId))
                .Include(specialty => specialty.Specialty)
                .Include(profile => profile.Profile)
                .ThenInclude(user => user.Users)
                .SingleOrDefaultAsync();

            return result;
        }

        public async Task<Doctor> GetDoctorByName(string fullname)
        {
            var p = await _context.Profiles.Where(pr => pr.FullName.Equals(fullname)).Include(d => d.Doctors)
                .Include(u => u.Users).Where(pr => pr.Users.SingleOrDefault().Disabled == false).SingleOrDefaultAsync();
            
            var result = await _context.Doctors.Where(doctor => doctor.DoctorId == p.Doctors.SingleOrDefault().DoctorId)
                .Include(specialty => specialty.Specialty)
                .Include(profile => profile.Profile)
                .ThenInclude(user => user.Users).SingleOrDefaultAsync();

            return result;
        }


    }
}
