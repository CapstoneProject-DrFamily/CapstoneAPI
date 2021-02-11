using AutoMapper;
using Capstone_API_V2.Helper;
using Capstone_API_V2.Models;
using Capstone_API_V2.Repositories;
using Capstone_API_V2.UnitOfWork;
using Capstone_API_V2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone_API_V2.Services
{
    public class ExaminationHistoryService : BaseService<ExaminationHistory, ExaminationHistoryModel>, IExaminationHistoryService
    {
        public ExaminationHistoryService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        protected override IGenericRepository<ExaminationHistory> _repository => _unitOfWork.ExaminationHistoryRepository;

        public override async Task<ExaminationHistoryModel> CreateAsync(ExaminationHistoryModel dto)
        {
            var entity = _mapper.Map<ExaminationHistory>(dto);

            _repository.Add(entity);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<ExaminationHistoryModel>(entity);
        }
    }
}
