using Capstone_API_V2.Helper;
using Capstone_API_V2.Models;
using Capstone_API_V2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone_API_V2.Repositories
{
    public interface IDoctorRepository
    {
        Task<DoctorRequestModel> GetRequestDoctorInfo(int profileID);
        Task<List<Doctor>> GetAllDoctor();
        Task<Doctor> GetDoctorByID(int doctorId);
        Task<List<Doctor>> GetDoctorByName(string fullname);
        Task<List<Doctor>> GetWaitingDoctor();
        Task<List<Doctor>> GetBySpecialtyId(int specialtyId);
    }
}
