using Capstone_API_V2.Models;
using Capstone_API_V2.ViewModels;
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

        public IQueryable<DependentModel> GetDependents(int accountId)
        {
            var listDependent = _context.Patients
                                                .Where(patient => patient.AccountId == accountId)
                                                .Select(patient => new DependentModel 
                                                {
                                                    PatientID = patient.PatientId,
                                                    DependentName = patient.Profile.FullName,
                                                    DependentRelationShip = patient.Relationship,
                                                });
            return listDependent;
        }
    }
}
