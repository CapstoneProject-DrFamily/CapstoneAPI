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
                .ThenInclude(patient => patient.Users)
                .Where(patient => patient.Profile.Users.SingleOrDefault().Disabled == false && patient.Disabled == false)
                .ToListAsync();

            return result;
        }

        public IQueryable<DependentModel> GetDependents(int accountId)
        {
            var listDependent = _context.Patients
                                                .Where(patient => patient.AccountId == accountId && patient.Disabled == false)
                                                .Select(patient => new DependentModel 
                                                {
                                                    PatientID = patient.PatientId,
                                                    DependentImage = patient.Profile.Image,
                                                    DependentName = patient.Profile.FullName,
                                                    DependentRelationShip = patient.Relationship,
                                                    ProfileID = patient.ProfileId
                                                });
            return listDependent;
        }

        public async Task<Patient> GetPatientByID(int patientId)
        {
            var result = await _context.Patients.Where(patient => patient.PatientId.Equals(patientId))
                .Include(patient => patient.Profile)
                .ThenInclude(patient => patient.Users)
                .Where(patient => patient.Disabled == false && patient.Profile.Users.SingleOrDefault().Disabled == false)
                .SingleOrDefaultAsync();
             if(result == null)
            {
                result = await _context.Patients.Where(patient => patient.PatientId.Equals(patientId))
                .Include(patient => patient.Profile)
                .Where(patient => patient.Disabled == false)
                .SingleOrDefaultAsync();
            }
            return result;
        }
    }
}
