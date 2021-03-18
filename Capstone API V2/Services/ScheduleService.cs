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
    public class ScheduleService : BaseService<Schedule, ScheduleModel>, IScheduleService
    {
        public ScheduleService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        protected override IGenericRepository<Schedule> _repository => _unitOfWork.ScheduleRepository;

        public override async Task<ScheduleModel> CreateAsync(ScheduleModel dto)
        {
            //Increment index when inserted
            dto.ScheduleId = 0;

            var entity = _mapper.Map<Schedule>(dto);
            entity.Disabled = false;
            entity.InsBy = dto.InsBy;
            entity.InsDatetime = ConvertTimeZone();
            entity.UpdBy = dto.UpdBy;
            entity.UpdDatetime = ConvertTimeZone();

            _repository.Add(entity);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<ScheduleModel>(entity);
        }

        public async override Task<ScheduleModel> UpdateAsync(ScheduleModel dto)
        {
            var entity = await _unitOfWork.ScheduleRepository.GetById(dto.ScheduleId);
            if (entity != null)
            {
                entity.AppointmentTime = dto.AppointmentTime;
                entity.Status = dto.Status;
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

            if (entity == null) throw new Exception("Not found schedule object with id: " + id);

            entity.Disabled = true;

            return await _unitOfWork.SaveAsync() > 0;
        }
    }
}
