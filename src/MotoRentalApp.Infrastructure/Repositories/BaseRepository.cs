using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MotoRentalApp.Infrastructure.Repositories
{
    public class BaseRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;

        public BaseRepository(DbContext context)
        {
            _context = context;
        }
        
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await SaveChangesAsync();
            return entity;
        }

        
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync(); // Corrigido para retornar IEnumerable
        }

        
        public async Task<TEntity> GetByIdAsync(int id) // Alterado para int
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        
        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            await SaveChangesAsync();
            return entity;
        }

        
        public async Task DeleteAsync(int id) // Alterado para int
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.Set<TEntity>().Remove(entity);
                await SaveChangesAsync();
            }
        }

        
        private async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        
        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }
    }
}
