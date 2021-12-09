using HCARS.Domain.Consts;
using HCARS.Domain.IRepository;
using HCARS.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HCARS.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected HCarsDbContext _context;

        public BaseRepository(HCarsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync(String[] includes = null) {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);

            return await query.ToListAsync();
         
         }
        

        public async Task<T> GetByIdAsync(int id)=> await _context.Set<T>().FindAsync(id);

        public async Task<T> FindAsync(Expression<Func<T, bool>> criateria, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if(includes != null)
                foreach(var include in includes)
                    query = query.Include(include);
            return await query.SingleOrDefaultAsync(criateria);
        }

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criateria, string[] includes)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);
            return await query.Where(criateria).ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criateria, int skip, int take)
            => await _context.Set<T>().Where(criateria).Skip(skip).Take(take).ToListAsync();

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criateria, int? skip, int? take, Expression<Func<T, 
            object>> orderBy = null, string orderByDirection = "ASC")
        {
            IQueryable<T> query = _context.Set<T>().Where(criateria);
            if (take.HasValue)
                query = query.Take(take.Value);
            if (skip.HasValue)
                query = query.Take(skip.Value);

            if(orderBy != null)
            {
                if(orderByDirection == OrderBy.Ascending)
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            }

            return await query.ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
             await _context.Set<T>().AddRangeAsync(entities);
            return entities;
        }

        public T Update(T entity)
        {
            _context.Update(entity);
            return entity;

        }

        public  void Delete(int id)
        {
            var entity = _context.Set<T>().Find(id);
            _context.Set<T>().Remove(entity);
        }

        public void AttachAsync(T entity)
        {
            _context.Set<T>().Attach(entity);
        }

        public async Task<int> CountAsync()
        {
           return await _context.Set<T>().CountAsync();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> criateria)
        {
            return await _context.Set<T>().CountAsync(criateria);
        }

        public void DeleteRangeAsync(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }
    }
}
