using System;
using System.Threading.Tasks;
using Capstone_API_V2.Models;
using Capstone_API_V2.Repositories;

namespace Capstone_API_V2.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private FamilyDoctorContext _context;

        public UnitOfWork(FamilyDoctorContext context)
        {
            _context = context;
            InitRepository();
        }

        private bool _disposed = false;

        public IGenericRepository<Medicine> MedicineRepository { get; set; }

        public IGenericRepository<Doctor> DoctorRepository { get; set; }

        public IGenericRepository<Feedback> FeedbackRepository { get; set; }

        public IGenericRepository<HealthRecord> HealthRecordRepository { get; set; }

        public IGenericRepository<Patient> PatientRepository { get; set; }

        public IGenericRepository<Prescription> PrescriptionRepository { get; set; }

        public IGenericRepository<PrescriptionDetail> PrescriptionDetailRepository { get; set; }

        public IGenericRepository<Profile> ProfileRepository { get; set; }

        public IGenericRepository<Role> RoleRepository { get; set; }
        public IGenericRepository<Schedule> ScheduleRepository { get; set; }

        public IGenericRepository<Service> ServiceRepository { get; set; }

        public IGenericRepository<Specialty> SpecialtyRepository { get; set; }

        public IGenericRepository<Symptom> SymptomRepository { get; set; }

        public IGenericRepository<SymptomDetail> SymptomDetailRepository { get; set; }

        public IGenericRepository<Treatment> TransactionRepository { get; set; }

        public IGenericRepository<Account> UserGenRepository { get; set; }

        public IGenericRepository<Disease> DiseaseRepository { get; set; }

        public IGenericRepository<AppConfig> AppConfigRepository { get; set; }

        public IUserRepository UserRepository { get; set; }

        public IPatientRepository PatientRepositorySep { get; set; }

        public IDoctorRepository DoctorRepositorySep { get; set; }

        public IGenericRepository<ExaminationHistory> ExaminationHistoryRepository { get; set; }

        public ITransactionRepository TransactionRepositorySep { get; set; }

        public IPrescriptionRepository PrescriptionRepositorySep { get ; set; }

        public IDiseaseRepository DiseaseRepositorySep { get; set; }

        public IScheduleRepository ScheduleRepositorySep { get; set; }

        private void InitRepository()
        {
            MedicineRepository = new GenericRepository<Medicine>(_context);
            DoctorRepository = new GenericRepository<Doctor>(_context);
            FeedbackRepository = new GenericRepository<Feedback>(_context);
            HealthRecordRepository = new GenericRepository<HealthRecord>(_context);
            PatientRepository = new GenericRepository<Patient>(_context);
            PrescriptionRepository = new GenericRepository<Prescription>(_context);
            PrescriptionDetailRepository = new GenericRepository<PrescriptionDetail>(_context);
            ProfileRepository = new GenericRepository<Profile>(_context);
            RoleRepository = new GenericRepository<Role>(_context);
            ScheduleRepository = new GenericRepository<Schedule>(_context);
            ServiceRepository = new GenericRepository<Service>(_context);
            SpecialtyRepository = new GenericRepository<Specialty>(_context);
            SymptomRepository = new GenericRepository<Symptom>(_context);
            SymptomDetailRepository = new GenericRepository<SymptomDetail>(_context);
            TransactionRepository = new GenericRepository<Treatment>(_context);
            UserGenRepository = new GenericRepository<Account>(_context);
            AppConfigRepository = new GenericRepository<AppConfig>(_context);
            UserRepository = new UserRepository(_context);
            PatientRepositorySep = new PatientRepository(_context);
            DoctorRepositorySep = new DoctorRepository(_context);
            ExaminationHistoryRepository = new GenericRepository<ExaminationHistory>(_context);
            TransactionRepositorySep = new TransactionRepository(_context);
            PrescriptionRepositorySep = new PrescriptionRepository(_context);
            DiseaseRepository = new GenericRepository<Disease>(_context);
            DiseaseRepositorySep = new DiseaseRepository(_context);
            ScheduleRepositorySep = new ScheduleRepository(_context);
        }

    protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
