using Capstone_API_V2.Models;
using Capstone_API_V2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone_API_V2.Services
{
    public interface IPatientService : IBaseService<Patient, PatientModel>
    {
        IQueryable<DependentModel> GetDepdentByIdAsync(int ID);
    }
}
