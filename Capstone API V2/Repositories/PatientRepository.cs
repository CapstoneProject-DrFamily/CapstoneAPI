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
                .Include(patient => patient.Account)
                .Where(patient => patient.Account.Disabled == false && patient.Disabled == false)
                .ToListAsync();

            return result;
        }

        public IQueryable<DependentModel> GetDependents(int accountId)
        {
            var listDependent = _context.Patients
                                                .Where(patient => patient.AccountId == accountId && patient.Disabled == false)
                                                .Select(patient => new DependentModel 
                                                {
                                                    PatientID = patient.Id,
                                                    DependentImage = patient.Image,
                                                    DependentName = patient.Fullname,
                                                    DependentRelationShip = patient.Relationship
                                                });
            return listDependent;
        }

        public async Task<Patient> GetPatientByID(int patientId)
        {
            var result = await _context.Patients.Where(patient => patient.Id.Equals(patientId))
                .Include(patient => patient.Account)
                .Where(patient => patient.Disabled == false && patient.Account.Disabled == false)
                .SingleOrDefaultAsync();
            return result;
        }
    }
}
