using Capstone_API_V2.Models;
using Capstone_API_V2.ViewModels;
using Capstone_API_V2.ViewModels.SimpleModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone_API_V2.Services
{
    public interface IPatientService : IBaseService<Patient, PatientModel>
    {
        IQueryable<DependentModel> GetDepdentByIdAsync(int ID);
        Task<PatientSimpModel> CreatePatient(PatientSimpModel dto);
        Task<PatientSimpModel> UpdatePatient(PatientSimpModel dto);
    }
}
