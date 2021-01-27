using Capstone_API_V2.Models;
using Capstone_API_V2.ViewModels;
using Profile = AutoMapper.Profile;

namespace Capstone_API_V2.AutoMapper
{
    public class ViewModelEntityCommonMapper : Profile
    {
        public ViewModelEntityCommonMapper()
        {
            CreateMap<Role, RoleModel>();
            CreateMap<RoleModel, Role>();

            CreateMap<User, UserModel>();
            CreateMap<UserModel, User>();

            CreateMap<Medicine, MedicineModel>();
            CreateMap<MedicineModel, Medicine>();
        }
    }
}
