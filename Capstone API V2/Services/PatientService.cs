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
    public class PatientService : BaseService<Patient, PatientModel>, IPatientService
    {
        public PatientService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        protected override IGenericRepository<Patient> _repository => _unitOfWork.PatientRepository;

        public override async Task<PatientModel> CreateAsync(PatientModel dto)
        {
            var entity = _mapper.Map<Patient>(dto);
            /*entity.InsBy = Constants.Roles.ROLE_ADMIN;
            entity.InsDatetime = DateTime.Now;
            entity.UpdBy = Constants.Roles.ROLE_ADMIN;
            entity.UpdDatetime = DateTime.Now;*/

            _repository.Add(entity);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<PatientModel>(entity);
        }

        public IQueryable<DependentModel> GetDepdentByIdAsync(int ID)
        {
            return _unitOfWork.PatientRepositorySep.GetDependents(ID);
        }

        /*public override async Task<bool> DeleteAsync(object id)
        {
            var entity = await _repository.GetById(id);

            if (entity == null) throw new Exception("Not found entity object with id: " + id);

            entity.Disabled = true;

            return await _unitOfWork.SaveAsync() > 0;
        }*/
    }
}
