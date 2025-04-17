
using System.Linq.Expressions;

namespace Pexel.Application.Contracts.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync(Expression<Func<T,bool>> filter = null , Func<IQueryable<T> , IOrderedQueryable<T>> OrderBy = null, string[] include = null);

        Task<T> FirstOrDefault(Expression<Func<T,bool>>filter=null , Func<IQueryable<T> , IOrderedQueryable<T>> OrderBy = null, string[] include = null);

       IQueryable<T> Sync(Expression<Func<T,bool>> filter = null, Func<IQueryable<T>,IOrderedQueryable<T>> OrderBy = null, string[] include = null);
        Task<T> GetByIdAsync(int Id);
        Task<bool> CreateAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);

        Task<bool> AddRange(List<T> entity);

        Task<bool> IsExist(Expression< Func<T, bool>> filter);
    }
}
