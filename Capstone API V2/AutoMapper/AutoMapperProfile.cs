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

            CreateMap<Account, UserModel>();
            CreateMap<UserModel, Account>();

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

            CreateMap<Treatment, TreatmentModel>();
            CreateMap<TreatmentModel, Treatment>();

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

            CreateMap<Treatment, TreatmentSimpModel>();
            CreateMap<TreatmentSimpModel, Treatment>(); 
            
            CreateMap<Treatment, TreatmentPutModel>();
            CreateMap<TreatmentPutModel, Treatment>();

            CreateMap<Treatment, TreatmentSimpModel>();
            CreateMap<TreatmentSimpModel, Treatment>();

            CreateMap<Treatment, TreatmentHistoryModel>()
                .ForMember(des => des.DoctorName, act => act.MapFrom(src => src.Doctor.Fullname))
                .ForMember(des => des.PatientId, act => act.MapFrom(src => src.PatientId))
                .ForMember(des => des.PatientName, act => act.MapFrom(src => src.Patient.Fullname))
                .ForMember(des => des.Relationship, act => act.MapFrom(src => src.Patient.Relationship))
                .ForMember(des => des.ServiceName, act => act.MapFrom(src =>src.Service.Name))
                .ForMember(des => des.ServicePrice, act => act.MapFrom(src => src.Service.Price));


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

            /*CreateMap<Doctor, DoctorRequestModel>()
                .ForMember(des => des.DoctorId, act => act.MapFrom(src => src.Id))
                .ForMember(des => des.DoctorName, act => act.MapFrom(src => src.Fullname))
                .ForMember(des => des.DoctorImage, act => act.MapFrom(src => src.Image))
                .ForMember(des => des., act => act.MapFrom(src => src.Image))*/
        }
    }
}
