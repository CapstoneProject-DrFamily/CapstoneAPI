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
    public class HealthRecordService : BaseService<HealthRecord,HealthRecordModel>, IHealthRecordService
    {
        public HealthRecordService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        protected override IGenericRepository<HealthRecord> _repository => _unitOfWork.HealthRecordRepository;

        public override async Task<HealthRecordModel> CreateAsync(HealthRecordModel dto)
        {
            var entity = _mapper.Map<HealthRecord>(dto);
            entity.InsDatetime = ConvertTimeZone();
            entity.UpdDatetime = ConvertTimeZone();

            _repository.Add(entity);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<HealthRecordModel>(entity);
        }

        public async override Task<HealthRecordModel> UpdateAsync(HealthRecordModel dto)
        {
            var entity = await _unitOfWork.HealthRecordRepository.GetById(dto.RecordId);
            if (entity != null)
            {
                entity.ActivityFrequency = dto.ActivityFrequency;
                entity.BirthDefects = dto.BirthDefects;
                entity.BirthHeight = dto.BirthHeight;
                entity.BirthWeight = dto.BirthWeight;
                entity.CancerFamily = dto.CancerFamily;
                entity.ChemicalAllergy = dto.ChemicalAllergy;
                entity.ChemicalAllergyFamily = dto.ChemicalAllergyFamily;
                entity.CleftLip = dto.CleftLip;
                entity.ConditionAtBirth = dto.ConditionAtBirth;
                entity.ContactTime = dto.ContactTime;
                entity.Disease = dto.Disease;
                entity.DiseaseFamily = dto.DiseaseFamily;
                entity.DrinkingFrequency = dto.DrinkingFrequency;
                entity.DrugFrequency = dto.DrugFrequency;
                entity.ExposureElement = dto.ExposureElement;
                entity.Eyesight = dto.Eyesight;
                entity.FoodAllergy = dto.FoodAllergy;
                entity.FoodAllergyFamily = dto.FoodAllergyFamily;
                entity.Hand = dto.Hand;
                entity.Hearing = dto.Hearing;
                entity.Leg = dto.Leg;
                entity.MedicineAllergy = dto.MedicineAllergy;
                entity.MedicineAllergyFamily = dto.MedicineAllergyFamily;
                entity.Other = dto.Other;
                entity.OtherAllergy = dto.OtherAllergy;
                entity.OtherAllergyFamily = dto.OtherAllergyFamily;
                entity.OtherDefects = dto.OtherDefects;
                entity.OtherDisabilities = dto.OtherDisabilities;
                entity.OtherDiseases = dto.OtherDiseases;
                entity.OtherDiseasesFamily = dto.OtherDiseasesFamily;
                entity.OtherRisks = dto.OtherRisks;
                entity.Scoliosis = dto.Scoliosis;
                entity.SmokingFrequency = dto.SmokingFrequency;
                entity.SurgeryHistory = dto.SurgeryHistory;
                entity.ToiletType = dto.ToiletType;
                entity.Tuberculosis = dto.Tuberculosis;
                entity.TuberculosisFamily = dto.TuberculosisFamily;
                entity.SurgeryHistory = dto.SurgeryHistory;
                entity.UpdBy = dto.UpdBy;
                entity.UpdDatetime = ConvertTimeZone();

                _repository.Update(entity);
                await _unitOfWork.SaveAsync();

                return _mapper.Map<HealthRecordModel>(entity);
            }
            return null;
        }
    }
}
