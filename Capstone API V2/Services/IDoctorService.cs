using Capstone_API_V2.Models;
using Capstone_API_V2.ViewModels;
using Capstone_API_V2.ViewModels.SimpleModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone_API_V2.Services
{
    public interface IDoctorService : IBaseService<Doctor, DoctorModel>
    {
        Task<DoctorRequestModel> GetRequestDoctorInfo(int profileID);
        Task<DoctorSimpModel> CreateDoctor(DoctorSimpModel dto); 
        Task<DoctorSimpModel> UpdateDoctor(DoctorSimpModel dto);
        Task<List<DoctorModel>> GetAllDoctor();
        Task<DoctorModel> GetDoctorByID(int doctorId);
        Task<List<DoctorModel>> GetDoctorByName(string fullname);
    }
}
