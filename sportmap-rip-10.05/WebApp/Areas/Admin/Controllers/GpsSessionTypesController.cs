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
    public class GpsSessionTypesController : Controller
    {
        private readonly AppDbContext _context;

        public GpsSessionTypesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: GpsSessionTypes
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.GpsSessionTypes
                .Include(g => g.Description)
                .ThenInclude(g => g!.Translations)
                .Include(g => g.Name)
                .ThenInclude(g => g!.Translations);
            return View(await appDbContext.ToListAsync());
        }

        // GET: GpsSessionTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gpsSessionType = await _context.GpsSessionTypes
                .Include(g => g.Description)
                .ThenInclude(g => g!.Translations)
                .Include(g => g.Name)
                .ThenInclude(g => g!.Translations)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gpsSessionType == null)
            {
                return NotFound();
            }

            return View(gpsSessionType);
        }

        // GET: GpsSessionTypes/Create
        public IActionResult Create()
        {
            ViewData["DescriptionId"] = new SelectList(_context.LangStrs, "Id", "Id");
            ViewData["NameId"] = new SelectList(_context.LangStrs, "Id", "Id");
            return View();
        }

        // POST: GpsSessionTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NameId,DescriptionId,PaceMin,PaceMax,CreatedBy,CreatedAt,ChangedBy,ChangedAt,Id")] GpsSessionType gpsSessionType)
        {
            if (ModelState.IsValid)
            {
                gpsSessionType.Id = Guid.NewGuid();
                _context.Add(gpsSessionType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DescriptionId"] = new SelectList(_context.LangStrs, "Id", "Id", gpsSessionType.DescriptionId);
            ViewData["NameId"] = new SelectList(_context.LangStrs, "Id", "Id", gpsSessionType.NameId);
            return View(gpsSessionType);
        }

        // GET: GpsSessionTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gpsSessionType = await _context.GpsSessionTypes.FindAsync(id);
            if (gpsSessionType == null)
            {
                return NotFound();
            }
            ViewData["DescriptionId"] = new SelectList(_context.LangStrs, "Id", "Id", gpsSessionType.DescriptionId);
            ViewData["NameId"] = new SelectList(_context.LangStrs, "Id", "Id", gpsSessionType.NameId);
            return View(gpsSessionType);
        }

        // POST: GpsSessionTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("NameId,DescriptionId,PaceMin,PaceMax,CreatedBy,CreatedAt,ChangedBy,ChangedAt,Id")] GpsSessionType gpsSessionType)
        {
            if (id != gpsSessionType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gpsSessionType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GpsSessionTypeExists(gpsSessionType.Id))
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
            ViewData["DescriptionId"] = new SelectList(_context.LangStrs, "Id", "Id", gpsSessionType.DescriptionId);
            ViewData["NameId"] = new SelectList(_context.LangStrs, "Id", "Id", gpsSessionType.NameId);
            return View(gpsSessionType);
        }

        // GET: GpsSessionTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gpsSessionType = await _context.GpsSessionTypes
                .Include(g => g.Description)
                .Include(g => g.Name)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gpsSessionType == null)
            {
                return NotFound();
            }

            return View(gpsSessionType);
        }

        // POST: GpsSessionTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var gpsSessionType = await _context.GpsSessionTypes.FindAsync(id);
            _context.GpsSessionTypes.Remove(gpsSessionType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GpsSessionTypeExists(Guid id)
        {
            return _context.GpsSessionTypes.Any(e => e.Id == id);
        }
    }
}
