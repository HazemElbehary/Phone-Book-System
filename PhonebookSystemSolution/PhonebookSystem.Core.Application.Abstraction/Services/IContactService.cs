using PhonebookSystem.Core.Application.Abstraction.DTOs;

namespace PhonebookSystem.Core.Application.Abstraction.Services
{
    public interface IContactService
    {
        Task<IEnumerable<ReturnedContactDTO>> GetContactsAsync();
        Task AddContactAsync(ContactDTO createdContact);
        Task DeleteContact(int id);
    }
}
