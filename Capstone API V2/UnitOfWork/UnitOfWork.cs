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

        public IGenericRepository<Family> FamilyRepository { get; set; }

        public IGenericRepository<FamilyDetail> FamilyDetailRepository { get; set; }

        public IGenericRepository<HealthRecord> HealthRecordRepository { get; set; }

        public IGenericRepository<Patient> PatientRepository { get; set; }

        public IGenericRepository<Prescription> PrescriptionRepository { get; set; }

        public IGenericRepository<PrescriptionDetail> PrescriptionDetailRepository { get; set; }

        public IGenericRepository<Profile> ProfileRepository { get; set; }

        public IGenericRepository<Role> RoleRepository { get; set; }
        public IGenericRepository<Schedule> ScheduleRepository { get; set; }

        public IGenericRepository<Service> ServiceRepository { get; set; }

        public IGenericRepository<ServiceDetail> ServiceDetailRepository { get; set; }

        public IGenericRepository<Specialty> SpecialtyRepository { get; set; }

        public IGenericRepository<Symptom> SymptomRepository { get; set; }

        public IGenericRepository<Transaction> TransactionRepository { get; set; }

        public IGenericRepository<User> UserGenRepository { get; set; }

        public IUserRepository UserRepository { get; set; }

        public IPatientRepository PatientRepositorySep { get; set; }


        private void InitRepository()
        {
            MedicineRepository = new GenericRepository<Medicine>(_context);
            DoctorRepository = new GenericRepository<Doctor>(_context);
            FeedbackRepository = new GenericRepository<Feedback>(_context);
            FamilyRepository = new GenericRepository<Family>(_context);
            FamilyDetailRepository = new GenericRepository<FamilyDetail>(_context);
            HealthRecordRepository = new GenericRepository<HealthRecord>(_context);
            PatientRepository = new GenericRepository<Patient>(_context);
            PrescriptionRepository = new GenericRepository<Prescription>(_context);
            PrescriptionDetailRepository = new GenericRepository<PrescriptionDetail>(_context);
            ProfileRepository = new GenericRepository<Profile>(_context);
            RoleRepository = new GenericRepository<Role>(_context);
            ScheduleRepository = new GenericRepository<Schedule>(_context);
            ServiceRepository = new GenericRepository<Service>(_context);
            ServiceDetailRepository = new GenericRepository<ServiceDetail>(_context);
            SpecialtyRepository = new GenericRepository<Specialty>(_context);
            SymptomRepository = new GenericRepository<Symptom>(_context);
            TransactionRepository = new GenericRepository<Transaction>(_context);
            UserRepository = new UserRepository(_context);
            PatientRepositorySep = new PatientRepository(_context);
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
