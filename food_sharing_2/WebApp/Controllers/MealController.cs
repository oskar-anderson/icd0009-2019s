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
    public class MealController : Controller
    {
        private readonly AppDbContext _context;

        public MealController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Meal
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Meals.Include(m => m.Base).Include(m => m.Category).Include(m => m.Size);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Meal/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meal = await _context.Meals
                .Include(m => m.Base)
                .Include(m => m.Category)
                .Include(m => m.Size)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meal == null)
            {
                return NotFound();
            }

            return View(meal);
        }

        // GET: Meal/Create
        public IActionResult Create()
        {
            ViewData["BaseId"] = new SelectList(_context.Bases, "Id", "Id");
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id");
            ViewData["SizeId"] = new SelectList(_context.Sizes, "Id", "Id");
            return View();
        }

        // POST: Meal/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,SizeId,BaseId,Name,Picture,Modifications,Extras,Description,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] Meal meal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(meal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BaseId"] = new SelectList(_context.Bases, "Id", "Id", meal.BaseId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", meal.CategoryId);
            ViewData["SizeId"] = new SelectList(_context.Sizes, "Id", "Id", meal.SizeId);
            return View(meal);
        }

        // GET: Meal/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meal = await _context.Meals.FindAsync(id);
            if (meal == null)
            {
                return NotFound();
            }
            ViewData["BaseId"] = new SelectList(_context.Bases, "Id", "Id", meal.BaseId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", meal.CategoryId);
            ViewData["SizeId"] = new SelectList(_context.Sizes, "Id", "Id", meal.SizeId);
            return View(meal);
        }

        // POST: Meal/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CategoryId,SizeId,BaseId,Name,Picture,Modifications,Extras,Description,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] Meal meal)
        {
            if (id != meal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(meal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MealExists(meal.Id))
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
            ViewData["BaseId"] = new SelectList(_context.Bases, "Id", "Id", meal.BaseId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", meal.CategoryId);
            ViewData["SizeId"] = new SelectList(_context.Sizes, "Id", "Id", meal.SizeId);
            return View(meal);
        }

        // GET: Meal/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meal = await _context.Meals
                .Include(m => m.Base)
                .Include(m => m.Category)
                .Include(m => m.Size)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meal == null)
            {
                return NotFound();
            }

            return View(meal);
        }

        // POST: Meal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var meal = await _context.Meals.FindAsync(id);
            _context.Meals.Remove(meal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MealExists(string id)
        {
            return _context.Meals.Any(e => e.Id == id);
        }
    }
}
