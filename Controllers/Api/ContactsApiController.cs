using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Pzpp.Data;
using Pzpp.Data.Entities;

namespace Pzpp.Controllers.Api
{
    [ApiController]
    [Authorize]
    [Route("api/contacts")]
    public class ContactsApiController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public ContactsApiController(ApplicationDbContext db)
            => _db = db;

        // GET api/contacts
        [HttpGet]
        public async Task<IEnumerable<Contact>> Get()
        {
            var userId = User.FindFirst("sub")?.Value;
            return await _db.Contacts
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }

        // GET api/contacts/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Contact>> Get(int id)
        {
            var userId = User.FindFirst("sub")?.Value;
            var contact = await _db.Contacts
                .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);
            if (contact == null)
                return NotFound();
            return contact;
        }

        // POST api/contacts
        [HttpPost]
        public async Task<ActionResult<Contact>> Post([FromBody] Contact contact)
        {
            contact.UserId = User.FindFirst("sub")?.Value;
            _db.Contacts.Add(contact);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = contact.Id }, contact);
        }

        // PUT api/contacts/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] Contact contact)
        {
            if (id != contact.Id)
                return BadRequest();
            if (contact.UserId != User.FindFirst("sub")?.Value)
                return Forbid();

            _db.Entry(contact).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/contacts/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = User.FindFirst("sub")?.Value;
            var contact = await _db.Contacts
                .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);
            if (contact == null)
                return NotFound();

            _db.Contacts.Remove(contact);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
