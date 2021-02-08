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
    }
}
