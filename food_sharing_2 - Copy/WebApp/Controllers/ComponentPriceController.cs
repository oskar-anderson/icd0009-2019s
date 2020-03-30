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
            return View(await _context.ComponentPrices.ToListAsync());
        }

        // GET: ComponentPrice/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var componentPrice = await _context.ComponentPrices
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
            return View();
        }

        // POST: ComponentPrice/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ComponentId,RestaurantId,Gross,Tax,Since,Until,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] ComponentPrice componentPrice)
        {
            if (ModelState.IsValid)
            {
                componentPrice.Id = Guid.NewGuid();
                _context.Add(componentPrice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(componentPrice);
        }

        // GET: ComponentPrice/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
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
            return View(componentPrice);
        }

        // POST: ComponentPrice/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ComponentId,RestaurantId,Gross,Tax,Since,Until,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] ComponentPrice componentPrice)
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
            return View(componentPrice);
        }

        // GET: ComponentPrice/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var componentPrice = await _context.ComponentPrices
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
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var componentPrice = await _context.ComponentPrices.FindAsync(id);
            _context.ComponentPrices.Remove(componentPrice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComponentPriceExists(Guid id)
        {
            return _context.ComponentPrices.Any(e => e.Id == id);
        }
    }
}
