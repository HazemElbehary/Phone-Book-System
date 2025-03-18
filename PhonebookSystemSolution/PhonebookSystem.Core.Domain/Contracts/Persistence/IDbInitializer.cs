namespace PhonebookSystem.Core.Domain.Contracts.Persistence
{
    public interface IDbInitializer
    {
        public Task InitializeAsync();

        public Task SeedAsync();
    }
}
