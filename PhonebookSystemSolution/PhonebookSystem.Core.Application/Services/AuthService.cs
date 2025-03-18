using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PhonebookSystem.Core.Application.Abstraction.DTOs.Auth;
using PhonebookSystem.Core.Application.Abstraction.Services;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PhonebookSystem.Core.Application.Services
{
    internal class AuthService(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IOptions<JwtSettings> jwtSettings
            ) : IAuthService
    {
        private readonly JwtSettings _jwtSettings = jwtSettings.Value;

        public async Task<UserDTO> RegisterAsync(RegisterDTO model)
        {
            var user = new IdentityUser()
            {
                Email = model.Email,
                UserName = model.UserName
            };

            user.EmailConfirmed = true;
            user.PhoneNumberConfirmed = true;
            var result = await userManager.CreateAsync(user, model.Password);

            await userManager.AddToRoleAsync(user, "User");

            if (!result.Succeeded)
            {
                var errorDictionary = new Dictionary<string, string[]>();

                errorDictionary["General"] = result.Errors.Select(e => e.Description).ToArray();

                string errors = "";
                foreach (var error in errorDictionary)
                {
                    errors += $"{error.Key}: {error.Value}";
                }

                throw new ValidationException(errors);
            };


            var response = new UserDTO()
            {
                Id = user.Id,
                Email = user.Email,
                Token = await GenerateTokenAsync(user)
            };

            return response;
        }

        public async Task<UserDTO> LoginAsync(LoginDTO model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);

            if (user is null) throw new Exception("Invalid Login");

            var result = await signInManager.CheckPasswordSignInAsync(user, model.Password, lockoutOnFailure: true);

            if (result.IsNotAllowed) throw new Exception("Account Not Confirmed Yet.");

            if (result.IsLockedOut) throw new Exception("Account Is Locked.");

            if (!result.Succeeded) throw new Exception("Invalid Login.");


            var response = new UserDTO()
            {
                Id = user.Id,
                Email = user.Email!,
                Token = await GenerateTokenAsync(user)
            };

            return response;
        }

        private async Task<string> GenerateTokenAsync(IdentityUser user)
        {
            var userClaims = await userManager.GetClaimsAsync(user);
            var roleAsClaims = new List<Claim>();

            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                roleAsClaims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.PrimarySid, user.Id),
                new Claim(ClaimTypes.Email, user.Email!),
            }.Union(userClaims)
            .Union(roleAsClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);


            var tokenObject = new JwtSecurityToken(
                issuer: _jwtSettings.Issure,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenObject);
        }

        public string GetCurrentUserRole(Claim claim)
        {
            //if (claim == null)
            //{
            //    throw new Exception("User role not found.");
            //}

            return claim?.Value;
        }
    }
}