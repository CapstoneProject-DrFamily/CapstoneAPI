using Capstone_API_V2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone_API_V2.Repositories
{
    public interface IPrescriptionRepository
    {
        Task<List<Prescription>> GetAllPrescription();
        Task<Prescription> GetPrescriptionByID(int prescriptionID);
    }
}
