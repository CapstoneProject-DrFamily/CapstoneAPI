using Capstone_API_V2.Models;
using Capstone_API_V2.ViewModels;
using Capstone_API_V2.ViewModels.SimpleModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone_API_V2.Services
{
    public interface IPrescriptionService : IBaseService<Prescription, PrescriptionModel>
    {
        Task<PrescriptionSimpModel> CreatePrescription(PrescriptionSimpModel dto);
        Task<PrescriptionSimpModel> UpdatePrescription(PrescriptionSimpModel dto);
        Task<List<PrescriptionModel>> GetAllPrescription();
        Task<PrescriptionModel> GetPrescriptionByID(int prescriptionID);
    }
}
