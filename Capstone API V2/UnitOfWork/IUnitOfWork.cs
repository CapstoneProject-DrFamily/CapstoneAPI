using Capstone_API_V2.Models;
using Capstone_API_V2.Repositories;
using System.Threading.Tasks;

namespace Capstone_API_V2.UnitOfWork
{
    public interface IUnitOfWork
    {
        IGenericRepository<Medicine> MedicineRepository { get; }
        IGenericRepository<Doctor> DoctorRepository { get; }
        IGenericRepository<Feedback> FeedbackRepository { get; }
        IGenericRepository<HealthRecord> HealthRecordRepository { get; }
        IGenericRepository<Patient> PatientRepository { get; }
        IGenericRepository<Prescription> PrescriptionRepository { get; }
        IGenericRepository<PrescriptionDetail> PrescriptionDetailRepository { get; }
        IGenericRepository<Profile> ProfileRepository { get; }
        IGenericRepository<Role> RoleRepository { get; }
        IGenericRepository<Schedule> ScheduleRepository { get; }
        IGenericRepository<Service> ServiceRepository { get; }
        IGenericRepository<Specialty> SpecialtyRepository { get; }
        IGenericRepository<Symptom> SymptomRepository { get; }
        IGenericRepository<SymptomDetail> SymptomDetailRepository { get; }
        IGenericRepository<Transaction> TransactionRepository { get; }
        IGenericRepository<User> UserGenRepository { get; }
        IGenericRepository<Disease> DiseaseRepository { get; }
        IGenericRepository<AppConfig> AppConfigRepository { get; }
        IUserRepository UserRepository { get; }
        IPatientRepository PatientRepositorySep { get; set; }
        IDoctorRepository DoctorRepositorySep { get; set; }
        IGenericRepository<ExaminationHistory> ExaminationHistoryRepository { get; }
        ITransactionRepository TransactionRepositorySep { get; }
        IPrescriptionRepository PrescriptionRepositorySep { get; set; }
        IDiseaseRepository DiseaseRepositorySep { get; set; }
        IScheduleRepository ScheduleRepositorySep { get; set; }

        Task<int> SaveAsync();
    }
}
