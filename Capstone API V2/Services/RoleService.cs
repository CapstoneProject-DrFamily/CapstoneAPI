using System.Linq;
using AutoMapper;
using Capstone_API_V2.Models;
using Capstone_API_V2.Repositories;
using Capstone_API_V2.UnitOfWork;
using Capstone_API_V2.ViewModels;

namespace Capstone_API_V2.Services
{
    public class RoleService : BaseService<Role, RoleModel>, IRoleService
    {
        public RoleService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        protected override IGenericRepository<Role> _repository => _unitOfWork.RoleRepository;

        public string GetRole(User user)
        {
            var role = _repository.GetByObject(u => u.RoleId == user.RoleId).SingleOrDefault();
            return role.Name;
        }
    }
}
