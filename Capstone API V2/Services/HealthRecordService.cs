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
            //entity.InsBy = Constants.Roles.ROLE_ADMIN;
            entity.InsDatetime = ConvertTimeZone();
            //entity.UpdBy = Constants.Roles.ROLE_ADMIN;
            entity.UpdDatetime = ConvertTimeZone();

            _repository.Add(entity);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<HealthRecordModel>(entity);
        }

        /*public async override Task<HealthRecordModel> UpdateAsync(HealthRecordModel dto)
        {
            var entity = await _unitOfWork.HealthRecordRepository.GetById(dto.RecordId);
            if (entity != null)
            {
                entity.Name = dto.Name;
                entity.Form = dto.Form;
                entity.Strength = dto.Strength;
                entity.ActiveIngredient = dto.ActiveIngredient;
                entity.UpdBy = Constants.Roles.ROLE_ADMIN;
                entity.UpdDatetime = ConvertTimeZone();

                _repository.Update(entity);
                await _unitOfWork.SaveAsync();

                return _mapper.Map<MedicineModel>(entity);
            }
            return null;
        }*/
    }
}
