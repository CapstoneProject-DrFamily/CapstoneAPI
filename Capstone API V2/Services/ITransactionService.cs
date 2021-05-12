using Capstone_API_V2.Models;
using Capstone_API_V2.ViewModels;
using Capstone_API_V2.ViewModels.SimpleModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone_API_V2.Services
{
    public interface ITransactionService : IBaseService<Treatment, TreatmentModel>
    {
        Task<TreatmentSimpModel> CreateTransaction(TreatmentSimpModel dto);
        Task<List<TreatmentSimpModel>> CreateTransactions(List<TreatmentSimpModel> dtos);
        Task<TreatmentPutModel> UpdateTransaction(TreatmentPutModel dto);
        Task<List<TreatmentModel>> GetAllTransaction();
        Task<TreatmentModel> GetTransactionByID(string transactionID);
        IQueryable<TreatmentHistoryModel> GetTransactionByDoctorIDAsync(int doctorID, int status, DateTime dateStart);
        IQueryable<TreatmentHistoryModel> GetTransactionByPatientIDAsync(int patientID, int status);
    }
}
