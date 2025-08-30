using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotesContactsMvc.Data;
using NotesContactsMvc.Models;

namespace NotesContactsMvc.Controllers
{
    public class NotesController : Controller
    {
        private readonly AppDbContext _db;
        public NotesController(AppDbContext db) => _db = db;

        public async Task<IActionResult> Index() =>
            View(await _db.Notes.OrderByDescending(n => n.CreatedAt).ToListAsync());

        public async Task<IActionResult> Details(int id)
        {
            var note = await _db.Notes.FindAsync(id);
            if (note == null) return NotFound();
            return View(note);
        }

        public IActionResult Create() => View(new Note());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Note note)
        {
            if (!ModelState.IsValid) return View(note);
            note.CreatedAt = DateTime.Now;
            _db.Notes.Add(note);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var note = await _db.Notes.FindAsync(id);
            if (note == null) return NotFound();
            return View(note);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Note note)
        {
            if (!ModelState.IsValid) return View(note);
            _db.Update(note);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var note = await _db.Notes.FindAsync(id);
            if (note == null) return NotFound();
            return View(note);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var note = await _db.Notes.FindAsync(id);
            if (note == null) return NotFound();
            _db.Notes.Remove(note);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
