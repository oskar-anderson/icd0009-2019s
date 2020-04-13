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
    public class PrimaryThreesController : Controller
    {
        private readonly AppDbContext _context;

        public PrimaryThreesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: PrimaryThrees
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.PrimaryThrees.Include(p => p.ChildThree);
            return View(await appDbContext.ToListAsync());
        }

        // GET: PrimaryThrees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var primaryThree = await _context.PrimaryThrees
                .Include(p => p.ChildThree)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (primaryThree == null)
            {
                return NotFound();
            }

            return View(primaryThree);
        }

        // GET: PrimaryThrees/Create
        public IActionResult Create()
        {
            ViewData["ChildThreeId"] = new SelectList(_context.ChildThrees, "Id", "Value");
            return View();
        }

        // POST: PrimaryThrees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Value,ChildThreeId")] PrimaryThree primaryThree)
        {
            if (ModelState.IsValid)
            {
                _context.Add(primaryThree);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ChildThreeId"] = new SelectList(_context.ChildThrees, "Id", "Value", primaryThree.ChildThreeId);
            return View(primaryThree);
        }

        // GET: PrimaryThrees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var primaryThree = await _context.PrimaryThrees.FindAsync(id);
            if (primaryThree == null)
            {
                return NotFound();
            }
            ViewData["ChildThreeId"] = new SelectList(_context.ChildThrees, "Id", "Value", primaryThree.ChildThreeId);
            return View(primaryThree);
        }

        // POST: PrimaryThrees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Value,ChildThreeId")] PrimaryThree primaryThree)
        {
            if (id != primaryThree.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(primaryThree);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrimaryThreeExists(primaryThree.Id))
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
            ViewData["ChildThreeId"] = new SelectList(_context.ChildThrees, "Id", "Value", primaryThree.ChildThreeId);
            return View(primaryThree);
        }

        // GET: PrimaryThrees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var primaryThree = await _context.PrimaryThrees
                .Include(p => p.ChildThree)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (primaryThree == null)
            {
                return NotFound();
            }

            return View(primaryThree);
        }

        // POST: PrimaryThrees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var primaryThree = await _context.PrimaryThrees.FindAsync(id);
            _context.PrimaryThrees.Remove(primaryThree);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrimaryThreeExists(int id)
        {
            return _context.PrimaryThrees.Any(e => e.Id == id);
        }
    }
}
