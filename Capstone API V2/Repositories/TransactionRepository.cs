using Capstone_API_V2.Models;
using Capstone_API_V2.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone_API_V2.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly FamilyDoctorContext _context;

        public TransactionRepository(FamilyDoctorContext context)
        {
            _context = context;
        }

        public async Task<List<Transaction>> GetAllTransaction()
        {
            var result = await _context.Transactions.Where(transaction => transaction.Disabled == false && transaction.Status != 0)
                .Include(transaction => transaction.Doctor)
                .Include(transaction => transaction.Doctor.Specialty)
                .Include(transaction => transaction.Doctor.DoctorNavigation)
                .Include(transaction => transaction.ExaminationHistory)
                .Include(transaction => transaction.Patient)
                .Include(transaction => transaction.Patient.PatientNavigation)
                .Include(transaction => transaction.Prescription)
                .Include(transaction => transaction.Service)
                .Include(transaction => transaction.SymptomDetails)
                .ThenInclude(symptomDetail => symptomDetail.Symptom).ToListAsync();

            return result;
        }

        public IQueryable<TransactionHistoryModel> GetTransactionByDoctorID(int doctorID, int status)
        {
            var transactions = _context.Transactions
                                                .Where(transaction => status != -1? transaction.DoctorId == doctorID && transaction.Disabled == false && transaction.Status == (byte)status : transaction.DoctorId == doctorID && transaction.Disabled == false && transaction.Status != 0)
                                                .Select(transaction => new TransactionHistoryModel
                                                {
                                                    TransactionId = transaction.TransactionId,
                                                    DateStart = transaction.DateStart,
                                                    DateEnd = transaction.DateEnd,
                                                    DoctorName = transaction.Doctor.DoctorNavigation.FullName,
                                                    PatientName = transaction.Patient.PatientNavigation.FullName,
                                                    Location = transaction.Location,
                                                    ServiceName = transaction.Service.ServiceName,
                                                    ServicePrice = transaction.Service.ServicePrice,
                                                    Status = transaction.Status
                                                }).OrderByDescending(o => o.DateStart);
            return transactions;
        }

        public async Task<Transaction> GetTransactionByID(string transactionID)
        {
            var result = await _context.Transactions.Where(transaction => transaction.TransactionId.Equals(transactionID) && transaction.Disabled == false)
                .Include(transaction => transaction.Doctor)
                .Include(transaction => transaction.Doctor.Specialty)
                .Include(transaction => transaction.Doctor.DoctorNavigation)
                .Include(transaction => transaction.ExaminationHistory)
                .Include(transaction => transaction.Patient)
                .Include(transaction => transaction.Patient.PatientNavigation)
                .Include(transaction => transaction.Prescription)
                .Include(transaction => transaction.Service)
                .Include(transaction => transaction.SymptomDetails)
                .ThenInclude(symptomDetail => symptomDetail.Symptom).SingleOrDefaultAsync();

            return result;
        }

        public IQueryable<TransactionHistoryModel> GetTransactionByPatientID(int patientID, int status)
        {
            var transactions = _context.Transactions
                                                .Where(transaction => status != -1 ? transaction.PatientId == patientID && transaction.Disabled == false && transaction.Status == status : transaction.PatientId == patientID && transaction.Disabled == false && transaction.Status != 0)
                                                .Select(transaction => new TransactionHistoryModel
                                                {
                                                    TransactionId = transaction.TransactionId,
                                                    DateStart = transaction.DateStart,
                                                    DateEnd = transaction.DateEnd,
                                                    DoctorName = transaction.Doctor.DoctorNavigation.FullName,
                                                    PatientName = transaction.Patient.PatientNavigation.FullName,
                                                    Location = transaction.Location,
                                                    ServiceName = transaction.Service.ServiceName,
                                                    ServicePrice = transaction.Service.ServicePrice,
                                                    Status = transaction.Status
                                                }).OrderByDescending(o => o.DateStart);
            return transactions;
        }
    }
}
