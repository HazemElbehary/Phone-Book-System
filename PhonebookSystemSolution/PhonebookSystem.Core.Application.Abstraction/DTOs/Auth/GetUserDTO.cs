namespace PhonebookSystem.Core.Application.Abstraction.DTOs.Auth
{
    public class GetUserDTO
    {
        public required string Email { get; set; }
        public string? Password { get; set; }
    }
}
