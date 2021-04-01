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
    public class DiseaseService : BaseService<Disease, DiseaseModel>, IDiseaseService
    {
        public DiseaseService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        protected override IGenericRepository<Disease> _repository => _unitOfWork.DiseaseRepository;

        public async override Task<DiseaseModel> CreateAsync(DiseaseModel dto)
        {
            var entity = _mapper.Map<Disease>(dto);
            entity.Disabled = false;
            entity.InsBy = Constants.Roles.ROLE_ADMIN;
            entity.InsDatetime = ConvertTimeZone();
            entity.UpdBy = Constants.Roles.ROLE_ADMIN;
            entity.UpdDatetime = ConvertTimeZone();

            _repository.Add(entity);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<DiseaseModel>(entity);
        }

        public async override Task<DiseaseModel> UpdateAsync(DiseaseModel dto)
        {
            var entity = await _unitOfWork.DiseaseRepository.GetById(dto.DiseaseCode);
            if (entity != null)
            {
                entity.DiseaseName = dto.DiseaseName;
                entity.ChapterCode = dto.ChapterCode;
                entity.ChapterName = dto.ChapterName;
                entity.MainGroupCode = dto.MainGroupCode;
                entity.MainGroupName = dto.MainGroupName;
                entity.UpdBy = Constants.Roles.ROLE_ADMIN;
                entity.UpdDatetime = ConvertTimeZone();

                _repository.Update(entity);
                await _unitOfWork.SaveAsync();

                return _mapper.Map<DiseaseModel>(entity);
            }
            return null;
        }

        public async override Task<bool> DeleteAsync(object id)
        {
            var entity = await _repository.GetById(id);

            if (entity == null || entity.Disabled == true) throw new Exception("Not found entity object with id: " + id);

            entity.Disabled = true;

            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<List<DiseaseModel>> GetAllDisease()
        {
            var diseases = await _unitOfWork.DiseaseRepositorySep.GetAllDisease();
            return _mapper.Map<List<DiseaseModel>>(diseases);
        }

        public async Task<PaginatedList<DiseaseModel>> GetAsync(ResourceParameter model)
        {
            return await _unitOfWork.DiseaseRepositorySep.Get(model); ;
        }

        public async Task<DiseaseModel> GetDiseaseByID(string diseaseId)
        {
            var diseases = await _unitOfWork.DiseaseRepositorySep.GetDiseaseByID(diseaseId);
            return _mapper.Map<DiseaseModel>(diseases);
        }
    }
}
