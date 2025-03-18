using Microsoft.EntityFrameworkCore;
using PhonebookSystem.Core.Domain.Contracts.Persistence;
using PhonebookSystem.Infrastructure.Persistence.Data;

namespace PhonebookSystem.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<TEntity>(ApplicationDbContext dbContext) : IGenericRepository<TEntity>
        where TEntity : class
    {
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool withTracking = false)
        {
            if (withTracking)
                return await dbContext.Set<TEntity>().ToListAsync();

            return await dbContext.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task<TEntity?> GetAsync(int Id)
        {
            return await dbContext.Set<TEntity>().FindAsync(Id);
        }

        public async Task AddAsync(TEntity entity)
        {
            await dbContext.Set<TEntity>().AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            dbContext.Set<TEntity>().Remove(entity);
        }

    }
}
