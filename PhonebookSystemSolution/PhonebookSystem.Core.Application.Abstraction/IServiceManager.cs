using PhonebookSystem.Core.Application.Abstraction.Services;

namespace PhonebookSystem.Core.Application.Abstraction
{
    public interface IServiceManager
    {
        public IContactService ContactService { get; }
        public IAuthService AuthService { get; }
    }
}
