using System.Linq.Expressions;

namespace MotoRentalApp.Application.Interfaces.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity> AddAsync(TEntity entity);
        Task<IEnumerable<TEntity>> GetAllAsync(); 
        Task<TEntity> GetByIdAsync(int id); 
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task DeleteAsync(int id); 
    }

}
