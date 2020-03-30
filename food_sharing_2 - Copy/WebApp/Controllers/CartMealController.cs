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
    public class CartMealController : Controller
    {
        private readonly AppDbContext _context;

        public CartMealController(AppDbContext context)
        {
            _context = context;
        }

        // GET: CartMeal
        public async Task<IActionResult> Index()
        {
            return View(await _context.CartMeals.ToListAsync());
        }

        // GET: CartMeal/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartMeal = await _context.CartMeals
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cartMeal == null)
            {
                return NotFound();
            }

            return View(cartMeal);
        }

        // GET: CartMeal/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CartMeal/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CartId,MealId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] CartMeal cartMeal)
        {
            if (ModelState.IsValid)
            {
                cartMeal.Id = Guid.NewGuid();
                _context.Add(cartMeal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cartMeal);
        }

        // GET: CartMeal/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartMeal = await _context.CartMeals.FindAsync(id);
            if (cartMeal == null)
            {
                return NotFound();
            }
            return View(cartMeal);
        }

        // POST: CartMeal/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CartId,MealId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] CartMeal cartMeal)
        {
            if (id != cartMeal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cartMeal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartMealExists(cartMeal.Id))
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
            return View(cartMeal);
        }

        // GET: CartMeal/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartMeal = await _context.CartMeals
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cartMeal == null)
            {
                return NotFound();
            }

            return View(cartMeal);
        }

        // POST: CartMeal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var cartMeal = await _context.CartMeals.FindAsync(id);
            _context.CartMeals.Remove(cartMeal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartMealExists(Guid id)
        {
            return _context.CartMeals.Any(e => e.Id == id);
        }
    }
}
