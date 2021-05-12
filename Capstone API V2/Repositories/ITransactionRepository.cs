using Capstone_API_V2.Models;
using Capstone_API_V2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone_API_V2.Repositories
{
    public interface ITransactionRepository
    {
        Task<List<Treatment>> GetAllTransaction();
        Task<Treatment> GetTransactionByID(string transactionID);
        IQueryable<TreatmentHistoryModel> GetTransactionByDoctorID(int doctorID, int status, DateTime dateStart);
        IQueryable<TreatmentHistoryModel> GetTransactionByPatientID(int patientID, int status);
        bool CheckOldPatient(int patientId, int doctorId);
    }
}
