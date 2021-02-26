using AutoMapper;
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
    public class PrescriptionService : BaseService<Prescription, PrescriptionModel>, IPrescriptionService
    {
        public PrescriptionService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        protected override IGenericRepository<Prescription> _repository => _unitOfWork.PrescriptionRepository;

        public async Task<PrescriptionSimpModel> CreatePrescription(PrescriptionSimpModel dto)
        {
            var prescription = new Prescription
            {
                PrescriptionId = 0,
                Description = dto.Description,
                InsBy = dto.InsBy,
                InsDatetime = ConvertTimeZone(),
                UpdBy = dto.UpdBy,
                UpdDatetime = ConvertTimeZone()
        };
            dto.PrescriptionId = prescription.PrescriptionId;

            _unitOfWork.PrescriptionRepository.Add(_mapper.Map<Prescription>(prescription));

            foreach (PrescriptionDetailModel prescriptionDetail in dto.PrescriptionDetails)
            {
                var prescriptionDetailModel = new PrescriptionDetailModel
                {
                    PrescriptionDetailId = 0,
                    MedicineId = prescriptionDetail.MedicineId,
                    PrescriptionId = dto.PrescriptionId,
                    Method = prescriptionDetail.Method,
                    NumberOfTime = prescriptionDetail.NumberOfTime,
                    QuantityPerTime = prescriptionDetail.QuantityPerTime,
                    TimesPerDay = prescriptionDetail.TimesPerDay,

                };
                _unitOfWork.PrescriptionDetailRepository.Add(_mapper.Map<PrescriptionDetail>(prescriptionDetailModel));
            }

            await _unitOfWork.SaveAsync();

            return _mapper.Map<PrescriptionSimpModel>(dto);
        }

        public async Task<List<PrescriptionModel>> GetAllPrescription()
        {
            //var prescriptions = _mapper.Map<List<PrescriptionModel>>(await _unitOfWork.PrescriptionRepositorySep.GetAllPrescription());

            List<PrescriptionModel> prescriptions = new List<PrescriptionModel>();
            var entity = await _unitOfWork.PrescriptionRepositorySep.GetAllPrescription();
            foreach (Prescription prescription in entity)
            {
                PrescriptionModel prescriptionModel = _mapper.Map<PrescriptionModel>(prescription);
                prescriptions.Add(prescriptionModel);
            }
            return prescriptions;
        }

        public async Task<PrescriptionModel> GetPrescriptionByID(int prescriptionID)
        {
            var entity = await _unitOfWork.PrescriptionRepositorySep.GetPrescriptionByID(prescriptionID);
            return _mapper.Map<PrescriptionModel>(entity);
        }

        public override async Task<bool> DeleteAsync(object id)
        {
            var prescription = await _unitOfWork.PrescriptionRepositorySep.GetPrescriptionByID((int)id);

            if (prescription == null) throw new Exception("Not found prescription with id: " + id);
            foreach(PrescriptionDetail prescriptionDetail in prescription.PrescriptionDetails)
            {
                _unitOfWork.PrescriptionDetailRepository.Delete(prescriptionDetail.PrescriptionDetailId);
            }
            _unitOfWork.PrescriptionRepository.Delete(id);

            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<PrescriptionSimpModel> UpdatePrescription(PrescriptionSimpModel dto)
        {
            var entity = await _unitOfWork.PrescriptionRepositorySep.GetPrescriptionByID(dto.PrescriptionId);

            if (entity != null)
            {
                entity.UpdBy = dto.UpdBy;
                entity.UpdDatetime = ConvertTimeZone();
                entity.Description = dto.Description;

                _unitOfWork.PrescriptionRepository.Update(entity);

                foreach (PrescriptionDetailModel prescriptionDetail in dto.PrescriptionDetails)
                {
                    _unitOfWork.PrescriptionDetailRepository.Update(_mapper.Map<PrescriptionDetail>(prescriptionDetail));
                }
                await _unitOfWork.SaveAsync();
                return dto;
            }
            return null;

        }
    }
}
