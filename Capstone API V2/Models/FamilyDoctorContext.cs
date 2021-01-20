using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Capstone_API_V2.Models
{
    public partial class FamilyDoctorContext : DbContext
    {
        public FamilyDoctorContext()
        {
        }

        public FamilyDoctorContext(DbContextOptions<FamilyDoctorContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<Family> Families { get; set; }
        public virtual DbSet<FamilyDetail> FamilyDetails { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<HealthRecord> HealthRecords { get; set; }
        public virtual DbSet<Medicine> Medicines { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Prescription> Prescriptions { get; set; }
        public virtual DbSet<PrescriptionDetail> PrescriptionDetails { get; set; }
        public virtual DbSet<Profile> Profiles { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<ServiceDetail> ServiceDetails { get; set; }
        public virtual DbSet<Specialty> Specialties { get; set; }
        public virtual DbSet<Symptom> Symptoms { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=Capstone_DB");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.ToTable("Doctor");

                entity.Property(e => e.DoctorId)
                    .HasColumnName("doctor_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Degree)
                    .HasColumnName("degree")
                    .HasMaxLength(50);

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(50);

                entity.Property(e => e.Experience)
                    .HasColumnName("experience")
                    .HasMaxLength(50);

                entity.Property(e => e.ProfileId)
                    .HasColumnName("profile_id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.SpecialtyId).HasColumnName("specialty_id");

                entity.HasOne(d => d.Profile)
                    .WithMany(p => p.Doctors)
                    .HasForeignKey(d => d.ProfileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Doctors_Profile");

                entity.HasOne(d => d.Specialty)
                    .WithMany(p => p.Doctors)
                    .HasForeignKey(d => d.SpecialtyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Doctors_Specialties");
            });

            modelBuilder.Entity<Family>(entity =>
            {
                entity.ToTable("Family");

                entity.Property(e => e.FamilyId).HasColumnName("family_id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<FamilyDetail>(entity =>
            {
                entity.ToTable("FamilyDetail");

                entity.Property(e => e.FamilyDetailId).HasColumnName("family_detail_id");

                entity.Property(e => e.Disabled).HasColumnName("disabled");

                entity.Property(e => e.FamilyId).HasColumnName("family_id");

                entity.Property(e => e.PatientId).HasColumnName("patient_id");

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(12);

                entity.Property(e => e.Relationship)
                    .HasColumnName("relationship")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Family)
                    .WithMany(p => p.FamilyDetails)
                    .HasForeignKey(d => d.FamilyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FamilyDetail_Families");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.FamilyDetails)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FamilyDetail_Patients");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.ToTable("Feedback");

                entity.Property(e => e.FeedbackId).HasColumnName("feedback_id");

                entity.Property(e => e.DoctorId).HasColumnName("doctor_id");

                entity.Property(e => e.Note)
                    .HasColumnName("note")
                    .HasMaxLength(50);

                entity.Property(e => e.PatientId).HasColumnName("patient_id");

                entity.Property(e => e.RatingPoint).HasColumnName("rating_point");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK_Feedback_Doctors");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("FK_Feedback_Patients");
            });

            modelBuilder.Entity<HealthRecord>(entity =>
            {
                entity.HasKey(e => e.RecordId);

                entity.ToTable("HealthRecord");

                entity.Property(e => e.RecordId).HasColumnName("record_id");

                entity.Property(e => e.PatientId).HasColumnName("patient_id");

                entity.Property(e => e.SymptomId).HasColumnName("symptom_id");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.HealthRecords)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("FK_HealthRecord_Patients");

                entity.HasOne(d => d.Symptom)
                    .WithMany(p => p.HealthRecords)
                    .HasForeignKey(d => d.SymptomId)
                    .HasConstraintName("FK_HealthRecord_Symptoms");
            });

            modelBuilder.Entity<Medicine>(entity =>
            {
                entity.HasKey(e => e.DrugId)
                    .HasName("PK_Drug_1");

                entity.ToTable("Medicine");

                entity.HasIndex(e => e.Name)
                    .HasName("UC_Drugs")
                    .IsUnique();

                entity.Property(e => e.DrugId).HasColumnName("drug_id");

                entity.Property(e => e.Disabled).HasColumnName("disabled");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.ToTable("Patient");

                entity.Property(e => e.PatientId).HasColumnName("patient_id");

                entity.Property(e => e.BloodType)
                    .HasColumnName("blood_type")
                    .HasMaxLength(3);

                entity.Property(e => e.Height).HasColumnName("height");

                entity.Property(e => e.ProfileId).HasColumnName("profile_id");

                entity.Property(e => e.Weight).HasColumnName("weight");

                entity.HasOne(d => d.Profile)
                    .WithMany(p => p.Patients)
                    .HasForeignKey(d => d.ProfileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Patients_Profile");
            });

            modelBuilder.Entity<Prescription>(entity =>
            {
                entity.ToTable("Prescription");

                entity.Property(e => e.PrescriptionId).HasColumnName("prescription_id");

                entity.Property(e => e.Description).HasColumnName("description");
            });

            modelBuilder.Entity<PrescriptionDetail>(entity =>
            {
                entity.ToTable("PrescriptionDetail");

                entity.Property(e => e.PrescriptionDetailId).HasColumnName("prescription_detail_id");

                entity.Property(e => e.DrugId).HasColumnName("drug_id");

                entity.Property(e => e.PrescriptionId).HasColumnName("prescription_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Drug)
                    .WithMany(p => p.PrescriptionDetails)
                    .HasForeignKey(d => d.DrugId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PrescriptionDetail_Drug1");

                entity.HasOne(d => d.Prescription)
                    .WithMany(p => p.PrescriptionDetails)
                    .HasForeignKey(d => d.PrescriptionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PrescriptionDetail_Prescriptions");
            });

            modelBuilder.Entity<Profile>(entity =>
            {
                entity.ToTable("Profile");

                entity.Property(e => e.ProfileId).HasColumnName("profile_id");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.Address).HasColumnName("address");

                entity.Property(e => e.Birthday)
                    .HasColumnName("birthday")
                    .HasColumnType("date");

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasColumnName("full_name")
                    .HasMaxLength(50);

                entity.Property(e => e.Gender)
                    .HasColumnName("gender")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Image).HasColumnName("image");

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(12)
                    .IsFixedLength();

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Profiles)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Profile_Accounts");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleId)
                    .HasColumnName("role_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(50);

                entity.Property(e => e.Disabled).HasColumnName("disabled");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.ToTable("Schedule");

                entity.Property(e => e.ScheduleId).HasColumnName("schedule_id");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("date");

                entity.Property(e => e.Disabled).HasColumnName("disabled");

                entity.Property(e => e.DoctorId).HasColumnName("doctor_id");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Time).HasColumnName("time");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK_Schedule_Doctors");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.ToTable("Service");

                entity.Property(e => e.ServiceId).HasColumnName("service_id");

                entity.Property(e => e.Disabled).HasColumnName("disabled");

                entity.Property(e => e.ServiceDescription)
                    .HasColumnName("service_description")
                    .HasMaxLength(50);

                entity.Property(e => e.ServiceName)
                    .HasColumnName("service_name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ServiceDetail>(entity =>
            {
                entity.ToTable("ServiceDetail");

                entity.Property(e => e.ServiceDetailId).HasColumnName("service_detail_id");

                entity.Property(e => e.AcceptedDate)
                    .HasColumnName("accepted_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.DoctorId).HasColumnName("doctor_id");

                entity.Property(e => e.FamilyId).HasColumnName("family_id");

                entity.Property(e => e.ServiceId).HasColumnName("service_id");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.ServiceDetails)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ServiceDetail_Doctors");

                entity.HasOne(d => d.Family)
                    .WithMany(p => p.ServiceDetails)
                    .HasForeignKey(d => d.FamilyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ServiceDetail_Families");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.ServiceDetails)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ServiceDetail_Services");
            });

            modelBuilder.Entity<Specialty>(entity =>
            {
                entity.ToTable("Specialty");

                entity.Property(e => e.SpecialtyId).HasColumnName("specialty_id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Disabled).HasColumnName("disabled");

                entity.Property(e => e.Image).HasColumnName("image");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Symptom>(entity =>
            {
                entity.ToTable("Symptom");

                entity.Property(e => e.SymptomId).HasColumnName("symptom_id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Disabled).HasColumnName("disabled");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("Transaction");

                entity.Property(e => e.TransactionId)
                    .HasColumnName("transaction_id")
                    .HasMaxLength(50);

                entity.Property(e => e.DateEnd)
                    .HasColumnName("date_end")
                    .HasColumnType("datetime");

                entity.Property(e => e.DateStart)
                    .HasColumnName("date_start")
                    .HasColumnType("datetime");

                entity.Property(e => e.Disabled).HasColumnName("disabled");

                entity.Property(e => e.DoctorId).HasColumnName("doctor_id");

                entity.Property(e => e.Location)
                    .HasColumnName("location")
                    .HasMaxLength(50);

                entity.Property(e => e.Note).HasColumnName("note");

                entity.Property(e => e.PatientId).HasColumnName("patient_id");

                entity.Property(e => e.PrescriptionId).HasColumnName("prescription_id");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK_Transactions_Doctors");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("FK_Transactions_Patients");

                entity.HasOne(d => d.Prescription)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.PrescriptionId)
                    .HasConstraintName("FK_Transactions_Prescriptions");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.AccountId)
                    .HasName("PK_Accounts");

                entity.ToTable("User");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.Disabled).HasColumnName("disabled");

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(50);

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Accounts_Roles");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
