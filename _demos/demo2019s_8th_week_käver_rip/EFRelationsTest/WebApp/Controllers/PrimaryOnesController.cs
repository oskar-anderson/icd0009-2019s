using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain;
using WebApp.Data;

namespace WebApp.Controllers
{
    public class PrimaryOnesController : Controller
    {
        private readonly AppDbContext _context;

        public PrimaryOnesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: PrimaryOnes
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.PrimaryOnes.Include(p => p.ChildOne);
            return View(await appDbContext.ToListAsync());
        }

        // GET: PrimaryOnes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var primaryOne = await _context.PrimaryOnes
                .Include(p => p.ChildOne)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (primaryOne == null)
            {
                return NotFound();
            }

            return View(primaryOne);
        }

        // GET: PrimaryOnes/Create
        public IActionResult Create()
        {
            ViewData["ChildOneId"] = new SelectList(_context.ChildOnes, "Id", "Value");
            return View();
        }

        // POST: PrimaryOnes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Value,ChildOneId")] PrimaryOne primaryOne)
        {
            if (ModelState.IsValid)
            {
                _context.Add(primaryOne);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ChildOneId"] = new SelectList(_context.ChildOnes, "Id", "Value", primaryOne.ChildOneId);
            return View(primaryOne);
        }

        // GET: PrimaryOnes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var primaryOne = await _context.PrimaryOnes.FindAsync(id);
            if (primaryOne == null)
            {
                return NotFound();
            }
            ViewData["ChildOneId"] = new SelectList(_context.ChildOnes, "Id", "Value", primaryOne.ChildOneId);
            return View(primaryOne);
        }

        // POST: PrimaryOnes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Value,ChildOneId")] PrimaryOne primaryOne)
        {
            if (id != primaryOne.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(primaryOne);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrimaryOneExists(primaryOne.Id))
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
            ViewData["ChildOneId"] = new SelectList(_context.ChildOnes, "Id", "Value", primaryOne.ChildOneId);
            return View(primaryOne);
        }

        // GET: PrimaryOnes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var primaryOne = await _context.PrimaryOnes
                .Include(p => p.ChildOne)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (primaryOne == null)
            {
                return NotFound();
            }

            return View(primaryOne);
        }

        // POST: PrimaryOnes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var primaryOne = await _context.PrimaryOnes.FindAsync(id);
            _context.PrimaryOnes.Remove(primaryOne);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrimaryOneExists(int id)
        {
            return _context.PrimaryOnes.Any(e => e.Id == id);
        }
    }
}
