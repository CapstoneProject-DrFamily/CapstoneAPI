using Capstone_API_V2.Helper;
using Capstone_API_V2.Models;
using Capstone_API_V2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone_API_V2.Repositories
{
    public interface IDiseaseRepository
    {
        Task<PaginatedList<DiseaseModel>> Get(ResourceParameter model);
        Task<List<Disease>> GetAllDisease();
        Task<Disease> GetDiseaseByID(string diseaseCode);

    }
}
