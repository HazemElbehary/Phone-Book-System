using PhonebookSystem.Core.Application.Abstraction.DTOs;
using PhonebookSystem.Core.Application.Abstraction.Services;
using PhonebookSystem.Core.Domain.Contracts.Persistence;
using PhonebookSystem.Core.Domain.Entities;

namespace PhonebookSystem.Core.Application.Services
{
    internal class ContactService(IUnitOfWork unitOfWork) : IContactService
    {
        public async Task<IEnumerable<ReturnedContactDTO>> GetContactsAsync()
        {
            var Contacts = await unitOfWork.GetRepository<Contact>().GetAllAsync();
            List<ReturnedContactDTO> returnedcontacts = new();

            foreach (var contact in Contacts)
            {
                returnedcontacts.Add(new ReturnedContactDTO()
                {
                    Id = contact.Id,
                    Name = contact.Name,
                    PhoneNumber = contact.PhoneNumber,
                    Email = contact.Email
                });
            }

            return returnedcontacts;
        }

        public async Task AddContactAsync(ContactDTO ContactDto)
            {
                var contact = new Contact()
                {
                    Name = ContactDto.Name,
                    Email = ContactDto.Email,
                    PhoneNumber = ContactDto.PhoneNumber
                };

                await unitOfWork.GetRepository<Contact>().AddAsync(contact);
                await unitOfWork.CompleteAsync();
            }

        public async Task DeleteContact(int id)
            {
                var contact = await unitOfWork.GetRepository<Contact>().GetAsync(id);
                unitOfWork.GetRepository<Contact>().Delete(contact);
                await unitOfWork.CompleteAsync();
            }
    }
}
