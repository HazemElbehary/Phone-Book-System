using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhonebookSystem.Core.Application.Abstraction;
using PhonebookSystem.Core.Application.Abstraction.DTOs;

namespace APIs.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ContactsController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<ReturnedContactDTO>> GetContacts()
        {
            var Contacts = await serviceManager.ContactService.GetContactsAsync();
            return Ok(Contacts);
        }

        [HttpPost("CreateContact")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AddContact(ContactDTO contactDTO)
        {
            try
            {
                await serviceManager.ContactService.AddContactAsync(contactDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("DeleteContact/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteContact(int id)
        {
            try
            {
                await serviceManager.ContactService.DeleteContact(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }
    }
}