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
    public class FamilyDoctorService : BaseService<Service, ServiceModel>, IFamilyDoctorService
    {
        public FamilyDoctorService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        protected override IGenericRepository<Service> _repository => _unitOfWork.ServiceRepository;

        public override async Task<ServiceModel> CreateAsync(ServiceModel dto)
        {
            //Increment index when inserted
            dto.Id = 0;
            var entity = _mapper.Map<Service>(dto);
            entity.Disabled = false;
            entity.InsBy = Constants.Roles.ROLE_ADMIN;
            entity.InsDatetime = ConvertTimeZone();
            entity.UpdBy = Constants.Roles.ROLE_ADMIN;
            entity.UpdDatetime = ConvertTimeZone();

            _repository.Add(entity);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<ServiceModel>(entity);
        }

        public override async Task<ServiceModel> UpdateAsync(ServiceModel dto)
        {
            var entity = await _unitOfWork.ServiceRepository.GetById(dto.Id);
            if(entity != null)
            {
                entity.Name = dto.Name;
                entity.Price = dto.Price;
                entity.Description = dto.Description;
                entity.Image = dto.Image;
                entity.UpdBy = Constants.Roles.ROLE_ADMIN;
                entity.UpdDatetime = ConvertTimeZone();

                _repository.Update(entity);
                await _unitOfWork.SaveAsync();

                return _mapper.Map<ServiceModel>(entity);
            }
            return null;
        }

        public override async Task<bool> DeleteAsync(object id)
        {
            var entity = await _repository.GetById(id);

            if (entity == null || entity.Disabled == true) throw new Exception("Not found service with id: " + id);

            entity.Disabled = true;

            return await _unitOfWork.SaveAsync() > 0;
        }
    }
}
