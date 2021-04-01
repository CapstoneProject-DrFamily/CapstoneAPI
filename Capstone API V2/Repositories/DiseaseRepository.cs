using Capstone_API_V2.Helper;
using Capstone_API_V2.Models;
using Capstone_API_V2.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone_API_V2.Repositories
{
    public class DiseaseRepository : IDiseaseRepository
    {
        private readonly FamilyDoctorContext _context;

        public DiseaseRepository(FamilyDoctorContext context)
        {
            _context = context;
        }

        public async Task<List<Disease>> GetAllDisease()
        {
            var result = await _context.Diseases
                .Where(disease => disease.Disabled == false)
                .ToListAsync();

            return result;
        }

        public async Task<PaginatedList<DiseaseModel>> Get(ResourceParameter model)
        {
            IQueryable<DiseaseModel> query = _context.Diseases.Where(disease => !string.IsNullOrWhiteSpace(model.SearchValue) ? disease.Disabled == false
                && disease.DiseaseName.StartsWith(model.SearchValue) : disease.Disabled == false)
                .Select(disease => new DiseaseModel 
                {
                    DiseaseCode = disease.DiseaseCode,
                    DiseaseName = disease.DiseaseName,
                    ChapterCode = disease.ChapterCode,
                    ChapterName= disease.ChapterName,
                    MainGroupCode = disease.MainGroupCode,
                    MainGroupName =  disease.MainGroupName,
                    TypeCode = disease.TypeCode,
                    TypeName = disease.TypeName
                });

            return await PaginatedList<DiseaseModel>.CreateAsync(query.AsNoTracking(), model.PageIndex, model.PageSize);
        }

        public async Task<Disease> GetDiseaseByID(string diseaseCode)
        {
            var result = await _context.Diseases.Where(disease => disease.DiseaseCode.Equals(diseaseCode) && disease.Disabled == false)
                .SingleOrDefaultAsync();
            return result;
        }
    }
}
