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
    public class MedicineService : BaseService<Medicine, MedicineModel>, IMedicineService
    {
        public MedicineService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        protected override IGenericRepository<Medicine> _repository => _unitOfWork.MedicineRepository;

        public override async Task<MedicineModel> CreateAsync(MedicineModel dto)
        {
            //Increment index when inserted
            dto.Id = 0;

            var entity = _mapper.Map<Medicine>(dto);
            entity.Disabled = false;
            entity.InsBy = Constants.Roles.ROLE_ADMIN;
            entity.InsDatetime = ConvertTimeZone();
            entity.UpdBy = Constants.Roles.ROLE_ADMIN;
            entity.UpdDatetime = ConvertTimeZone();

            _repository.Add(entity);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<MedicineModel>(entity);
        }

        public override async Task<MedicineModel> UpdateAsync(MedicineModel dto)
        {
            var entity = await _unitOfWork.MedicineRepository.GetById(dto.Id);
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
