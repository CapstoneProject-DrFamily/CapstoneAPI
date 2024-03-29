﻿using System;
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

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<AppConfig> AppConfigs { get; set; }
        public virtual DbSet<Disease> Diseases { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<ExaminationHistory> ExaminationHistories { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<HealthRecord> HealthRecords { get; set; }
        public virtual DbSet<Medicine> Medicines { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Prescription> Prescriptions { get; set; }
        public virtual DbSet<PrescriptionDetail> PrescriptionDetails { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<Specialty> Specialties { get; set; }
        public virtual DbSet<Treatment> Treatments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=Capstone_DB");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Disabled).HasColumnName("disabled");

                entity.Property(e => e.InsBy)
                    .HasColumnName("insBy")
                    .HasMaxLength(50);

                entity.Property(e => e.InsDatetime)
                    .HasColumnName("insDatetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.NotiToken).HasColumnName("notiToken");

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(50);

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.UpdBy)
                    .HasColumnName("updBy")
                    .HasMaxLength(50);

                entity.Property(e => e.UpdDatetime)
                    .HasColumnName("updDatetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(50);

                entity.Property(e => e.Waiting).HasColumnName("waiting");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Accounts_Roles");
            });

            modelBuilder.Entity<AppConfig>(entity =>
            {
                entity.HasKey(e => e.AppId);

                entity.ToTable("AppConfig");

                entity.Property(e => e.AppId).HasColumnName("app_id");

                entity.Property(e => e.AppName)
                    .HasColumnName("app_name")
                    .HasMaxLength(15);

                entity.Property(e => e.ConfigValue).HasColumnName("config_value");

                entity.Property(e => e.InsBy)
                    .HasColumnName("insBy")
                    .HasMaxLength(50);

                entity.Property(e => e.InsDatetime)
                    .HasColumnName("insDatetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdBy)
                    .HasColumnName("updBy")
                    .HasMaxLength(50);

                entity.Property(e => e.UpdDatetime)
                    .HasColumnName("updDatetime")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<Disease>(entity =>
            {
                entity.HasKey(e => e.DiseaseCode)
                    .HasName("PK_Disease1");

                entity.ToTable("Disease");

                entity.Property(e => e.DiseaseCode)
                    .HasColumnName("disease_code")
                    .HasMaxLength(255);

                entity.Property(e => e.ChapterCode)
                    .HasColumnName("chapter_code")
                    .HasMaxLength(255);

                entity.Property(e => e.ChapterName)
                    .HasColumnName("chapter_name")
                    .HasMaxLength(255);

                entity.Property(e => e.Disabled).HasColumnName("disabled");

                entity.Property(e => e.DiseaseName)
                    .HasColumnName("disease_name")
                    .HasMaxLength(255);

                entity.Property(e => e.InsBy)
                    .HasColumnName("insBy")
                    .HasMaxLength(50);

                entity.Property(e => e.InsDatetime)
                    .HasColumnName("insDatetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.MainGroupCode)
                    .HasColumnName("main_group_code")
                    .HasMaxLength(255);

                entity.Property(e => e.MainGroupName)
                    .HasColumnName("main_group_name")
                    .HasMaxLength(255);

                entity.Property(e => e.TypeCode)
                    .HasColumnName("type_code")
                    .HasMaxLength(255);

                entity.Property(e => e.TypeName)
                    .HasColumnName("type_name")
                    .HasMaxLength(255);

                entity.Property(e => e.UpdBy)
                    .HasColumnName("updBy")
                    .HasMaxLength(50);

                entity.Property(e => e.UpdDatetime)
                    .HasColumnName("updDatetime")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.ToTable("Doctor");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Birthday)
                    .HasColumnName("birthday")
                    .HasColumnType("date");

                entity.Property(e => e.Degree).HasColumnName("degree");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Disabled).HasColumnName("disabled");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(100);

                entity.Property(e => e.Experience)
                    .HasColumnName("experience")
                    .HasMaxLength(50);

                entity.Property(e => e.Fullname)
                    .HasColumnName("fullname")
                    .HasMaxLength(50);

                entity.Property(e => e.Gender)
                    .HasColumnName("gender")
                    .HasMaxLength(50);

                entity.Property(e => e.IdCard)
                    .HasColumnName("id_card")
                    .HasMaxLength(50);

                entity.Property(e => e.Image).HasColumnName("image");

                entity.Property(e => e.InsBy)
                    .HasColumnName("insBy")
                    .HasMaxLength(50);

                entity.Property(e => e.InsDatetime)
                    .HasColumnName("insDatetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.School).HasColumnName("school");

                entity.Property(e => e.SpecialtyId).HasColumnName("specialty_id");

                entity.Property(e => e.UpdBy)
                    .HasColumnName("updBy")
                    .HasMaxLength(50);

                entity.Property(e => e.UpdDatetime)
                    .HasColumnName("updDatetime")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.Doctor)
                    .HasForeignKey<Doctor>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Doctor_Account");

                entity.HasOne(d => d.Specialty)
                    .WithMany(p => p.Doctors)
                    .HasForeignKey(d => d.SpecialtyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Doctors_Specialties");
            });

            modelBuilder.Entity<ExaminationHistory>(entity =>
            {
                entity.ToTable("ExaminationHistory");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(50);

                entity.Property(e => e.Activity)
                    .HasColumnName("activity")
                    .HasMaxLength(50);

                entity.Property(e => e.Advisory).HasColumnName("advisory");

                entity.Property(e => e.BloodPressure)
                    .HasColumnName("blood_pressure")
                    .HasMaxLength(7);

                entity.Property(e => e.BloodTest).HasColumnName("blood_test");

                entity.Property(e => e.Cardiovascular)
                    .HasColumnName("cardiovascular")
                    .HasMaxLength(50);

                entity.Property(e => e.Conclusion).HasColumnName("conclusion");

                entity.Property(e => e.Dermatology)
                    .HasColumnName("dermatology")
                    .HasMaxLength(50);

                entity.Property(e => e.Endocrine)
                    .HasColumnName("endocrine")
                    .HasMaxLength(50);

                entity.Property(e => e.Evaluation)
                    .HasColumnName("evaluation")
                    .HasMaxLength(50);

                entity.Property(e => e.Gastroenterology)
                    .HasColumnName("gastroenterology")
                    .HasMaxLength(50);

                entity.Property(e => e.Height).HasColumnName("height");

                entity.Property(e => e.History).HasColumnName("history");

                entity.Property(e => e.InsBy)
                    .HasColumnName("insBy")
                    .HasMaxLength(50);

                entity.Property(e => e.InsDatetime)
                    .HasColumnName("insDatetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.LeftEye).HasColumnName("left_eye");

                entity.Property(e => e.LeftEyeGlassed).HasColumnName("left_eye_glassed");

                entity.Property(e => e.Mental)
                    .HasColumnName("mental")
                    .HasMaxLength(50);

                entity.Property(e => e.Mucosa)
                    .HasColumnName("mucosa")
                    .HasMaxLength(50);

                entity.Property(e => e.Nephrology)
                    .HasColumnName("nephrology")
                    .HasMaxLength(50);

                entity.Property(e => e.Nerve)
                    .HasColumnName("nerve")
                    .HasMaxLength(50);

                entity.Property(e => e.Nutrition)
                    .HasColumnName("nutrition")
                    .HasMaxLength(50);

                entity.Property(e => e.ObstetricsGynecology)
                    .HasColumnName("obstetrics_gynecology")
                    .HasMaxLength(50);

                entity.Property(e => e.OdontoStomatology)
                    .HasColumnName("odonto_stomatology")
                    .HasMaxLength(50);

                entity.Property(e => e.Ophthalmology)
                    .HasColumnName("ophthalmology")
                    .HasMaxLength(50);

                entity.Property(e => e.OtherBody).HasColumnName("other_body");

                entity.Property(e => e.Otorhinolaryngology)
                    .HasColumnName("otorhinolaryngology")
                    .HasMaxLength(50);

                entity.Property(e => e.PulseRate).HasColumnName("pulse_rate");

                entity.Property(e => e.Respiratory)
                    .HasColumnName("respiratory")
                    .HasMaxLength(50);

                entity.Property(e => e.RespiratoryRate).HasColumnName("respiratory_rate");

                entity.Property(e => e.Rheumatology)
                    .HasColumnName("rheumatology")
                    .HasMaxLength(50);

                entity.Property(e => e.RightEye).HasColumnName("right_eye");

                entity.Property(e => e.RightEyeGlassed).HasColumnName("right_eye_glassed");

                entity.Property(e => e.Surgery)
                    .HasColumnName("surgery")
                    .HasMaxLength(50);

                entity.Property(e => e.Temperature).HasColumnName("temperature");

                entity.Property(e => e.UpdBy)
                    .HasColumnName("updBy")
                    .HasMaxLength(50);

                entity.Property(e => e.UpdDatetime)
                    .HasColumnName("updDatetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.UrineTest).HasColumnName("urine_test");

                entity.Property(e => e.WaistCircumference).HasColumnName("waist_circumference");

                entity.Property(e => e.Weight).HasColumnName("weight");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.ExaminationHistory)
                    .HasForeignKey<ExaminationHistory>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExaminationHistory_Transaction");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.ToTable("Feedback");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(50);

                entity.Property(e => e.InsBy)
                    .HasColumnName("insBy")
                    .HasMaxLength(50);

                entity.Property(e => e.InsDatetime)
                    .HasColumnName("insDatetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.Note).HasColumnName("note");

                entity.Property(e => e.RatingPoint).HasColumnName("rating_point");

                entity.Property(e => e.UpdBy)
                    .HasColumnName("updBy")
                    .HasMaxLength(50);

                entity.Property(e => e.UpdDatetime)
                    .HasColumnName("updDatetime")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.Feedback)
                    .HasForeignKey<Feedback>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Feedback_Transaction");
            });

            modelBuilder.Entity<HealthRecord>(entity =>
            {
                entity.ToTable("HealthRecord");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ActivityFrequency)
                    .HasColumnName("activity_frequency")
                    .HasMaxLength(50);

                entity.Property(e => e.BirthDefects)
                    .HasColumnName("birth_defects")
                    .HasMaxLength(50);

                entity.Property(e => e.BirthHeight).HasColumnName("birth_height");

                entity.Property(e => e.BirthWeight).HasColumnName("birth_weight");

                entity.Property(e => e.Cancer)
                    .HasColumnName("cancer")
                    .HasMaxLength(50);

                entity.Property(e => e.CancerFamily)
                    .HasColumnName("cancer_family")
                    .HasMaxLength(50);

                entity.Property(e => e.ChemicalAllergy)
                    .HasColumnName("chemical_allergy")
                    .HasMaxLength(50);

                entity.Property(e => e.ChemicalAllergyFamily)
                    .HasColumnName("chemical_allergy_family")
                    .HasMaxLength(50);

                entity.Property(e => e.CleftLip)
                    .HasColumnName("cleft_lip")
                    .HasMaxLength(50);

                entity.Property(e => e.ConditionAtBirth)
                    .HasColumnName("condition_at_birth")
                    .HasMaxLength(50);

                entity.Property(e => e.ContactTime)
                    .HasColumnName("contact_time")
                    .HasMaxLength(50);

                entity.Property(e => e.Disable).HasColumnName("disable");

                entity.Property(e => e.Disease)
                    .HasColumnName("disease")
                    .HasMaxLength(50);

                entity.Property(e => e.DiseaseFamily)
                    .HasColumnName("disease_family")
                    .HasMaxLength(50);

                entity.Property(e => e.DrinkingFrequency)
                    .HasColumnName("drinking_frequency")
                    .HasMaxLength(50);

                entity.Property(e => e.DrugFrequency)
                    .HasColumnName("drug_frequency")
                    .HasMaxLength(50);

                entity.Property(e => e.ExposureElement)
                    .HasColumnName("exposure_element")
                    .HasMaxLength(50);

                entity.Property(e => e.Eyesight)
                    .HasColumnName("eyesight")
                    .HasMaxLength(50);

                entity.Property(e => e.FoodAllergy)
                    .HasColumnName("food_allergy")
                    .HasMaxLength(50);

                entity.Property(e => e.FoodAllergyFamily)
                    .HasColumnName("food_allergy_family")
                    .HasMaxLength(50);

                entity.Property(e => e.Hand)
                    .HasColumnName("hand")
                    .HasMaxLength(50);

                entity.Property(e => e.Hearing)
                    .HasColumnName("hearing")
                    .HasMaxLength(50);

                entity.Property(e => e.InsBy)
                    .HasColumnName("insBy")
                    .HasMaxLength(50);

                entity.Property(e => e.InsDatetime)
                    .HasColumnName("insDatetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.Leg)
                    .HasColumnName("leg")
                    .HasMaxLength(50);

                entity.Property(e => e.MedicineAllergy)
                    .HasColumnName("medicine_allergy")
                    .HasMaxLength(50);

                entity.Property(e => e.MedicineAllergyFamily)
                    .HasColumnName("medicine_allergy_family")
                    .HasMaxLength(50);

                entity.Property(e => e.Other)
                    .HasColumnName("other")
                    .HasMaxLength(50);

                entity.Property(e => e.OtherAllergy)
                    .HasColumnName("other_allergy")
                    .HasMaxLength(50);

                entity.Property(e => e.OtherAllergyFamily)
                    .HasColumnName("other_allergy_family")
                    .HasMaxLength(50);

                entity.Property(e => e.OtherDefects)
                    .HasColumnName("other_defects")
                    .HasMaxLength(50);

                entity.Property(e => e.OtherDisabilities)
                    .HasColumnName("other_disabilities")
                    .HasMaxLength(50);

                entity.Property(e => e.OtherDiseases)
                    .HasColumnName("other_diseases")
                    .HasMaxLength(50);

                entity.Property(e => e.OtherDiseasesFamily)
                    .HasColumnName("other_diseases_family")
                    .HasMaxLength(50);

                entity.Property(e => e.OtherRisks)
                    .HasColumnName("other_risks")
                    .HasMaxLength(50);

                entity.Property(e => e.PatientId).HasColumnName("patient_id");

                entity.Property(e => e.Scoliosis)
                    .HasColumnName("scoliosis")
                    .HasMaxLength(50);

                entity.Property(e => e.SmokingFrequency)
                    .HasColumnName("smoking_frequency")
                    .HasMaxLength(50);

                entity.Property(e => e.SurgeryHistory)
                    .HasColumnName("surgery_history")
                    .HasMaxLength(50);

                entity.Property(e => e.ToiletType)
                    .HasColumnName("toilet_type")
                    .HasMaxLength(50);

                entity.Property(e => e.Tuberculosis)
                    .HasColumnName("tuberculosis")
                    .HasMaxLength(50);

                entity.Property(e => e.TuberculosisFamily)
                    .HasColumnName("tuberculosis_family")
                    .HasMaxLength(50);

                entity.Property(e => e.UpdBy)
                    .HasColumnName("updBy")
                    .HasMaxLength(50);

                entity.Property(e => e.UpdDatetime)
                    .HasColumnName("updDatetime")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.HealthRecords)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HealthRecord_Patient");
            });

            modelBuilder.Entity<Medicine>(entity =>
            {
                entity.ToTable("Medicine");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ActiveIngredient)
                    .HasColumnName("activeIngredient")
                    .HasMaxLength(255);

                entity.Property(e => e.Disabled).HasColumnName("disabled");

                entity.Property(e => e.Form)
                    .HasColumnName("form")
                    .HasMaxLength(255);

                entity.Property(e => e.InsBy)
                    .HasColumnName("insBy")
                    .HasMaxLength(50);

                entity.Property(e => e.InsDatetime)
                    .HasColumnName("insDatetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255);

                entity.Property(e => e.Strength)
                    .HasColumnName("strength")
                    .HasMaxLength(255);

                entity.Property(e => e.UpdBy)
                    .HasColumnName("updBy")
                    .HasMaxLength(50);

                entity.Property(e => e.UpdDatetime)
                    .HasColumnName("updDatetime")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.ToTable("Patient");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.Birthday)
                    .HasColumnName("birthday")
                    .HasColumnType("date");

                entity.Property(e => e.BloodType)
                    .HasColumnName("blood_type")
                    .HasMaxLength(3);

                entity.Property(e => e.Disabled).HasColumnName("disabled");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(100);

                entity.Property(e => e.Fullname)
                    .HasColumnName("fullname")
                    .HasMaxLength(50);

                entity.Property(e => e.Gender)
                    .HasColumnName("gender")
                    .HasMaxLength(50);

                entity.Property(e => e.Height).HasColumnName("height");

                entity.Property(e => e.IdCard)
                    .HasColumnName("id_card")
                    .HasMaxLength(50);

                entity.Property(e => e.Image).HasColumnName("image");

                entity.Property(e => e.InsBy)
                    .HasColumnName("insBy")
                    .HasMaxLength(50);

                entity.Property(e => e.InsDatetime)
                    .HasColumnName("insDatetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.Location).HasColumnName("location");

                entity.Property(e => e.Relationship)
                    .HasColumnName("relationship")
                    .HasMaxLength(50);

                entity.Property(e => e.UpdBy)
                    .HasColumnName("updBy")
                    .HasMaxLength(50);

                entity.Property(e => e.UpdDatetime)
                    .HasColumnName("updDatetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.Weight).HasColumnName("weight");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Patients)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Patient_Account");
            });

            modelBuilder.Entity<Prescription>(entity =>
            {
                entity.ToTable("Prescription");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(50);

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.DiseaseId)
                    .HasColumnName("disease_id")
                    .HasMaxLength(255);

                entity.Property(e => e.InsBy)
                    .HasColumnName("insBy")
                    .HasMaxLength(50);

                entity.Property(e => e.InsDatetime)
                    .HasColumnName("insDatetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdBy)
                    .HasColumnName("updBy")
                    .HasMaxLength(50);

                entity.Property(e => e.UpdDatetime)
                    .HasColumnName("updDatetime")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Disease)
                    .WithMany(p => p.Prescriptions)
                    .HasForeignKey(d => d.DiseaseId)
                    .HasConstraintName("FK_Prescription_Disease");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.Prescription)
                    .HasForeignKey<Prescription>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Prescription_Transaction");
            });

            modelBuilder.Entity<PrescriptionDetail>(entity =>
            {
                entity.ToTable("PrescriptionDetail");

                entity.Property(e => e.PrescriptionDetailId).HasColumnName("prescription_detail_id");

                entity.Property(e => e.AfternoonQuantity).HasColumnName("afternoon_quantity");

                entity.Property(e => e.MedicineId).HasColumnName("medicine_id");

                entity.Property(e => e.Method)
                    .HasColumnName("method")
                    .HasMaxLength(15);

                entity.Property(e => e.MorningQuantity).HasColumnName("morning_quantity");

                entity.Property(e => e.NoonQuantity).HasColumnName("noon_quantity");

                entity.Property(e => e.PrescriptionId)
                    .IsRequired()
                    .HasColumnName("prescription_id")
                    .HasMaxLength(50);

                entity.Property(e => e.TotalDays).HasColumnName("total_days");

                entity.Property(e => e.TotalQuantity).HasColumnName("total_quantity");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnName("type")
                    .HasMaxLength(15);

                entity.HasOne(d => d.Medicine)
                    .WithMany(p => p.PrescriptionDetails)
                    .HasForeignKey(d => d.MedicineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PrescriptionDetail_Medicine");

                entity.HasOne(d => d.Prescription)
                    .WithMany(p => p.PrescriptionDetails)
                    .HasForeignKey(d => d.PrescriptionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PrescriptionDetail_Prescriptions");
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

                entity.Property(e => e.InsBy)
                    .HasColumnName("insBy")
                    .HasMaxLength(50);

                entity.Property(e => e.InsDatetime)
                    .HasColumnName("insDatetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.UpdBy)
                    .HasColumnName("updBy")
                    .HasMaxLength(50);

                entity.Property(e => e.UpdDatetime)
                    .HasColumnName("updDatetime")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.ToTable("Schedule");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AppointmentTime)
                    .HasColumnName("appointment_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.Disabled).HasColumnName("disabled");

                entity.Property(e => e.DoctorId).HasColumnName("doctor_id");

                entity.Property(e => e.InsBy)
                    .HasColumnName("insBy")
                    .HasMaxLength(50);

                entity.Property(e => e.InsDatetime)
                    .HasColumnName("insDatetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UpdBy)
                    .HasColumnName("updBy")
                    .HasMaxLength(50);

                entity.Property(e => e.UpdDatetime)
                    .HasColumnName("updDatetime")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Schedule_Doctors");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.ToTable("Service");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(50);

                entity.Property(e => e.Disabled).HasColumnName("disabled");

                entity.Property(e => e.Image).HasColumnName("image");

                entity.Property(e => e.InsBy)
                    .HasColumnName("insBy")
                    .HasMaxLength(50);

                entity.Property(e => e.InsDatetime)
                    .HasColumnName("insDatetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsDefault).HasColumnName("isDefault");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(19, 4)");

                entity.Property(e => e.SpecialtyId).HasColumnName("specialty_id");

                entity.Property(e => e.UpdBy)
                    .HasColumnName("updBy")
                    .HasMaxLength(50);

                entity.Property(e => e.UpdDatetime)
                    .HasColumnName("updDatetime")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Specialty)
                    .WithMany(p => p.Services)
                    .HasForeignKey(d => d.SpecialtyId)
                    .HasConstraintName("FK_Service_Specialty");
            });

            modelBuilder.Entity<Specialty>(entity =>
            {
                entity.ToTable("Specialty");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Disabled).HasColumnName("disabled");

                entity.Property(e => e.Image).HasColumnName("image");

                entity.Property(e => e.InsBy)
                    .HasColumnName("insBy")
                    .HasMaxLength(50);

                entity.Property(e => e.InsDatetime)
                    .HasColumnName("insDatetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.UpdBy)
                    .HasColumnName("updBy")
                    .HasMaxLength(50);

                entity.Property(e => e.UpdDatetime)
                    .HasColumnName("updDatetime")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<Treatment>(entity =>
            {
                entity.ToTable("Treatment");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(50);

                entity.Property(e => e.DateEnd)
                    .HasColumnName("date_end")
                    .HasColumnType("datetime");

                entity.Property(e => e.DateStart)
                    .HasColumnName("date_start")
                    .HasColumnType("datetime");

                entity.Property(e => e.Disabled).HasColumnName("disabled");

                entity.Property(e => e.DoctorId).HasColumnName("doctor_id");

                entity.Property(e => e.EstimatedTime)
                    .HasColumnName("estimatedTime")
                    .HasMaxLength(50);

                entity.Property(e => e.Location).HasColumnName("location");

                entity.Property(e => e.Note).HasColumnName("note");

                entity.Property(e => e.PatientId).HasColumnName("patient_id");

                entity.Property(e => e.ReasonCancel)
                    .HasColumnName("reason_cancel")
                    .HasMaxLength(255);

                entity.Property(e => e.ScheduleId).HasColumnName("schedule_id");

                entity.Property(e => e.ServiceId).HasColumnName("service_id");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Treatments)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK_Transactions_Doctors");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Treatments)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("FK_Transactions_Patients");

                entity.HasOne(d => d.Schedule)
                    .WithMany(p => p.Treatments)
                    .HasForeignKey(d => d.ScheduleId)
                    .HasConstraintName("FK_Transaction_Schedule");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.Treatments)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("FK_Transaction_Service");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
