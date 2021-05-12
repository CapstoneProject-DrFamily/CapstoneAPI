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
using static Capstone_API_V2.Helper.Constants;

namespace Capstone_API_V2.Services
{
    public class TransactionService : BaseService<Treatment, TreatmentModel>, ITransactionService
    {
        public TransactionService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        protected override IGenericRepository<Treatment> _repository => _unitOfWork.TransactionRepository;


        public async Task<TreatmentSimpModel> CreateTransaction(TreatmentSimpModel dto)
        {
            var transaction = new Treatment
            {
                Id = "TS-" + Guid.NewGuid().ToString(),
                DoctorId = dto.DoctorId,
                //DoctorId = dto.DoctorId != 0 ? dto.DoctorId : null,
                PatientId = dto.PatientId != 0 ? dto.PatientId : null,
                ScheduleId = dto.ScheduleId != 0 ? dto.ScheduleId : null,
                DateStart = ConvertTimeZone(),
                ServiceId = dto.ServiceId,
                //ServiceId = dto.ServiceId != 0 ? dto.ServiceId : null,
                Location = dto.Location,
                Note = dto.Note,
                Status = TransactionStatus.OPEN,
                Disabled = false
            };
            dto.Id = transaction.Id;
            dto.DateStart = transaction.DateStart;

            _unitOfWork.TransactionRepository.Add(_mapper.Map<Treatment>(transaction));
            if (dto.SymptomDetails != null)
            {
                foreach (SymptomDetailModel symptomDetail in dto.SymptomDetails)
                {
                    var symptomDetailModel = new SymptomDetailModel
                    {
                        SymptomDetailId = 0,
                        SymptomId = symptomDetail.SymptomId,
                        TransactionId = dto.Id
                    };
                    _unitOfWork.SymptomDetailRepository.Add(_mapper.Map<SymptomDetail>(symptomDetailModel));
                }
            }
            await _unitOfWork.SaveAsync();

            return _mapper.Map<TreatmentSimpModel>(dto);
        }

        public async Task<List<TreatmentSimpModel>> CreateTransactions(List<TreatmentSimpModel> dtos)
        {
            foreach(var dto in dtos)
            {
                var transaction = new Treatment
                {
                    Id = "TS-" + Guid.NewGuid().ToString(),
                    DoctorId = dto.DoctorId,
                    PatientId = dto.PatientId != 0 ? dto.PatientId : null,
                    ScheduleId = dto.ScheduleId != 0 ? dto.ScheduleId : null,
                    DateStart = ConvertTimeZone(),
                    ServiceId = dto.ServiceId,
                    Location = dto.Location,
                    Note = dto.Note,
                    Status = TransactionStatus.OPEN,
                    Disabled = false
                };
                dto.Id = transaction.Id;
                dto.DateStart = transaction.DateStart;

                _unitOfWork.TransactionRepository.Add(_mapper.Map<Treatment>(transaction));
            }
            await _unitOfWork.SaveAsync();
            return dtos;
        }

        public async Task<List<TreatmentModel>> GetAllTransaction()
        {
            List<TreatmentModel> transactions = new List<TreatmentModel>();
            var entity = await _unitOfWork.TransactionRepositorySep.GetAllTransaction();
            foreach (Treatment transaction in entity)
            {
                TreatmentModel transactionModel = _mapper.Map<TreatmentModel>(transaction);
                transactions.Add(transactionModel);
            }
            return transactions;
        }

        public async Task<TreatmentModel> GetTransactionByID(string transactionID)
        {
            var entity = await _unitOfWork.TransactionRepositorySep.GetTransactionByID(transactionID);
            var result = _mapper.Map<TreatmentModel>(entity);
            result.isOldPatient = _unitOfWork.TransactionRepositorySep.CheckOldPatient(result.PatientId, result.DoctorId);
            return result;
        }

        public override async Task<bool> DeleteAsync(object id)
        {
            var entity = await _repository.GetById(id);

            if (entity == null || entity.Disabled == true) throw new Exception("Not found transaction with id: " + id);

            entity.Disabled = true;

            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<TreatmentPutModel> UpdateTransaction(TreatmentPutModel dto)
        {
            var entity = await _unitOfWork.TransactionRepositorySep.GetTransactionByID(dto.Id);
            if(entity != null)
            {
                entity.DoctorId = dto.DoctorId;
                entity.PatientId = dto.PatientId != 0 ? dto.PatientId : null;
                entity.DateEnd = ConvertTimeZone();
                entity.Location = dto.Location;
                entity.Note = dto.Note;
                entity.EstimatedTime = dto.EstimatedTime;
                entity.Status = dto.Status;
                entity.ScheduleId = dto.ScheduleId;
                entity.ReasonCancel = dto.ReasonCancel;
                //entity.PrescriptionId = dto.PrescriptionId;
                //entity.ExamId = dto.ExamId;
                _unitOfWork.TransactionRepository.Update(entity);
                await _unitOfWork.SaveAsync();
                return dto;
            }
            return null;
            
        }

        public IQueryable<TreatmentHistoryModel> GetTransactionByDoctorIDAsync(int doctorID, int status, DateTime dateStart)
        {
            return _unitOfWork.TransactionRepositorySep.GetTransactionByDoctorID(doctorID, status, dateStart);
        }

        public IQueryable<TreatmentHistoryModel> GetTransactionByPatientIDAsync(int patientID, int status)
        {
            return _unitOfWork.TransactionRepositorySep.GetTransactionByPatientID(patientID, status);
        }
    }
}
