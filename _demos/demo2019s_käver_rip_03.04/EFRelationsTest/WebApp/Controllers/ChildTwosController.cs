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
    public class ChildTwosController : Controller
    {
        private readonly AppDbContext _context;

        public ChildTwosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ChildTwos
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ChildTwos.Include(c => c.PrimaryTwo);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ChildTwos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var childTwo = await _context.ChildTwos
                .Include(c => c.PrimaryTwo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (childTwo == null)
            {
                return NotFound();
            }

            return View(childTwo);
        }

        // GET: ChildTwos/Create
        public IActionResult Create()
        {
            ViewData["PrimaryTwoId"] = new SelectList(_context.PrimaryTwos, "Id", "Value");
            return View();
        }

        // POST: ChildTwos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Value,PrimaryTwoId")] ChildTwo childTwo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(childTwo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PrimaryTwoId"] = new SelectList(_context.PrimaryTwos, "Id", "Value", childTwo.PrimaryTwoId);
            return View(childTwo);
        }

        // GET: ChildTwos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var childTwo = await _context.ChildTwos.FindAsync(id);
            if (childTwo == null)
            {
                return NotFound();
            }
            ViewData["PrimaryTwoId"] = new SelectList(_context.PrimaryTwos, "Id", "Value", childTwo.PrimaryTwoId);
            return View(childTwo);
        }

        // POST: ChildTwos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Value,PrimaryTwoId")] ChildTwo childTwo)
        {
            if (id != childTwo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(childTwo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChildTwoExists(childTwo.Id))
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
            ViewData["PrimaryTwoId"] = new SelectList(_context.PrimaryTwos, "Id", "Value", childTwo.PrimaryTwoId);
            return View(childTwo);
        }

        // GET: ChildTwos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var childTwo = await _context.ChildTwos
                .Include(c => c.PrimaryTwo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (childTwo == null)
            {
                return NotFound();
            }

            return View(childTwo);
        }

        // POST: ChildTwos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var childTwo = await _context.ChildTwos.FindAsync(id);
            _context.ChildTwos.Remove(childTwo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChildTwoExists(int id)
        {
            return _context.ChildTwos.Any(e => e.Id == id);
        }
    }
}
