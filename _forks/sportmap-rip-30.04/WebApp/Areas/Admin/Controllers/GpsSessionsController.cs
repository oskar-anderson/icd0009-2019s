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
    public class GpsSessionsController : Controller
    {
        private readonly AppDbContext _context;

        public GpsSessionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: GpsSessions
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.GpsSessions.Include(g => g.AppUser).Include(g => g.GpsSessionType);
            return View(await appDbContext.ToListAsync());
        }

        // GET: GpsSessions/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gpsSession = await _context.GpsSessions
                .Include(g => g.AppUser)
                .Include(g => g.GpsSessionType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gpsSession == null)
            {
                return NotFound();
            }

            return View(gpsSession);
        }

        // GET: GpsSessions/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName");
            ViewData["GpsSessionTypeId"] = new SelectList(_context.GpsSessionTypes, "Id", "Id");
            return View();
        }

        // POST: GpsSessions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,RecordedAt,Duration,Speed,Distance,Climb,Descent,PaceMin,PaceMax,GpsSessionTypeId,AppUserId,CreatedBy,CreatedAt,ChangedBy,ChangedAt,Id")] GpsSession gpsSession)
        {
            if (ModelState.IsValid)
            {
                gpsSession.Id = Guid.NewGuid();
                _context.Add(gpsSession);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", gpsSession.AppUserId);
            ViewData["GpsSessionTypeId"] = new SelectList(_context.GpsSessionTypes, "Id", "Id", gpsSession.GpsSessionTypeId);
            return View(gpsSession);
        }

        // GET: GpsSessions/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gpsSession = await _context.GpsSessions.FindAsync(id);
            if (gpsSession == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", gpsSession.AppUserId);
            ViewData["GpsSessionTypeId"] = new SelectList(_context.GpsSessionTypes, "Id", "Id", gpsSession.GpsSessionTypeId);
            return View(gpsSession);
        }

        // POST: GpsSessions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Description,RecordedAt,Duration,Speed,Distance,Climb,Descent,PaceMin,PaceMax,GpsSessionTypeId,AppUserId,CreatedBy,CreatedAt,ChangedBy,ChangedAt,Id")] GpsSession gpsSession)
        {
            if (id != gpsSession.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gpsSession);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GpsSessionExists(gpsSession.Id))
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
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", gpsSession.AppUserId);
            ViewData["GpsSessionTypeId"] = new SelectList(_context.GpsSessionTypes, "Id", "Id", gpsSession.GpsSessionTypeId);
            return View(gpsSession);
        }

        // GET: GpsSessions/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gpsSession = await _context.GpsSessions
                .Include(g => g.AppUser)
                .Include(g => g.GpsSessionType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gpsSession == null)
            {
                return NotFound();
            }

            return View(gpsSession);
        }

        // POST: GpsSessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var gpsSession = await _context.GpsSessions.FindAsync(id);
            _context.GpsSessions.Remove(gpsSession);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GpsSessionExists(Guid id)
        {
            return _context.GpsSessions.Any(e => e.Id == id);
        }
    }
}
