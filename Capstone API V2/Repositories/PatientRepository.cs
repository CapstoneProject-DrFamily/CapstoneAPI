using Capstone_API_V2.Models;
using Capstone_API_V2.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone_API_V2.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly FamilyDoctorContext _context;

        public PatientRepository(FamilyDoctorContext context)
        {
            _context = context;
        }

        public async Task<List<Patient>> GetAllPatient()
        {
            var result = await _context.Patients
                .Include(profile => profile.Profile)
                .ThenInclude(user => user.Users)
                .Where(user => user.Profile.Users.SingleOrDefault().Disabled == false)
                .ToListAsync();

            return result;
        }

        public IQueryable<DependentModel> GetDependents(int accountId)
        {
            var listDependent = _context.Patients
                                                .Where(patient => patient.AccountId == accountId)
                                                .Select(patient => new DependentModel 
                                                {
                                                    PatientID = patient.PatientId,
                                                    DependentImage = patient.Profile.Image,
                                                    DependentName = patient.Profile.FullName,
                                                    DependentRelationShip = patient.Relationship,
                                                });
            return listDependent;
        }

        public async Task<Patient> GetPatientByID(int patientId)
        {
            var result = await _context.Patients.Where(patient => patient.PatientId.Equals(patientId))
                .Include(profile => profile.Profile)
                .ThenInclude(user => user.Users)
                .Where(user => user.Profile.Users.SingleOrDefault().Disabled == false)
                .SingleOrDefaultAsync();

            return result;
        }
    }
}
