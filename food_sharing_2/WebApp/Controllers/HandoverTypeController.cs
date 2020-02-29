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
    public class HandoverTypeController : Controller
    {
        private readonly AppDbContext _context;

        public HandoverTypeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: HandoverType
        public async Task<IActionResult> Index()
        {
            return View(await _context.HandoverTypes.ToListAsync());
        }

        // GET: HandoverType/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var handoverType = await _context.HandoverTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (handoverType == null)
            {
                return NotFound();
            }

            return View(handoverType);
        }

        // GET: HandoverType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HandoverType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] HandoverType handoverType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(handoverType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(handoverType);
        }

        // GET: HandoverType/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var handoverType = await _context.HandoverTypes.FindAsync(id);
            if (handoverType == null)
            {
                return NotFound();
            }
            return View(handoverType);
        }

        // POST: HandoverType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] HandoverType handoverType)
        {
            if (id != handoverType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(handoverType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HandoverTypeExists(handoverType.Id))
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
            return View(handoverType);
        }

        // GET: HandoverType/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var handoverType = await _context.HandoverTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (handoverType == null)
            {
                return NotFound();
            }

            return View(handoverType);
        }

        // POST: HandoverType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var handoverType = await _context.HandoverTypes.FindAsync(id);
            _context.HandoverTypes.Remove(handoverType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HandoverTypeExists(string id)
        {
            return _context.HandoverTypes.Any(e => e.Id == id);
        }
    }
}
