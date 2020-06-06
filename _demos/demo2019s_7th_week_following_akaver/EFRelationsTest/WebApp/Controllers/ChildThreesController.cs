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
    public class ChildThreesController : Controller
    {
        private readonly AppDbContext _context;

        public ChildThreesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ChildThrees
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ChildThrees.Include(c => c.PrimaryThree);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ChildThrees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var childThree = await _context.ChildThrees
                .Include(c => c.PrimaryThree)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (childThree == null)
            {
                return NotFound();
            }

            return View(childThree);
        }

        // GET: ChildThrees/Create
        public IActionResult Create()
        {
            ViewData["PrimaryThreeId"] = new SelectList(_context.PrimaryThrees, "Id", "Value");
            return View();
        }

        // POST: ChildThrees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Value,PrimaryThreeId")] ChildThree childThree)
        {
            if (ModelState.IsValid)
            {
                _context.Add(childThree);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PrimaryThreeId"] = new SelectList(_context.PrimaryThrees, "Id", "Value", childThree.PrimaryThreeId);
            return View(childThree);
        }

        // GET: ChildThrees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var childThree = await _context.ChildThrees.FindAsync(id);
            if (childThree == null)
            {
                return NotFound();
            }
            ViewData["PrimaryThreeId"] = new SelectList(_context.PrimaryThrees, "Id", "Value", childThree.PrimaryThreeId);
            return View(childThree);
        }

        // POST: ChildThrees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Value,PrimaryThreeId")] ChildThree childThree)
        {
            if (id != childThree.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(childThree);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChildThreeExists(childThree.Id))
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
            ViewData["PrimaryThreeId"] = new SelectList(_context.PrimaryThrees, "Id", "Value", childThree.PrimaryThreeId);
            return View(childThree);
        }

        // GET: ChildThrees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var childThree = await _context.ChildThrees
                .Include(c => c.PrimaryThree)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (childThree == null)
            {
                return NotFound();
            }

            return View(childThree);
        }

        // POST: ChildThrees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var childThree = await _context.ChildThrees.FindAsync(id);
            _context.ChildThrees.Remove(childThree);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChildThreeExists(int id)
        {
            return _context.ChildThrees.Any(e => e.Id == id);
        }
    }
}
