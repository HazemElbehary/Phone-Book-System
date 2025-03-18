using PhonebookSystem.Core.Application.Abstraction;
using PhonebookSystem.Core.Application.Abstraction.Services;
using PhonebookSystem.Core.Application.Services;
using PhonebookSystem.Core.Domain.Contracts.Persistence;

namespace PhonebookSystem.Core.Application
{
    internal class ServiceManager : IServiceManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Lazy<IContactService> _contactService;
        private readonly Lazy<IAuthService> _authService;

        public ServiceManager(
            IUnitOfWork unitOfWork,
            Func<IContactService> ContactServiceFactry,
            Func<IAuthService> AuthServiceFactry)
        {
            _unitOfWork = unitOfWork;
            _contactService = new Lazy<IContactService>(ContactServiceFactry);
            _authService = new Lazy<IAuthService>(AuthServiceFactry);
        }
        public IContactService ContactService => _contactService.Value;
        public IAuthService AuthService => _authService.Value;
    }
}
