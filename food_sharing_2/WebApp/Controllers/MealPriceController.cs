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
    public class MealPriceController : Controller
    {
        private readonly AppDbContext _context;

        public MealPriceController(AppDbContext context)
        {
            _context = context;
        }

        // GET: MealPrice
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.MealPrices.Include(m => m.ClientGroup).Include(m => m.Meal).Include(m => m.Restaurant);
            return View(await appDbContext.ToListAsync());
        }

        // GET: MealPrice/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mealPrice = await _context.MealPrices
                .Include(m => m.ClientGroup)
                .Include(m => m.Meal)
                .Include(m => m.Restaurant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mealPrice == null)
            {
                return NotFound();
            }

            return View(mealPrice);
        }

        // GET: MealPrice/Create
        public IActionResult Create()
        {
            ViewData["ClientGroupId"] = new SelectList(_context.ClientGroups, "Id", "Id");
            ViewData["MealId"] = new SelectList(_context.Meals, "Id", "Id");
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Id");
            return View();
        }

        // POST: MealPrice/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MealId,RestaurantId,ClientGroupId,Name,Tax,Gross,Since,Until,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] MealPrice mealPrice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mealPrice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientGroupId"] = new SelectList(_context.ClientGroups, "Id", "Id", mealPrice.ClientGroupId);
            ViewData["MealId"] = new SelectList(_context.Meals, "Id", "Id", mealPrice.MealId);
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Id", mealPrice.RestaurantId);
            return View(mealPrice);
        }

        // GET: MealPrice/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mealPrice = await _context.MealPrices.FindAsync(id);
            if (mealPrice == null)
            {
                return NotFound();
            }
            ViewData["ClientGroupId"] = new SelectList(_context.ClientGroups, "Id", "Id", mealPrice.ClientGroupId);
            ViewData["MealId"] = new SelectList(_context.Meals, "Id", "Id", mealPrice.MealId);
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Id", mealPrice.RestaurantId);
            return View(mealPrice);
        }

        // POST: MealPrice/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MealId,RestaurantId,ClientGroupId,Name,Tax,Gross,Since,Until,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] MealPrice mealPrice)
        {
            if (id != mealPrice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mealPrice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MealPriceExists(mealPrice.Id))
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
            ViewData["ClientGroupId"] = new SelectList(_context.ClientGroups, "Id", "Id", mealPrice.ClientGroupId);
            ViewData["MealId"] = new SelectList(_context.Meals, "Id", "Id", mealPrice.MealId);
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Id", mealPrice.RestaurantId);
            return View(mealPrice);
        }

        // GET: MealPrice/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mealPrice = await _context.MealPrices
                .Include(m => m.ClientGroup)
                .Include(m => m.Meal)
                .Include(m => m.Restaurant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mealPrice == null)
            {
                return NotFound();
            }

            return View(mealPrice);
        }

        // POST: MealPrice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var mealPrice = await _context.MealPrices.FindAsync(id);
            _context.MealPrices.Remove(mealPrice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MealPriceExists(string id)
        {
            return _context.MealPrices.Any(e => e.Id == id);
        }
    }
}
