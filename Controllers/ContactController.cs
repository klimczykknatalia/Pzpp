using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pzpp.Data;
using Pzpp.Data.Entities;

namespace Pzpp.Controllers
{
    [Authorize]
    public class ContactController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public ContactController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context  = context;
            _userManager = userManager;
        }

        // -------------------------------------------------------------------
        // LISTA + WYSZUKIWANIE
        // /Contact?q=jan
        // -------------------------------------------------------------------
        public async Task<IActionResult> Index(string? q)
        {
            var userId = _userManager.GetUserId(User);

            IQueryable<Contact> query = _context.Contacts
                                                .Where(c => c.UserId == userId);

            if (!string.IsNullOrWhiteSpace(q))
            {
                string term = q.Trim().ToLower();

                // Szukamy we wszystkich głównych polach
                query = query.Where(c =>
                    EF.Functions.Like(c.FirstName.ToLower(),        $"%{term}%") ||
                    EF.Functions.Like(c.LastName.ToLower(),         $"%{term}%") ||
                    EF.Functions.Like(c.Email.ToLower(),            $"%{term}%") ||
                    EF.Functions.Like(c.PhoneNumber.ToLower(),      $"%{term}%") ||
                    EF.Functions.Like((c.FavoriteTymbark ?? "").ToLower(), $"%{term}%")
                );
            }

            var contacts = await query
                                .OrderBy(c => c.LastName)
                                .ThenBy(c => c.FirstName)
                                .ToListAsync();

            return View(contacts);
        }

        // -------------------------------------------------------------------
        // CREATE
        // -------------------------------------------------------------------
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Contact contact)
        {
            if (!ModelState.IsValid)
                return View(contact);

            contact.UserId = _userManager.GetUserId(User);
            _context.Add(contact);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // -------------------------------------------------------------------
        // EDIT
        // -------------------------------------------------------------------
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var userId  = _userManager.GetUserId(User);
            var contact = await _context.Contacts
                                        .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);

            return contact == null ? NotFound() : View(contact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Contact contact)
        {
            if (id != contact.Id) return NotFound();
            if (!ModelState.IsValid) return View(contact);

            var userId = _userManager.GetUserId(User);
            contact.UserId = userId;

            try
            {
                _context.Update(contact);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                bool exists = await _context.Contacts.AnyAsync(c => c.Id == id && c.UserId == userId);
                if (!exists) return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // -------------------------------------------------------------------
        // DELETE
        // -------------------------------------------------------------------
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var userId  = _userManager.GetUserId(User);
            var contact = await _context.Contacts
                                        .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);

            return contact == null ? NotFound() : View(contact);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId  = _userManager.GetUserId(User);
            var contact = await _context.Contacts
                                        .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);

            if (contact == null) return NotFound();

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // -------------------------------------------------------------------
        // DETAILS
        // -------------------------------------------------------------------
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var userId  = _userManager.GetUserId(User);
            var contact = await _context.Contacts
                                        .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);

            return contact == null ? NotFound() : View(contact);
        }
    }
}