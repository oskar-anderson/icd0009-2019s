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
    public class BaseController : Controller
    {
        private readonly AppDbContext _context;

        public BaseController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Base
        public async Task<IActionResult> Index()
        {
            return View(await _context.Bases.ToListAsync());
        }

        // GET: Base/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @base = await _context.Bases
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@base == null)
            {
                return NotFound();
            }

            return View(@base);
        }

        // GET: Base/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Base/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] Base @base)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@base);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@base);
        }

        // GET: Base/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @base = await _context.Bases.FindAsync(id);
            if (@base == null)
            {
                return NotFound();
            }
            return View(@base);
        }

        // POST: Base/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] Base @base)
        {
            if (id != @base.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@base);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BaseExists(@base.Id))
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
            return View(@base);
        }

        // GET: Base/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @base = await _context.Bases
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@base == null)
            {
                return NotFound();
            }

            return View(@base);
        }

        // POST: Base/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var @base = await _context.Bases.FindAsync(id);
            _context.Bases.Remove(@base);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BaseExists(string id)
        {
            return _context.Bases.Any(e => e.Id == id);
        }
    }
}
