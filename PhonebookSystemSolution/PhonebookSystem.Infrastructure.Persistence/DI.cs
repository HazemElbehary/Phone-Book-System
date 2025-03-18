using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhonebookSystem.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using PhonebookSystem.Core.Domain.Contracts.Persistence;
using PhonebookSystem.Infrastructure.Persistence.UOW;
using PhonebookSystem.Infrastructure.Persistence.Identity;

namespace PhonebookSystem.Infrastructure.Persistence
{
    public static class DI
    {
        public static IServiceCollection AddPresistenceServices(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<ApplicationDbContext>((serviceProvider, optionsBuilder) =>
            {
                optionsBuilder.UseNpgsql(configuration.GetConnectionString("ApplicationConnection"));
            });

            service.AddDbContext<ApplicationIdentityDbContext>(optionsBuilder =>
            {
                optionsBuilder.UseNpgsql(configuration.GetConnectionString("IdentityConnection"));
            });

            service.AddScoped(typeof(IApplicationIdentityDbInitializer), typeof(ApplicationIdentityDbInitializer));



            service.AddScoped<IDbInitializer, DbInitializer>();
            service.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

            return service;
        }
    }
}
