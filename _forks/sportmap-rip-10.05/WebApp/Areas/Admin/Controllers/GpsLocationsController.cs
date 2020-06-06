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
    public class GpsLocationsController : Controller
    {
        private readonly AppDbContext _context;

        public GpsLocationsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: GpsLocations
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.GpsLocations.Include(g => g.AppUser).Include(g => g.GpsLocationType).Include(g => g.GpsSession).Include(g => g.TrackPoint);
            return View(await appDbContext.ToListAsync());
        }

        // GET: GpsLocations/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gpsLocation = await _context.GpsLocations
                .Include(g => g.AppUser)
                .Include(g => g.GpsLocationType)
                .Include(g => g.GpsSession)
                .Include(g => g.TrackPoint)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gpsLocation == null)
            {
                return NotFound();
            }

            return View(gpsLocation);
        }

        // GET: GpsLocations/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName");
            ViewData["GpsLocationTypeId"] = new SelectList(_context.GpsLocationTypes, "Id", "Id");
            ViewData["GpsSessionId"] = new SelectList(_context.GpsSessions, "Id", "Description");
            ViewData["TrackPointId"] = new SelectList(_context.TrackPoints, "Id", "Id");
            return View();
        }

        // POST: GpsLocations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RecordedAt,Latitude,Longitude,Accuracy,Altitude,VerticalAccuracy,GpsSessionId,GpsLocationTypeId,TrackPointId,AppUserId,CreatedBy,CreatedAt,ChangedBy,ChangedAt,Id")] GpsLocation gpsLocation)
        {
            if (ModelState.IsValid)
            {
                gpsLocation.Id = Guid.NewGuid();
                _context.Add(gpsLocation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", gpsLocation.AppUserId);
            ViewData["GpsLocationTypeId"] = new SelectList(_context.GpsLocationTypes, "Id", "Id", gpsLocation.GpsLocationTypeId);
            ViewData["GpsSessionId"] = new SelectList(_context.GpsSessions, "Id", "Description", gpsLocation.GpsSessionId);
            ViewData["TrackPointId"] = new SelectList(_context.TrackPoints, "Id", "Id", gpsLocation.TrackPointId);
            return View(gpsLocation);
        }

        // GET: GpsLocations/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gpsLocation = await _context.GpsLocations.FindAsync(id);
            if (gpsLocation == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", gpsLocation.AppUserId);
            ViewData["GpsLocationTypeId"] = new SelectList(_context.GpsLocationTypes, "Id", "Id", gpsLocation.GpsLocationTypeId);
            ViewData["GpsSessionId"] = new SelectList(_context.GpsSessions, "Id", "Description", gpsLocation.GpsSessionId);
            ViewData["TrackPointId"] = new SelectList(_context.TrackPoints, "Id", "Id", gpsLocation.TrackPointId);
            return View(gpsLocation);
        }

        // POST: GpsLocations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("RecordedAt,Latitude,Longitude,Accuracy,Altitude,VerticalAccuracy,GpsSessionId,GpsLocationTypeId,TrackPointId,AppUserId,CreatedBy,CreatedAt,ChangedBy,ChangedAt,Id")] GpsLocation gpsLocation)
        {
            if (id != gpsLocation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gpsLocation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GpsLocationExists(gpsLocation.Id))
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
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", gpsLocation.AppUserId);
            ViewData["GpsLocationTypeId"] = new SelectList(_context.GpsLocationTypes, "Id", "Id", gpsLocation.GpsLocationTypeId);
            ViewData["GpsSessionId"] = new SelectList(_context.GpsSessions, "Id", "Description", gpsLocation.GpsSessionId);
            ViewData["TrackPointId"] = new SelectList(_context.TrackPoints, "Id", "Id", gpsLocation.TrackPointId);
            return View(gpsLocation);
        }

        // GET: GpsLocations/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gpsLocation = await _context.GpsLocations
                .Include(g => g.AppUser)
                .Include(g => g.GpsLocationType)
                .Include(g => g.GpsSession)
                .Include(g => g.TrackPoint)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gpsLocation == null)
            {
                return NotFound();
            }

            return View(gpsLocation);
        }

        // POST: GpsLocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var gpsLocation = await _context.GpsLocations.FindAsync(id);
            _context.GpsLocations.Remove(gpsLocation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GpsLocationExists(Guid id)
        {
            return _context.GpsLocations.Any(e => e.Id == id);
        }
    }
}
