using PhonebookSystem.Core.Application.Abstraction.DTOs.Auth;
using System.Security.Claims;

namespace PhonebookSystem.Core.Application.Abstraction.Services
{
    public interface IAuthService
    {
        Task<UserDTO> LoginAsync(LoginDTO model);

        Task<UserDTO> RegisterAsync(RegisterDTO model);

        string GetCurrentUserRole(Claim claim);
    }
}
