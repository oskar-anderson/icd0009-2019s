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
    public class MenuMealController : Controller
    {
        private readonly AppDbContext _context;

        public MenuMealController(AppDbContext context)
        {
            _context = context;
        }

        // GET: MenuMeal
        public async Task<IActionResult> Index()
        {
            return View(await _context.MenuMeals.ToListAsync());
        }

        // GET: MenuMeal/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuMeal = await _context.MenuMeals
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menuMeal == null)
            {
                return NotFound();
            }

            return View(menuMeal);
        }

        // GET: MenuMeal/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MenuMeal/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MealId,MenuId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] MenuMeal menuMeal)
        {
            if (ModelState.IsValid)
            {
                menuMeal.Id = Guid.NewGuid();
                _context.Add(menuMeal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(menuMeal);
        }

        // GET: MenuMeal/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuMeal = await _context.MenuMeals.FindAsync(id);
            if (menuMeal == null)
            {
                return NotFound();
            }
            return View(menuMeal);
        }

        // POST: MenuMeal/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("MealId,MenuId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] MenuMeal menuMeal)
        {
            if (id != menuMeal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menuMeal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuMealExists(menuMeal.Id))
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
            return View(menuMeal);
        }

        // GET: MenuMeal/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuMeal = await _context.MenuMeals
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menuMeal == null)
            {
                return NotFound();
            }

            return View(menuMeal);
        }

        // POST: MenuMeal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var menuMeal = await _context.MenuMeals.FindAsync(id);
            _context.MenuMeals.Remove(menuMeal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuMealExists(Guid id)
        {
            return _context.MenuMeals.Any(e => e.Id == id);
        }
    }
}
