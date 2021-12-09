using HCARS.Domain.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HCARS.Domain.IRepository
{
    public interface IBaseRepository<T> where T : class
    {
      Task<T> GetByIdAsync(int id);
      Task<IEnumerable<T>> GetAllAsync(String[] includes = null);
      Task<T> FindAsync(Expression<Func<T, bool>> criateria, String[] includes = null);
      Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criateria, String [] includes);
      Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criateria, int skip, int take);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criateria, int? skip, int? take
            , Expression<Func<T, object>> orderBy = null , string orderByDirection = OrderBy.Ascending);

     Task<T> AddAsync (T entity);
     Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);

      T Update(T entity);
     void Delete(int id);
     void AttachAsync (T entity);
     Task<int> CountAsync ();
     Task<int> CountAsync(Expression<Func<T, bool>> criateria);
     void DeleteRangeAsync(IEnumerable<T> entities);

    }
}
