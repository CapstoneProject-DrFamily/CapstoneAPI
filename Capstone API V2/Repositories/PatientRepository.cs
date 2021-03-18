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
                .Include(profile => profile.PatientNavigation)
                .ThenInclude(patient => patient.Account)
                .Where(patient => patient.PatientNavigation.Account.Disabled == false && patient.Disabled == false)
                .ToListAsync();

            return result;
        }

        public IQueryable<DependentModel> GetDependents(int accountId)
        {
            var listDependent = _context.Patients
                                                .Where(patient => patient.PatientNavigation.AccountId == accountId && patient.Disabled == false)
                                                .Select(patient => new DependentModel 
                                                {
                                                    PatientID = patient.PatientId,
                                                    DependentImage = patient.PatientNavigation.Image,
                                                    DependentName = patient.PatientNavigation.FullName,
                                                    DependentRelationShip = patient.Relationship,
                                                    ProfileID = patient.PatientNavigation.ProfileId
                                                });
            return listDependent;
        }

        public async Task<Patient> GetPatientByID(int patientId)
        {
            var result = await _context.Patients.Where(patient => patient.PatientId.Equals(patientId))
                .Include(patient => patient.PatientNavigation)
                .ThenInclude(patient => patient.Account)
                .Where(patient => patient.Disabled == false && patient.PatientNavigation.Account.Disabled == false)
                .SingleOrDefaultAsync();
             if(result == null)
            {
                result = await _context.Patients.Where(patient => patient.PatientId.Equals(patientId))
                .Include(patient => patient.PatientNavigation)
                .Where(patient => patient.Disabled == false)
                .SingleOrDefaultAsync();
            }
            return result;
        }
    }
}
