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
    public class PrimaryTwosController : Controller
    {
        private readonly AppDbContext _context;

        public PrimaryTwosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: PrimaryTwos
        public async Task<IActionResult> Index()
        {
            return View(await _context.PrimaryTwos.ToListAsync());
        }

        // GET: PrimaryTwos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var primaryTwo = await _context.PrimaryTwos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (primaryTwo == null)
            {
                return NotFound();
            }

            return View(primaryTwo);
        }

        // GET: PrimaryTwos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PrimaryTwos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Value")] PrimaryTwo primaryTwo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(primaryTwo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(primaryTwo);
        }

        // GET: PrimaryTwos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var primaryTwo = await _context.PrimaryTwos.FindAsync(id);
            if (primaryTwo == null)
            {
                return NotFound();
            }
            return View(primaryTwo);
        }

        // POST: PrimaryTwos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Value")] PrimaryTwo primaryTwo)
        {
            if (id != primaryTwo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(primaryTwo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrimaryTwoExists(primaryTwo.Id))
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
            return View(primaryTwo);
        }

        // GET: PrimaryTwos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var primaryTwo = await _context.PrimaryTwos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (primaryTwo == null)
            {
                return NotFound();
            }

            return View(primaryTwo);
        }

        // POST: PrimaryTwos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var primaryTwo = await _context.PrimaryTwos.FindAsync(id);
            _context.PrimaryTwos.Remove(primaryTwo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrimaryTwoExists(int id)
        {
            return _context.PrimaryTwos.Any(e => e.Id == id);
        }
    }
}
