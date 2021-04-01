using Capstone_API_V2.Helper;
using Capstone_API_V2.Models;
using Capstone_API_V2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone_API_V2.Services
{
    public interface IDiseaseService : IBaseService<Disease, DiseaseModel>
    {
        Task<PaginatedList<DiseaseModel>> GetAsync(ResourceParameter model);
        Task<List<DiseaseModel>> GetAllDisease();
        Task<DiseaseModel> GetDiseaseByID(string diseaseId);
    }
}
