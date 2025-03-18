using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhonebookSystem.Core.Application.Abstraction;
using PhonebookSystem.Core.Application.Abstraction.DTOs.Auth;
using System.Security.Claims;
using System.Threading.Tasks;

namespace APIs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO model)
        {
            var result = await serviceManager.AuthService.LoginAsync(model);
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO model)
        {
            var result = await serviceManager.AuthService.RegisterAsync(model);
            return Ok(result);
        }

        [HttpGet("getCurrentUserRole")]
        public  ActionResult GetCurrentUserRole()
        {
            var result = serviceManager.AuthService.GetCurrentUserRole(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)!);
            return Ok(new
            {
                role = result.ToLower()
            });
        }
    }
}