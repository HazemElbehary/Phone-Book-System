namespace PhonebookSystem.Core.Domain.Contracts.Persistence
{
    public interface IApplicationIdentityDbInitializer
    {
        public Task InitializeAsync();
        Task SeedAsync();
    }
}
