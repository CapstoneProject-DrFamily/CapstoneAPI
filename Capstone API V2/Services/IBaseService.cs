using Capstone_API_V2.Helper;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Capstone_API_V2.Services
{
    public interface IBaseService<TEntity, TDto>
        where TEntity : class
        where TDto : class
    {
        Task<PaginatedList<TEntity>> GetAsync(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> filter = null,
                                                        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                        string includeProperties = "");
        IQueryable<TDto> GetAll(Expression<Func<TEntity, bool>> filter = null,
                          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                          string includeProperties = "");
        Task<TDto> CreateAsync(TDto dto);
        Task<TDto> UpdateAsync(TDto dto);
        Task<bool> DeleteAsync(object id);
        Task<TEntity> GetByIdAsync(object id);
        DateTime ConvertTimeZone();
    }
}
