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
    public class SpecialtyService : BaseService<Specialty, SpecialtyModel>, ISpecialtyService
    {
        public SpecialtyService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        protected override IGenericRepository<Specialty> _repository => _unitOfWork.SpecialtyRepository;

        public override async Task<SpecialtyModel> CreateAsync(SpecialtyModel dto)
        {
            var entity = _mapper.Map<Specialty>(dto);
            entity.Disabled = false;
            entity.InsBy = Constants.Roles.ROLE_ADMIN;
            entity.InsDatetime = DateTime.Now;
            entity.UpdBy = Constants.Roles.ROLE_ADMIN;
            entity.UpdDatetime = DateTime.Now;

            _repository.Add(entity);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<SpecialtyModel>(entity);
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
