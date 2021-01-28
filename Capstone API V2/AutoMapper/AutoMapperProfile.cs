using Capstone_API_V2.Models;
using Capstone_API_V2.ViewModels;
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

            CreateMap<Doctor, DoctorModel>();
            CreateMap<DoctorModel, Doctor>(); 
            
            CreateMap<HealthRecord, HealthRecordModel>();
            CreateMap<HealthRecordModel, HealthRecord>();

            CreateMap<Profile, ProfileModel>();
            CreateMap<ProfileModel, Profile>();
        }
    }
}
