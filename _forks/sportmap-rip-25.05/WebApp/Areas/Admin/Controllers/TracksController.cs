#pragma warning disable 1591
using System;
using System.Linq;
using System.Threading.Tasks;
using DAL.App.EF;
using Domain.App;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class TracksController : Controller
    {
        private readonly AppDbContext _context;

        public TracksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Tracks
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Tracks
                .Include(t => t.AppUser)
                .Include(g => g.Description)
                .ThenInclude(g => g!.Translations)
                .Include(g => g.Name)
                .ThenInclude(g => g!.Translations);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Tracks/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var track = await _context.Tracks
                .Include(t => t.AppUser)
                .Include(g => g.Description)
                .ThenInclude(g => g!.Translations)
                .Include(g => g.Name)
                .ThenInclude(g => g!.Translations)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (track == null)
            {
                return NotFound();
            }

            return View(track);
        }

        // GET: Tracks/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName");
            ViewData["DescriptionId"] = new SelectList(_context.LangStrs, "Id", "Id");
            ViewData["NameId"] = new SelectList(_context.LangStrs, "Id", "Id");
            return View();
        }

        // POST: Tracks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NameId,DescriptionId,MapFile,AppUserId,CreatedBy,CreatedAt,ChangedBy,ChangedAt,Id")] Track track)
        {
            if (ModelState.IsValid)
            {
                track.Id = Guid.NewGuid();
                _context.Add(track);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", track.AppUserId);
            ViewData["DescriptionId"] = new SelectList(_context.LangStrs, "Id", "Id", track.DescriptionId);
            ViewData["NameId"] = new SelectList(_context.LangStrs, "Id", "Id", track.NameId);
            return View(track);
        }

        // GET: Tracks/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var track = await _context.Tracks.FindAsync(id);
            if (track == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", track.AppUserId);
            ViewData["DescriptionId"] = new SelectList(_context.LangStrs, "Id", "Id", track.DescriptionId);
            ViewData["NameId"] = new SelectList(_context.LangStrs, "Id", "Id", track.NameId);
            return View(track);
        }

        // POST: Tracks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("NameId,DescriptionId,MapFile,AppUserId,CreatedBy,CreatedAt,ChangedBy,ChangedAt,Id")] Track track)
        {
            if (id != track.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(track);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrackExists(track.Id))
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
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", track.AppUserId);
            ViewData["DescriptionId"] = new SelectList(_context.LangStrs, "Id", "Id", track.DescriptionId);
            ViewData["NameId"] = new SelectList(_context.LangStrs, "Id", "Id", track.NameId);
            return View(track);
        }

        // GET: Tracks/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var track = await _context.Tracks
                .Include(t => t.AppUser)
                .Include(t => t.Description)
                .Include(t => t.Name)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (track == null)
            {
                return NotFound();
            }

            return View(track);
        }

        // POST: Tracks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var track = await _context.Tracks.FindAsync(id);
            _context.Tracks.Remove(track);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrackExists(Guid id)
        {
            return _context.Tracks.Any(e => e.Id == id);
        }
    }
}
