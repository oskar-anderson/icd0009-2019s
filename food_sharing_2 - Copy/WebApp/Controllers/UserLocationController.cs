using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;

namespace WebApp.Controllers
{
    public class UserLocationController : Controller
    {
        private readonly AppDbContext _context;

        public UserLocationController(AppDbContext context)
        {
            _context = context;
        }

        // GET: UserLocation
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserLocations.ToListAsync());
        }

        // GET: UserLocation/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userLocation = await _context.UserLocations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userLocation == null)
            {
                return NotFound();
            }

            return View(userLocation);
        }

        // GET: UserLocation/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserLocation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppUserId,District,StreetName,BuildingNumber,ApartmentNumber,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] UserLocation userLocation)
        {
            if (ModelState.IsValid)
            {
                userLocation.Id = Guid.NewGuid();
                _context.Add(userLocation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userLocation);
        }

        // GET: UserLocation/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userLocation = await _context.UserLocations.FindAsync(id);
            if (userLocation == null)
            {
                return NotFound();
            }
            return View(userLocation);
        }

        // POST: UserLocation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AppUserId,District,StreetName,BuildingNumber,ApartmentNumber,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] UserLocation userLocation)
        {
            if (id != userLocation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userLocation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserLocationExists(userLocation.Id))
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
            return View(userLocation);
        }

        // GET: UserLocation/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userLocation = await _context.UserLocations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userLocation == null)
            {
                return NotFound();
            }

            return View(userLocation);
        }

        // POST: UserLocation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var userLocation = await _context.UserLocations.FindAsync(id);
            _context.UserLocations.Remove(userLocation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserLocationExists(Guid id)
        {
            return _context.UserLocations.Any(e => e.Id == id);
        }
    }
}
