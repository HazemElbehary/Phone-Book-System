using PhonebookSystem.Core.Domain.Contracts.Persistence;

namespace PhonebookSystem.APIs.Extensions
{
    public static class InitializerExtensions
    {
        public async static Task<WebApplication> InitializeApplication(this WebApplication app)
        {
            using var scope = app.Services.CreateAsyncScope();
            var services = scope.ServiceProvider;

            var ApplicationContextInitializer = services.GetRequiredService<IDbInitializer>();
            var IdnetityContextInitializer = services.GetRequiredService<IApplicationIdentityDbInitializer>();


            await ApplicationContextInitializer.InitializeAsync();
            await ApplicationContextInitializer.SeedAsync();
            await IdnetityContextInitializer.InitializeAsync();
            await IdnetityContextInitializer.SeedAsync();
            return app;
        }
    }
}
