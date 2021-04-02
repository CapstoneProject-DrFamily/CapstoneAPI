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
    public class AppConfigService : BaseService<AppConfig, AppConfigModel>, IAppConfigService
    {
        public AppConfigService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        protected override IGenericRepository<AppConfig> _repository => _unitOfWork.AppConfigRepository;

        public async override Task<AppConfigModel> CreateAsync(AppConfigModel dto)
        {
            var entity = _mapper.Map<AppConfig>(dto);
            //entity.Disabled = false;
            entity.InsBy = Constants.Roles.ROLE_ADMIN;
            entity.InsDatetime = ConvertTimeZone();
            entity.UpdBy = Constants.Roles.ROLE_ADMIN;
            entity.UpdDatetime = ConvertTimeZone();

            _repository.Add(entity);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<AppConfigModel>(entity);
        }

        public async override Task<AppConfigModel> UpdateAsync(AppConfigModel dto)
        {
            var entity = await _repository.GetById(dto.AppId);
            if (entity != null)
            {
                entity.ConfigValue = dto.ConfigValue;
                //entity.AppName = dto.AppName;
                entity.UpdBy = Constants.Roles.ROLE_ADMIN;
                entity.UpdDatetime = ConvertTimeZone();

                _repository.Update(entity);
                await _unitOfWork.SaveAsync();

                return _mapper.Map<AppConfigModel>(entity);
            }
            return null;
        }

        public async override Task<bool> DeleteAsync(object id)
        {
            var entity = await _repository.GetById(id);

            if (entity == null) throw new Exception("Not found entity object with id: " + id);

            _repository.Delete(id);

            return await _unitOfWork.SaveAsync() > 0;
        }
    }
}
