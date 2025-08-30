using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotesContactsMvc.Data;
using NotesContactsMvc.Models;

namespace NotesContactsMvc.Controllers
{
    public class ContactsController : Controller
    {
        private readonly AppDbContext _db;
        public ContactsController(AppDbContext db) => _db = db;

        public async Task<IActionResult> Index() =>
            View(await _db.Contacts.OrderBy(c => c.Name).ToListAsync());

        public async Task<IActionResult> Details(int id)
        {
            var c = await _db.Contacts.FindAsync(id);
            if (c == null) return NotFound();
            return View(c);
        }

        public IActionResult Create() => View(new Contact());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Contact contact)
        {
            if (!ModelState.IsValid) return View(contact);
            _db.Contacts.Add(contact);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var c = await _db.Contacts.FindAsync(id);
            if (c == null) return NotFound();
            return View(c);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Contact contact)
        {
            if (!ModelState.IsValid) return View(contact);
            _db.Update(contact);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var c = await _db.Contacts.FindAsync(id);
            if (c == null) return NotFound();
            return View(c);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var c = await _db.Contacts.FindAsync(id);
            if (c == null) return NotFound();
            _db.Contacts.Remove(c);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
