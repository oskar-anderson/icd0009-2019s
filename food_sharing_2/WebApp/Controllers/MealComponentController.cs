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
    public class MealComponentController : Controller
    {
        private readonly AppDbContext _context;

        public MealComponentController(AppDbContext context)
        {
            _context = context;
        }

        // GET: MealComponent
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.MealComponents.Include(m => m.Component).Include(m => m.Meal);
            return View(await appDbContext.ToListAsync());
        }

        // GET: MealComponent/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mealComponent = await _context.MealComponents
                .Include(m => m.Component)
                .Include(m => m.Meal)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mealComponent == null)
            {
                return NotFound();
            }

            return View(mealComponent);
        }

        // GET: MealComponent/Create
        public IActionResult Create()
        {
            ViewData["ComponentId"] = new SelectList(_context.Components, "Id", "Id");
            ViewData["MealId"] = new SelectList(_context.Meals, "Id", "Id");
            return View();
        }

        // POST: MealComponent/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ComponentId,MealId,Amount,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] MealComponent mealComponent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mealComponent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ComponentId"] = new SelectList(_context.Components, "Id", "Id", mealComponent.ComponentId);
            ViewData["MealId"] = new SelectList(_context.Meals, "Id", "Id", mealComponent.MealId);
            return View(mealComponent);
        }

        // GET: MealComponent/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mealComponent = await _context.MealComponents.FindAsync(id);
            if (mealComponent == null)
            {
                return NotFound();
            }
            ViewData["ComponentId"] = new SelectList(_context.Components, "Id", "Id", mealComponent.ComponentId);
            ViewData["MealId"] = new SelectList(_context.Meals, "Id", "Id", mealComponent.MealId);
            return View(mealComponent);
        }

        // POST: MealComponent/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ComponentId,MealId,Amount,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] MealComponent mealComponent)
        {
            if (id != mealComponent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mealComponent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MealComponentExists(mealComponent.Id))
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
            ViewData["ComponentId"] = new SelectList(_context.Components, "Id", "Id", mealComponent.ComponentId);
            ViewData["MealId"] = new SelectList(_context.Meals, "Id", "Id", mealComponent.MealId);
            return View(mealComponent);
        }

        // GET: MealComponent/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mealComponent = await _context.MealComponents
                .Include(m => m.Component)
                .Include(m => m.Meal)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mealComponent == null)
            {
                return NotFound();
            }

            return View(mealComponent);
        }

        // POST: MealComponent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var mealComponent = await _context.MealComponents.FindAsync(id);
            _context.MealComponents.Remove(mealComponent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MealComponentExists(string id)
        {
            return _context.MealComponents.Any(e => e.Id == id);
        }
    }
}
