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
    public class LangStrTranslationsController : Controller
    {
        private readonly AppDbContext _context;

        public LangStrTranslationsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: LangStrTranslations
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.LangStrTranslation.Include(l => l.LangStr).OrderBy(l => l.LangStrId);
            return View(await appDbContext.ToListAsync());
        }

        // GET: LangStrTranslations/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var langStrTranslation = await _context.LangStrTranslation
                .Include(l => l.LangStr)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (langStrTranslation == null)
            {
                return NotFound();
            }

            return View(langStrTranslation);
        }

        // GET: LangStrTranslations/Create
        public IActionResult Create()
        {
            ViewData["LangStrId"] = new SelectList(_context.LangStrs, "Id", "Id");
            return View();
        }

        // POST: LangStrTranslations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Culture,Value,LangStrId,CreatedBy,CreatedAt,ChangedBy,ChangedAt,Id")] LangStrTranslation langStrTranslation)
        {
            if (ModelState.IsValid)
            {
                langStrTranslation.Id = Guid.NewGuid();
                _context.Add(langStrTranslation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LangStrId"] = new SelectList(_context.LangStrs, "Id", "Id", langStrTranslation.LangStrId);
            return View(langStrTranslation);
        }

        // GET: LangStrTranslations/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var langStrTranslation = await _context.LangStrTranslation.FindAsync(id);
            if (langStrTranslation == null)
            {
                return NotFound();
            }
            ViewData["LangStrId"] = new SelectList(_context.LangStrs, "Id", "Id", langStrTranslation.LangStrId);
            return View(langStrTranslation);
        }

        // POST: LangStrTranslations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Culture,Value,LangStrId,CreatedBy,CreatedAt,ChangedBy,ChangedAt,Id")] LangStrTranslation langStrTranslation)
        {
            if (id != langStrTranslation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(langStrTranslation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LangStrTranslationExists(langStrTranslation.Id))
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
            ViewData["LangStrId"] = new SelectList(_context.LangStrs, "Id", "Id", langStrTranslation.LangStrId);
            return View(langStrTranslation);
        }

        // GET: LangStrTranslations/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var langStrTranslation = await _context.LangStrTranslation
                .Include(l => l.LangStr)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (langStrTranslation == null)
            {
                return NotFound();
            }

            return View(langStrTranslation);
        }

        // POST: LangStrTranslations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var langStrTranslation = await _context.LangStrTranslation.FindAsync(id);
            _context.LangStrTranslation.Remove(langStrTranslation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LangStrTranslationExists(Guid id)
        {
            return _context.LangStrTranslation.Any(e => e.Id == id);
        }
    }
}
