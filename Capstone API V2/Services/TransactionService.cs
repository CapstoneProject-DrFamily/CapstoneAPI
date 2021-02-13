using AutoMapper;
using Capstone_API_V2.Models;
using Capstone_API_V2.Repositories;
using Capstone_API_V2.UnitOfWork;
using Capstone_API_V2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Capstone_API_V2.Helper.Constants;

namespace Capstone_API_V2.Services
{
    public class TransactionService : BaseService<Transaction, TransactionModel>, ITransactionService
    {
        public TransactionService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        protected override IGenericRepository<Transaction> _repository => _unitOfWork.TransactionRepository;

        public override async Task<TransactionModel> CreateAsync(TransactionModel dto)
        {
            dto.TransactionId = Guid.NewGuid().ToString();
            dto.Status = TransactionStatus.OPEN;
            dto.Disabled = false;

            var entity = _mapper.Map<Transaction>(dto);

            _repository.Add(entity);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<TransactionModel>(entity);
        }

        public async Task<List<TransactionModel>> GetAllTransaction()
        {
            List<TransactionModel> transactions = new List<TransactionModel>();
            var entity = await _unitOfWork.TransactionRepositorySep.GetAllTransaction();
            foreach (Transaction transaction in entity)
            {
                TransactionModel transactionModel = _mapper.Map<TransactionModel>(transaction);
                transactions.Add(transactionModel);
            }
            return transactions;
        }

        public async Task<TransactionModel> GetTransactionByID(string transactionID)
        {
            var entity = await _unitOfWork.TransactionRepositorySep.GetTransactionByID(transactionID);
            return _mapper.Map<TransactionModel>(entity);
        }

        public override async Task<bool> DeleteAsync(object id)
        {
            var entity = await _repository.GetById(id);

            if (entity == null || entity.Disabled == true) throw new Exception("Not found entity object with id: " + id);

            entity.Disabled = true;

            return await _unitOfWork.SaveAsync() > 0;
        }
    }
}
