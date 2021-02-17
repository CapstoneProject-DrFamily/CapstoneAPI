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

        }
    }
}
