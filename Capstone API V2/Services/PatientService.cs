using AutoMapper;
using Capstone_API_V2.Helper;
using Capstone_API_V2.Models;
using Capstone_API_V2.Repositories;
using Capstone_API_V2.UnitOfWork;
using Capstone_API_V2.ViewModels;
using Capstone_API_V2.ViewModels.SimpleModel;
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

        public async Task<PatientSimpModel> CreatePatient(PatientSimpModel dto)
        {
            var profile = await _unitOfWork.ProfileRepository.GetById(dto.Id);
            string fullname = profile.FullName;

            var entity = _mapper.Map<Patient>(dto);
            entity.Disabled = false;
            entity.InsBy = fullname;
            entity.InsDatetime = ConvertTimeZone();
            entity.UpdBy = fullname;
            entity.UpdDatetime = ConvertTimeZone();
            _repository.Add(entity);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<PatientSimpModel>(entity);
        }

        public async Task<PatientSimpModel> UpdatePatient(PatientSimpModel dto)
        {
            var entity = await _unitOfWork.PatientRepository.GetById(dto.Id);
            //var profile = await _unitOfWork.ProfileRepository.GetById(dto.Id);
            string fullname = profile.FullName;

            if (entity != null)
            {
                entity.Birthday = dto.Birthday;
                entity.Email = dto.Email;
                entity.Fullname = dto.Fullname;
                entity.Gender = dto.Gender;
                entity.IdCard = dto.IdCard;
                entity.Image = dto.Image;
                entity.Height = dto.Height;
                entity.Weight = dto.Weight;
                entity.BloodType = dto.BloodType;
                entity.AccountId = dto.AccountId;
                entity.Relationship = dto.Relationship;
                entity.Location = dto.Location;
                entity.UpdBy = fullname;
                entity.UpdDatetime = ConvertTimeZone();
                _unitOfWork.PatientRepository.Update(entity);
                await _unitOfWork.SaveAsync();
                return dto;
            }
            return null;
        }

        public IQueryable<DependentModel> GetDepdentByIdAsync(int ID)
        {
            return _unitOfWork.PatientRepositorySep.GetDependents(ID);
        }

        public override async Task<bool> DeleteAsync(object id)
        {
            var patient = await _unitOfWork.PatientRepositorySep.GetPatientByID((int)id);
            if (patient == null) throw new Exception("Not found patient with id: " + id);
            
            patient.Disabled = true;

            if (Constants.Relationship.OWNER.Equals(patient.Relationship))
            {
                patient.Account.Disabled = true;
            }

            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<List<PatientModel>> GetAllPatient()
        {
            var patients = await _unitOfWork.PatientRepositorySep.GetAllPatient();
            return _mapper.Map<List<PatientModel>>(patients);
        }

        public async Task<PatientModel> GetPatientByID(int patientId)
        {
            var patient = await _unitOfWork.PatientRepositorySep.GetPatientByID(patientId);
            return _mapper.Map<PatientModel>(patient);
        }
    }
}
