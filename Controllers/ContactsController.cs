using contractsapi.Data;
using contractsapi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace contractsapi.Controllers
{
    [ApiController]
    [Route("api/Contacts")]
    public class ContactsController : Controller
    {
        private readonly ContractsApiDbContext dbContext;
        public ContactsController(ContractsApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            return Ok(await this.dbContext.Contracts.ToListAsync());
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetContacts([FromRoute] Guid id)
        {
           return Ok(await this.dbContext.Contracts.FindAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddContacts(AddContactsRequest addContactsRequest)
        {
            var contact = new Contracts
            {
                Id = Guid.NewGuid(),
                Address = addContactsRequest.Address,
                Email = addContactsRequest.Email,
                FullName = addContactsRequest.FullName,
                Phone = addContactsRequest.Phone
            };
            await dbContext.Contracts.AddAsync(contact);
            await dbContext.SaveChangesAsync();

            return Ok(contact);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContacts([FromRoute] Guid id, UpdateContactRequest updateContactRequest)
        {
            //await dbContext.Contracts.ForEachAsync(contractsapi =>
            // {
            //     if (contractsapi.Id == id)
            //     {
            //         contractsapi.Email = updateContactRequest.Email;
            //         contractsapi.FullName = updateContactRequest.FullName;
            //         contractsapi.Phone = updateContactRequest.Phone;
            //         contractsapi.Address = updateContactRequest.Address;
            //     }
            // });
            //await dbContext.SaveChangesAsync();
            //return Ok(updateContactRequest);

            var contract= await dbContext.Contracts.FindAsync(id);
            if(contract == null)
            {
                return NotFound();
            }
            else
            {
                contract.Email = updateContactRequest.Email;
                contract.FullName = updateContactRequest.FullName;
                contract.Phone = updateContactRequest.Phone;
                contract.Address = updateContactRequest.Address;
                await dbContext.SaveChangesAsync();
                return Ok(updateContactRequest);
            }
        }


        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteContacts([FromRoute] Guid id)
        {
            var contact = await dbContext.Contracts.FindAsync(id);
            if(contact == null)
            {
                  return NotFound(); 
            }
            else
            {
                dbContext.Contracts.Remove(contact);
                await dbContext.SaveChangesAsync();
                return Ok(contact);
            }
        }

    }
}
