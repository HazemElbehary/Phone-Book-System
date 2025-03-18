namespace PhonebookSystem.Core.Domain.Contracts.Persistence
{
    public interface IGenericRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllAsync(bool withTracking = false);
        Task<TEntity?> GetAsync(int Id);

        Task AddAsync(TEntity entity);

        void Delete(TEntity entity);
    }
}
