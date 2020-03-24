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
    public class SharingController : Controller
    {
        private readonly AppDbContext _context;

        public SharingController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Sharing
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sharings.ToListAsync());
        }

        // GET: Sharing/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sharing = await _context.Sharings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sharing == null)
            {
                return NotFound();
            }

            return View(sharing);
        }

        // GET: Sharing/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sharing/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppUserId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Sharing sharing)
        {
            if (ModelState.IsValid)
            {
                sharing.Id = Guid.NewGuid();
                _context.Add(sharing);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sharing);
        }

        // GET: Sharing/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sharing = await _context.Sharings.FindAsync(id);
            if (sharing == null)
            {
                return NotFound();
            }
            return View(sharing);
        }

        // POST: Sharing/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AppUserId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Sharing sharing)
        {
            if (id != sharing.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sharing);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SharingExists(sharing.Id))
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
            return View(sharing);
        }

        // GET: Sharing/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sharing = await _context.Sharings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sharing == null)
            {
                return NotFound();
            }

            return View(sharing);
        }

        // POST: Sharing/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var sharing = await _context.Sharings.FindAsync(id);
            _context.Sharings.Remove(sharing);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SharingExists(Guid id)
        {
            return _context.Sharings.Any(e => e.Id == id);
        }
    }
}
