using Microsoft.Extensions.DependencyInjection;
using PhonebookSystem.Core.Application.Abstraction;
using PhonebookSystem.Core.Application.Abstraction.Services;
using PhonebookSystem.Core.Application.Services;

namespace PhonebookSystem.Core.Application
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection service)
        {
            service.AddScoped(typeof(IContactService), typeof(ContactService));
            service.AddScoped(typeof(Func<IContactService>), (serviceProvider) =>
            {
                return () => serviceProvider.GetRequiredService<IContactService>();
            });

            service.AddScoped(typeof(IAuthService), typeof(AuthService));
            service.AddScoped(typeof(Func<IAuthService>), (serviceProvider) =>
            {
                return () => serviceProvider.GetRequiredService<IAuthService>();
            });

            service.AddScoped(typeof(IServiceManager), typeof(ServiceManager));

            return service;
        }
    }
}
