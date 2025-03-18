using System.ComponentModel.DataAnnotations;
namespace PhonebookSystem.Core.Application.Abstraction.DTOs.Auth
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string Password { get; set; }
    }
}
