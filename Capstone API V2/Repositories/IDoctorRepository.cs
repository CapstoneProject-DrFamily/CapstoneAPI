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
        Task<List<Doctor>> GetAllDoctor(string fullname);
        Task<Doctor> GetDoctorByID(int doctorId);
        Task<Doctor> GetDoctorByName(string fullname);
    }
}
