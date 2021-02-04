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
        IGenericRepository<Family> FamilyRepository { get; }
        IGenericRepository<FamilyDetail> FamilyDetailRepository { get; }
        IGenericRepository<HealthRecord> HealthRecordRepository { get; }
        IGenericRepository<Patient> PatientRepository { get; }
        IGenericRepository<Prescription> PrescriptionRepository { get; }
        IGenericRepository<PrescriptionDetail> PrescriptionDetailRepository { get; }
        IGenericRepository<Profile> ProfileRepository { get; }
        IGenericRepository<Role> RoleRepository { get; }
        IGenericRepository<Schedule> ScheduleRepository { get; }
        IGenericRepository<Service> ServiceRepository { get; }
        IGenericRepository<ServiceDetail> ServiceDetailRepository { get; }
        IGenericRepository<Specialty> SpecialtyRepository { get; }
        IGenericRepository<Symptom> SymptomRepository { get; }
        IGenericRepository<Transaction> TransactionRepository { get; }
        IGenericRepository<User> UserGenRepository { get; }
        IUserRepository UserRepository { get; }
        IPatientRepository PatientRepositorySep { get; set; }


        Task<int> SaveAsync();
    }
}
