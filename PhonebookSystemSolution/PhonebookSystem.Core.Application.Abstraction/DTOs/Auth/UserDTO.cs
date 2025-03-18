namespace PhonebookSystem.Core.Application.Abstraction.DTOs.Auth
{
    public class UserDTO
    {
        public required string Id { get; set; }
        public required string Email { get; set; }
        public required string Token { get; set; }
    }
}
