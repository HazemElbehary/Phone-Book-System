namespace PhonebookSystem.Core.Application.Abstraction.DTOs
{
    public class ContactDTO
    {
        public required string Name { get; set; }
        public required string PhoneNumber { get; set; }
        public string? Email { get; set; }
    }
}
