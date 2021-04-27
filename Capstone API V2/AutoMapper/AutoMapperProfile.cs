using Capstone_API_V2.Models;
using Capstone_API_V2.ViewModels;
using Capstone_API_V2.ViewModels.SimpleModel;
using MapperProfile = AutoMapper.Profile;

namespace Capstone_API_V2.AutoMapper
{
    public class ViewModelEntityCommonMapper : MapperProfile
    {
        public ViewModelEntityCommonMapper()
        {
            CreateMap<Role, RoleModel>();
            CreateMap<RoleModel, Role>();

            CreateMap<User, UserModel>();
            CreateMap<UserModel, User>();

            CreateMap<Medicine, MedicineModel>();
            CreateMap<MedicineModel, Medicine>();

            CreateMap<Patient, PatientModel>();
            CreateMap<PatientModel, Patient>();

            CreateMap<Patient, PatientSimpModel>();
            CreateMap<PatientSimpModel, Patient>();

            CreateMap<Doctor, DoctorModel>();
            CreateMap<DoctorModel, Doctor>();

            CreateMap<Doctor, DoctorSimpModel>();
            CreateMap<DoctorSimpModel, Doctor>();

            CreateMap<HealthRecord, HealthRecordModel>();
            CreateMap<HealthRecordModel, HealthRecord>();

            CreateMap<Profile, ProfileModel>();
            CreateMap<ProfileModel, Profile>();

            CreateMap<Symptom, SymptomModel>();
            CreateMap<SymptomModel, Symptom>();

            CreateMap<Specialty, SpecialtyModel>();
            CreateMap<SpecialtyModel, Specialty>();

            CreateMap<ExaminationHistory, ExaminationHistoryModel>();
            CreateMap<ExaminationHistoryModel, ExaminationHistory>();

            CreateMap<Transaction, TransactionModel>();
            CreateMap<TransactionModel, Transaction>();

            CreateMap<SymptomDetail, SymptomDetailModel>();
            CreateMap<SymptomDetailModel, SymptomDetail>();

            CreateMap<Service, ServiceModel>();
            CreateMap<ServiceModel, Service>(); 
            
            CreateMap<Prescription, PrescriptionModel>();
            CreateMap<PrescriptionModel, Prescription>();
            
            CreateMap<Prescription, PrescriptionSimpModel>();
            CreateMap<PrescriptionSimpModel, Prescription>();

            CreateMap<Profile, ProfileSimpModel>();
            CreateMap<ProfileSimpModel, Profile>();

            CreateMap<PrescriptionDetail, PrescriptionDetailModel>();
            CreateMap<PrescriptionDetailModel, PrescriptionDetail>();

            CreateMap<Transaction, TransactionSimpModel>();
            CreateMap<TransactionSimpModel, Transaction>(); 
            
            CreateMap<Transaction, TransactionPutModel>();
            CreateMap<TransactionPutModel, Transaction>();

            CreateMap<Transaction, TransactionSimpModel>();
            CreateMap<TransactionSimpModel, Transaction>();

            CreateMap<Transaction, TransactionHistoryModel>()
                .ForMember(des => des.DoctorName, act => act.MapFrom(src => src.Doctor.DoctorNavigation.FullName))
                .ForMember(des => des.PatientId, act => act.MapFrom(src => src.PatientId))
                .ForMember(des => des.PatientName, act => act.MapFrom(src => src.Patient.PatientNavigation.FullName))
                .ForMember(des => des.Relationship, act => act.MapFrom(src => src.Patient.Relationship))
                .ForMember(des => des.ServiceName, act => act.MapFrom(src =>src.Service.ServiceName))
                .ForMember(des => des.ServicePrice, act => act.MapFrom(src => src.Service.ServicePrice));


            CreateMap<Feedback, FeedbackModel>();
            CreateMap<FeedbackModel, Feedback>();

            CreateMap<Schedule, ScheduleModel>();
            CreateMap<ScheduleModel, Schedule>();
            CreateMap<Schedule, Schedule>();

            CreateMap<Schedule, ScheduleSimpModel>();
            CreateMap<ScheduleSimpModel, Schedule>();

            CreateMap<Disease, DiseaseModel>();
            CreateMap<DiseaseModel, Disease>();

            CreateMap<AppConfig, AppConfigModel>();
            CreateMap<AppConfigModel, AppConfig>();
        }
    }
}
