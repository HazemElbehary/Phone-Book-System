using PhonebookSystem.Core.Domain.Contracts.Persistence;
using PhonebookSystem.Infrastructure.Persistence.Data;
using PhonebookSystem.Infrastructure.Persistence.Repositories;
using System.Collections.Concurrent;

namespace PhonebookSystem.Infrastructure.Persistence.UOW
{
    public class UnitOfWork : IUnitOfWork, IAsyncDisposable
    {
        private readonly ApplicationDbContext _dbContext;
        ConcurrentDictionary<string, object> Repositories;

        public UnitOfWork(ApplicationDbContext context)
        {
            _dbContext = context;
            Repositories = new();
        }

        public async Task<int> CompleteAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _dbContext.DisposeAsync();
        }

        public IGenericRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class
        {
            return (GenericRepository<TEntity>)Repositories.GetOrAdd(typeof(TEntity).Name, new GenericRepository<TEntity>(_dbContext));
        }
    }
}
