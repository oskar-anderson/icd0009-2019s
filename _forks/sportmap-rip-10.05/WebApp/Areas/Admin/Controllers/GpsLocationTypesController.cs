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
    public class GpsLocationTypesController : Controller
    {
        private readonly AppDbContext _context;

        public GpsLocationTypesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: GpsLocationTypes
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.GpsLocationTypes
                .Include(g => g.Description)
                .ThenInclude(g => g!.Translations)
                .Include(g => g.Name)
                .ThenInclude(g => g!.Translations);
            return View(await appDbContext.ToListAsync());
        }

        // GET: GpsLocationTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gpsLocationType = await _context.GpsLocationTypes
                    .Include(g => g.Description)
                    .ThenInclude(g => g!.Translations)
                    .Include(g => g.Name)
                    .ThenInclude(g => g!.Translations)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gpsLocationType == null)
            {
                return NotFound();
            }

            return View(gpsLocationType);
        }

        // GET: GpsLocationTypes/Create
        public IActionResult Create()
        {
            ViewData["DescriptionId"] = new SelectList(_context.LangStrs, "Id", "Id");
            ViewData["NameId"] = new SelectList(_context.LangStrs, "Id", "Id");
            return View();
        }

        // POST: GpsLocationTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NameId,DescriptionId,CreatedBy,CreatedAt,ChangedBy,ChangedAt,Id")] GpsLocationType gpsLocationType)
        {
            if (ModelState.IsValid)
            {
                gpsLocationType.Id = Guid.NewGuid();
                _context.Add(gpsLocationType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DescriptionId"] = new SelectList(_context.LangStrs, "Id", "Id", gpsLocationType.DescriptionId);
            ViewData["NameId"] = new SelectList(_context.LangStrs, "Id", "Id", gpsLocationType.NameId);
            return View(gpsLocationType);
        }

        // GET: GpsLocationTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gpsLocationType = await _context.GpsLocationTypes.FindAsync(id);
            if (gpsLocationType == null)
            {
                return NotFound();
            }
            ViewData["DescriptionId"] = new SelectList(_context.LangStrs, "Id", "Id", gpsLocationType.DescriptionId);
            ViewData["NameId"] = new SelectList(_context.LangStrs, "Id", "Id", gpsLocationType.NameId);
            return View(gpsLocationType);
        }

        // POST: GpsLocationTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("NameId,DescriptionId,CreatedBy,CreatedAt,ChangedBy,ChangedAt,Id")] GpsLocationType gpsLocationType)
        {
            if (id != gpsLocationType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gpsLocationType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GpsLocationTypeExists(gpsLocationType.Id))
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
            ViewData["DescriptionId"] = new SelectList(_context.LangStrs, "Id", "Id", gpsLocationType.DescriptionId);
            ViewData["NameId"] = new SelectList(_context.LangStrs, "Id", "Id", gpsLocationType.NameId);
            return View(gpsLocationType);
        }

        // GET: GpsLocationTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gpsLocationType = await _context.GpsLocationTypes
                .Include(g => g.Description)
                .Include(g => g.Name)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gpsLocationType == null)
            {
                return NotFound();
            }

            return View(gpsLocationType);
        }

        // POST: GpsLocationTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var gpsLocationType = await _context.GpsLocationTypes.FindAsync(id);
            _context.GpsLocationTypes.Remove(gpsLocationType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GpsLocationTypeExists(Guid id)
        {
            return _context.GpsLocationTypes.Any(e => e.Id == id);
        }
    }
}
