using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Capstone_API_V2.Helper;
using Capstone_API_V2.Models;
using Microsoft.EntityFrameworkCore;

namespace Capstone_API_V2.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        internal FamilyDoctorContext _context;
        internal DbSet<TEntity> _dbSet;

        public GenericRepository(FamilyDoctorContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual void Add(TEntity entity)
        {
            if (entity == null) throw new ArgumentException("entity");
            _dbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entity = _dbSet.Find(id);
            _dbSet.Attach(entity);
            _dbSet.Remove(entity);
        }

        public virtual async Task<PaginatedList<TEntity>> Get(int pageIndex = 0, int pageSize = 0, Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await PaginatedList<TEntity>.CreateAsync(query.AsNoTracking(), pageIndex, pageSize);
        }

        public virtual IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query;
        }

        public virtual async Task<TEntity> GetById(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public IQueryable<TEntity> GetByObject(Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> query = _dbSet;
            query = query.Where(filter);

            return query;
        }

        public Task<TEntity> GetFirst(Expression<Func<TEntity, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            foreach (var includeProperty in includeProperties.Split
               (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            return query.FirstOrDefaultAsync();
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            if (entityToUpdate == null) throw new ArgumentException("entity");
            _dbSet.Attach(entityToUpdate);
            _dbSet.Update(entityToUpdate);
        }
    }
}
