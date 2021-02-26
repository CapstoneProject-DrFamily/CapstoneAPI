﻿using Capstone_API_V2.Models;
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
                .Include(transaction => transaction.Doctor.Profile)
                .Include(transaction => transaction.Exam)
                .Include(transaction => transaction.Patient)
                .Include(transaction => transaction.Patient.Profile)
                .Include(transaction => transaction.Prescription)
                .Include(transaction => transaction.Service)
                .Include(transaction => transaction.SymptomDetails)
                .ThenInclude(symptomDetail => symptomDetail.Symptom).ToListAsync();

            return result;
        }

        public async Task<Transaction> GetTransactionByID(string transactionID)
        {
            var result = await _context.Transactions.Where(transaction => transaction.TransactionId.Equals(transactionID) && transaction.Disabled == false)
                .Include(transaction => transaction.Doctor)
                .Include(transaction => transaction.Doctor.Profile)
                .Include(transaction => transaction.Exam)
                .Include(transaction => transaction.Patient)
                .Include(transaction => transaction.Patient.Profile)
                .Include(transaction => transaction.Prescription)
                .Include(transaction => transaction.Service)
                .Include(transaction => transaction.SymptomDetails)
                .ThenInclude(symptomDetail => symptomDetail.Symptom).SingleOrDefaultAsync();

            return result;
        }
    }
}
