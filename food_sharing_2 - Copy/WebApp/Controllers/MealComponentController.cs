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
            return View(await _context.MealComponents.ToListAsync());
        }

        // GET: MealComponent/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mealComponent = await _context.MealComponents
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
            return View();
        }

        // POST: MealComponent/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ComponentId,MealId,Amount,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] MealComponent mealComponent)
        {
            if (ModelState.IsValid)
            {
                mealComponent.Id = Guid.NewGuid();
                _context.Add(mealComponent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mealComponent);
        }

        // GET: MealComponent/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
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
            return View(mealComponent);
        }

        // POST: MealComponent/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ComponentId,MealId,Amount,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] MealComponent mealComponent)
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
            return View(mealComponent);
        }

        // GET: MealComponent/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mealComponent = await _context.MealComponents
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
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var mealComponent = await _context.MealComponents.FindAsync(id);
            _context.MealComponents.Remove(mealComponent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MealComponentExists(Guid id)
        {
            return _context.MealComponents.Any(e => e.Id == id);
        }
    }
}
