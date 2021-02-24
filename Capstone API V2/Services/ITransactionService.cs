﻿using Capstone_API_V2.Models;
using Capstone_API_V2.ViewModels;
using Capstone_API_V2.ViewModels.SimpleModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone_API_V2.Services
{
    public interface ITransactionService : IBaseService<Transaction, TransactionModel>
    {
        Task<TransactionSimpModel> CreateTransaction(TransactionSimpModel dto);
        Task<TransactionSimpModel> UpdateTransaction(TransactionSimpModel dto);
        Task<List<TransactionModel>> GetAllTransaction();
        Task<TransactionModel> GetTransactionByID(string transactionID);
    }
}