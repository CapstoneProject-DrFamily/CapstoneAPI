using AutoMapper;
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
    public class FeedbackService : BaseService<Feedback, FeedbackModel>, IFeedbackService
    {
        public FeedbackService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        protected override IGenericRepository<Feedback> _repository => _unitOfWork.FeedbackRepository;

        public override async Task<FeedbackModel> CreateAsync(FeedbackModel dto)
        {
            var entity = _mapper.Map<Feedback>(dto);
            entity.InsBy = dto.InsBy;
            entity.InsDatetime = ConvertTimeZone();
            entity.UpdBy = dto.UpdBy;
            entity.UpdDatetime = ConvertTimeZone();

            _repository.Add(entity);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<FeedbackModel>(entity);
        }

        public async override Task<FeedbackModel> UpdateAsync(FeedbackModel dto)
        {
            var entity = await _unitOfWork.FeedbackRepository.GetById(dto.FeedbackId);
            if (entity != null)
            {
                entity.Note = dto.Note;
                entity.RatingPoint = dto.RatingPoint;
                entity.UpdBy = dto.UpdBy;
                entity.UpdDatetime = ConvertTimeZone();
                _repository.Update(entity);
                await _unitOfWork.SaveAsync();
                return dto;
            }
            return null;
        }

        public override async Task<bool> DeleteAsync(object id)
        {
            var entity = await _repository.GetById(id);

            if (entity == null) throw new Exception("Not found feedback object with id: " + id);

            _repository.Delete(id);

            return await _unitOfWork.SaveAsync() > 0;
        }
    }
}
