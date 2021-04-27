using AutoMapper;
using Capstone_API_V2.Helper;
using Capstone_API_V2.Repositories;
using Capstone_API_V2.UnitOfWork;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Capstone_API_V2.Services
{
    public abstract class BaseService<TEntity, TDto> : IBaseService<TEntity, TDto>
        where TEntity : class
        where TDto : class
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        protected abstract IGenericRepository<TEntity> _repository { get; }

        public BaseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public virtual async Task<TDto> CreateAsync(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            _repository.Add(entity);

            await _unitOfWork.SaveAsync();

            return _mapper.Map<TDto>(entity);
        }

        public virtual async Task<bool> DeleteAsync(object id)
        {
            if (id != null)
            {
                _repository.Delete(id);

            }
            return await _unitOfWork.SaveAsync() > 0;
        }

        public virtual async Task<TDto> UpdateAsync(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            _repository.Update(entity);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<TDto>(entity);
        }

        public virtual async Task<TEntity> GetByIdAsync(object id)
        {
            if (id != null)
            {
                return _mapper.Map<TEntity>(await _repository.GetById(id));
            }
            return null;
        }

        public Task<PaginatedList<TEntity>> GetAsync(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            return _repository.Get(pageIndex, pageSize, filter, orderBy, includeProperties);
        }

        public IQueryable<TDto> GetAll(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            return _mapper.ProjectTo<TDto>(_repository.GetAll(filter, orderBy, includeProperties));
        }

        public IQueryable<TEntity> GetAllEntity(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            return _mapper.ProjectTo<TEntity>(_repository.GetAll(filter, orderBy, includeProperties));
        }

        public DateTime ConvertTimeZone()
        {
            DateTime serverTime = DateTime.Now; // gives you current Time in server timeZone
            DateTime utcTime = serverTime.ToUniversalTime(); // convert it to Utc using timezone setting of server computer
            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById(Constants.Format.VN_TIMEZONE_ID);
            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi);
            return localTime;
        }
    }
}
