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
            //Increment index when inserted
            dto.Id = 0;

            var entity = _mapper.Map<ExaminationHistory>(dto);
            entity.InsDatetime = ConvertTimeZone();
            entity.UpdDatetime = ConvertTimeZone();

            _repository.Add(entity);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<ExaminationHistoryModel>(entity);
        }

        public async override Task<ExaminationHistoryModel> UpdateAsync(ExaminationHistoryModel dto)
        {
            var entity = await _unitOfWork.ExaminationHistoryRepository.GetById(dto.Id);
            if (entity != null)
            {
                entity.AbdominalUltrasound = dto.AbdominalUltrasound;
                entity.Activity = dto.Activity;
                entity.Advisory = dto.Advisory;
                entity.BloodChemistry = dto.BloodChemistry;
                entity.BloodPressure = dto.BloodPressure;
                entity.Cardiovascular = dto.Cardiovascular;
                entity.Conclusion = dto.Conclusion;
                entity.Dermatology = dto.Dermatology;
                entity.Endocrine = dto.Endocrine;
                entity.Evaluation = dto.Evaluation;
                entity.Gastroenterology = dto.Gastroenterology;
                entity.Height = dto.Height;
                entity.Hematology = dto.Hematology;
                entity.History = dto.History;
                entity.LeftEye = dto.LeftEye;
                entity.LeftEyeGlassed = dto.LeftEyeGlassed;
                entity.Mental = dto.Mental;
                entity.Mucosa = dto.Mucosa;
                entity.Nephrology = dto.Nephrology;
                entity.Nerve = dto.Nerve;
                entity.Nutrition = dto.Nutrition;
                entity.ObstetricsGynecology = dto.ObstetricsGynecology;
                entity.OdontoStomatology = dto.OdontoStomatology;
                entity.Ophthalmology = dto.Ophthalmology;
                entity.OtherBody = dto.OtherBody;
                entity.Otorhinolaryngology = dto.Otorhinolaryngology;
                entity.PulseRate = dto.PulseRate;
                entity.Respiratory = dto.Respiratory;
                entity.RespiratoryRate = dto.RespiratoryRate;
                entity.Rheumatology = dto.Rheumatology;
                entity.RightEye = dto.RightEye;
                entity.RightEyeGlassed = dto.RightEyeGlassed;
                entity.Surgery = dto.Surgery;
                entity.Temperature = dto.Temperature;
                entity.UrineBiochemistry = dto.UrineBiochemistry;
                entity.WaistCircumference = dto.WaistCircumference;
                entity.Weight = dto.Weight;
                entity.UpdBy = dto.UpdBy;
                entity.UpdDatetime = ConvertTimeZone();

                _repository.Update(entity);
                await _unitOfWork.SaveAsync();

                return _mapper.Map<ExaminationHistoryModel>(entity);
            }
            return null;
        }
    }
}
