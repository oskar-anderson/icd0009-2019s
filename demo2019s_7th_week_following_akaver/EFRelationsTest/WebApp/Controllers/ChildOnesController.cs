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
    public class ChildOnesController : Controller
    {
        private readonly AppDbContext _context;

        public ChildOnesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ChildOnes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ChildOnes.ToListAsync());
        }

        // GET: ChildOnes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var childOne = await _context.ChildOnes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (childOne == null)
            {
                return NotFound();
            }

            return View(childOne);
        }

        // GET: ChildOnes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ChildOnes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Value")] ChildOne childOne)
        {
            if (ModelState.IsValid)
            {
                _context.Add(childOne);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(childOne);
        }

        // GET: ChildOnes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var childOne = await _context.ChildOnes.FindAsync(id);
            if (childOne == null)
            {
                return NotFound();
            }
            return View(childOne);
        }

        // POST: ChildOnes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Value")] ChildOne childOne)
        {
            if (id != childOne.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(childOne);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChildOneExists(childOne.Id))
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
            return View(childOne);
        }

        // GET: ChildOnes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var childOne = await _context.ChildOnes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (childOne == null)
            {
                return NotFound();
            }

            return View(childOne);
        }

        // POST: ChildOnes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var childOne = await _context.ChildOnes.FindAsync(id);
            _context.ChildOnes.Remove(childOne);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChildOneExists(int id)
        {
            return _context.ChildOnes.Any(e => e.Id == id);
        }
    }
}
