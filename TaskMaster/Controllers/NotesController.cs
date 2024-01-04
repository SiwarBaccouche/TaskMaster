using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskMaster.Areas.Identity.Data;
using TaskMaster.Models;

namespace TaskMaster.Controllers
{
    public class NotesController : Controller
    {
        private readonly TaskMasterDBContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public NotesController(TaskMasterDBContext context, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: Notes
        public async Task<IActionResult> Index()
        {
            var taskMasterDBContext = _context.Note.Include(n => n.Project).Include(n => n.Writer);
            return View(await taskMasterDBContext.ToListAsync());
        }

        // GET: Notes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Note == null)
            {
                return NotFound();
            }

            var note = await _context.Note
                .Include(n => n.Project)
                .Include(n => n.Writer)
                .FirstOrDefaultAsync(m => m.NoteId == id);
            if (note == null)
            {
                return NotFound();
            }

            return View(note);
        }

        // GET: Notes/Create
        public IActionResult Create()
        {
            ViewData["ProjectId"] = new SelectList(_context.Project, "Id", "Id");
            ViewData["WriterId"] = new SelectList(_context.Set<TaskMasterUser>(), "Id", "Id");
            return View();
        }

        // POST: Notes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NoteId,Title,Text,WriterId,ProjectId,ImportanceLevel,CreatedAt")] Note note)
        {
            if (ModelState.IsValid)
            {
                // Get the current logged-in user
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser != null)
                {
                    note.WriterId = currentUser.Id; // Assign the current user's ID as the WriterId
                }

                _context.Add(note);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProjectId"] = new SelectList(_context.Project, "Id", "Id", note.ProjectId);
            ViewData["WriterId"] = new SelectList(_context.Set<TaskMasterUser>(), "Id", "Id", note.WriterId);
            return View(note);
        }

        // GET: Notes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Note == null)
            {
                return NotFound();
            }

            var note = await _context.Note.FindAsync(id);
            if (note == null)
            {
                return NotFound();
            }
            ViewData["ProjectId"] = new SelectList(_context.Project, "Id", "Id", note.ProjectId);
            ViewData["WriterId"] = new SelectList(_context.Set<TaskMasterUser>(), "Id", "Id", note.WriterId);
            return View(note);
        }

        // POST: Notes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NoteId,Title,Text,WriterId,ProjectId,ImportanceLevel,CreatedAt")] Note note)
        {
            if (id != note.NoteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(note);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NoteExists(note.NoteId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProjectId"] = new SelectList(_context.Project, "Id", "Id", note.ProjectId);
            ViewData["WriterId"] = new SelectList(_context.Set<TaskMasterUser>(), "Id", "Id", note.WriterId);
            return View(note);
        }

        // GET: Notes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Note == null)
            {
                return NotFound();
            }

            var note = await _context.Note
                .Include(n => n.Project)
                .Include(n => n.Writer)
                .FirstOrDefaultAsync(m => m.NoteId == id);
            if (note == null)
            {
                return NotFound();
            }

            return View(note);
        }

        // POST: Notes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Note == null)
            {
                return Problem("Entity set 'TaskMasterDBContext.Note'  is null.");
            }
            var note = await _context.Note.FindAsync(id);
            if (note != null)
            {
                _context.Note.Remove(note);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NoteExists(int id)
        {
          return (_context.Note?.Any(e => e.NoteId == id)).GetValueOrDefault();
        }
    }
}
