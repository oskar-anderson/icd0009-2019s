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
    public class SharingItemController : Controller
    {
        private readonly AppDbContext _context;

        public SharingItemController(AppDbContext context)
        {
            _context = context;
        }

        // GET: SharingItem
        public async Task<IActionResult> Index()
        {
            return View(await _context.SharingItems.ToListAsync());
        }

        // GET: SharingItem/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sharingItem = await _context.SharingItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sharingItem == null)
            {
                return NotFound();
            }

            return View(sharingItem);
        }

        // GET: SharingItem/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SharingItem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SharingId,ItemId,FriendId,Percent,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] SharingItem sharingItem)
        {
            if (ModelState.IsValid)
            {
                sharingItem.Id = Guid.NewGuid();
                _context.Add(sharingItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sharingItem);
        }

        // GET: SharingItem/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sharingItem = await _context.SharingItems.FindAsync(id);
            if (sharingItem == null)
            {
                return NotFound();
            }
            return View(sharingItem);
        }

        // POST: SharingItem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("SharingId,ItemId,FriendId,Percent,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] SharingItem sharingItem)
        {
            if (id != sharingItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sharingItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SharingItemExists(sharingItem.Id))
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
            return View(sharingItem);
        }

        // GET: SharingItem/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sharingItem = await _context.SharingItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sharingItem == null)
            {
                return NotFound();
            }

            return View(sharingItem);
        }

        // POST: SharingItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var sharingItem = await _context.SharingItems.FindAsync(id);
            _context.SharingItems.Remove(sharingItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SharingItemExists(Guid id)
        {
            return _context.SharingItems.Any(e => e.Id == id);
        }
    }
}
