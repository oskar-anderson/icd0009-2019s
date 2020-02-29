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
    public class ComponentPriceController : Controller
    {
        private readonly AppDbContext _context;

        public ComponentPriceController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ComponentPrice
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ComponentPrices.Include(c => c.Component).Include(c => c.Restaurant);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ComponentPrice/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var componentPrice = await _context.ComponentPrices
                .Include(c => c.Component)
                .Include(c => c.Restaurant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (componentPrice == null)
            {
                return NotFound();
            }

            return View(componentPrice);
        }

        // GET: ComponentPrice/Create
        public IActionResult Create()
        {
            ViewData["ComponentId"] = new SelectList(_context.Components, "Id", "Id");
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Id");
            return View();
        }

        // POST: ComponentPrice/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ComponentId,RestaurantId,Gross,Tax,Since,Until,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] ComponentPrice componentPrice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(componentPrice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ComponentId"] = new SelectList(_context.Components, "Id", "Id", componentPrice.ComponentId);
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Id", componentPrice.RestaurantId);
            return View(componentPrice);
        }

        // GET: ComponentPrice/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var componentPrice = await _context.ComponentPrices.FindAsync(id);
            if (componentPrice == null)
            {
                return NotFound();
            }
            ViewData["ComponentId"] = new SelectList(_context.Components, "Id", "Id", componentPrice.ComponentId);
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Id", componentPrice.RestaurantId);
            return View(componentPrice);
        }

        // POST: ComponentPrice/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ComponentId,RestaurantId,Gross,Tax,Since,Until,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] ComponentPrice componentPrice)
        {
            if (id != componentPrice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(componentPrice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComponentPriceExists(componentPrice.Id))
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
            ViewData["ComponentId"] = new SelectList(_context.Components, "Id", "Id", componentPrice.ComponentId);
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Id", componentPrice.RestaurantId);
            return View(componentPrice);
        }

        // GET: ComponentPrice/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var componentPrice = await _context.ComponentPrices
                .Include(c => c.Component)
                .Include(c => c.Restaurant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (componentPrice == null)
            {
                return NotFound();
            }

            return View(componentPrice);
        }

        // POST: ComponentPrice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var componentPrice = await _context.ComponentPrices.FindAsync(id);
            _context.ComponentPrices.Remove(componentPrice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComponentPriceExists(string id)
        {
            return _context.ComponentPrices.Any(e => e.Id == id);
        }
    }
}
