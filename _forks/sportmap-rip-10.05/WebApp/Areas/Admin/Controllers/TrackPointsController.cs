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
    public class TrackPointsController : Controller
    {
        private readonly AppDbContext _context;

        public TrackPointsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: TrackPoints
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.TrackPoints.Include(t => t.AppUser).Include(t => t.Track);
            return View(await appDbContext.ToListAsync());
        }

        // GET: TrackPoints/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trackPoint = await _context.TrackPoints
                .Include(t => t.AppUser)
                .Include(t => t.Track)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trackPoint == null)
            {
                return NotFound();
            }

            return View(trackPoint);
        }

        // GET: TrackPoints/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName");
            ViewData["TrackId"] = new SelectList(_context.Tracks, "Id", "Id");
            return View();
        }

        // POST: TrackPoints/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Latitude,Longitude,Accuracy,PassOrder,TrackId,AppUserId,CreatedBy,CreatedAt,ChangedBy,ChangedAt,Id")] TrackPoint trackPoint)
        {
            if (ModelState.IsValid)
            {
                trackPoint.Id = Guid.NewGuid();
                _context.Add(trackPoint);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", trackPoint.AppUserId);
            ViewData["TrackId"] = new SelectList(_context.Tracks, "Id", "Id", trackPoint.TrackId);
            return View(trackPoint);
        }

        // GET: TrackPoints/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trackPoint = await _context.TrackPoints.FindAsync(id);
            if (trackPoint == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", trackPoint.AppUserId);
            ViewData["TrackId"] = new SelectList(_context.Tracks, "Id", "Id", trackPoint.TrackId);
            return View(trackPoint);
        }

        // POST: TrackPoints/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Latitude,Longitude,Accuracy,PassOrder,TrackId,AppUserId,CreatedBy,CreatedAt,ChangedBy,ChangedAt,Id")] TrackPoint trackPoint)
        {
            if (id != trackPoint.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trackPoint);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrackPointExists(trackPoint.Id))
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
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", trackPoint.AppUserId);
            ViewData["TrackId"] = new SelectList(_context.Tracks, "Id", "Id", trackPoint.TrackId);
            return View(trackPoint);
        }

        // GET: TrackPoints/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trackPoint = await _context.TrackPoints
                .Include(t => t.AppUser)
                .Include(t => t.Track)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trackPoint == null)
            {
                return NotFound();
            }

            return View(trackPoint);
        }

        // POST: TrackPoints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var trackPoint = await _context.TrackPoints.FindAsync(id);
            _context.TrackPoints.Remove(trackPoint);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrackPointExists(Guid id)
        {
            return _context.TrackPoints.Any(e => e.Id == id);
        }
    }
}
