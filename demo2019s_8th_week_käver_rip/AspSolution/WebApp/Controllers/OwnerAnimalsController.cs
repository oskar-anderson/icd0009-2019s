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
    public class OwnerAnimalsController : Controller
    {
        private readonly AppDbContext _context;

        public OwnerAnimalsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: OwnerAnimals
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.OwnerAnimals.Include(o => o.Animal).Include(o => o.Owner);
            return View(await appDbContext.ToListAsync());
        }

        // GET: OwnerAnimals/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ownerAnimal = await _context.OwnerAnimals
                .Include(o => o.Animal)
                .Include(o => o.Owner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ownerAnimal == null)
            {
                return NotFound();
            }

            return View(ownerAnimal);
        }

        // GET: OwnerAnimals/Create
        public IActionResult Create()
        {
            ViewData["AnimalId"] = new SelectList(_context.Animals, "Id", "AnimalName");
            ViewData["OwnerId"] = new SelectList(_context.Owners, "Id", "FirstName");
            return View();
        }

        // POST: OwnerAnimals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OwnerId,AnimalId,OwnedPercentage,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] OwnerAnimal ownerAnimal)
        {
            if (ModelState.IsValid)
            {
                ownerAnimal.Id = Guid.NewGuid();
                _context.Add(ownerAnimal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AnimalId"] = new SelectList(_context.Animals, "Id", "AnimalName", ownerAnimal.AnimalId);
            ViewData["OwnerId"] = new SelectList(_context.Owners, "Id", "FirstName", ownerAnimal.OwnerId);
            return View(ownerAnimal);
        }

        // GET: OwnerAnimals/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ownerAnimal = await _context.OwnerAnimals.FindAsync(id);
            if (ownerAnimal == null)
            {
                return NotFound();
            }
            ViewData["AnimalId"] = new SelectList(_context.Animals, "Id", "AnimalName", ownerAnimal.AnimalId);
            ViewData["OwnerId"] = new SelectList(_context.Owners, "Id", "FirstName", ownerAnimal.OwnerId);
            return View(ownerAnimal);
        }

        // POST: OwnerAnimals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("OwnerId,AnimalId,OwnedPercentage,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] OwnerAnimal ownerAnimal)
        {
            if (id != ownerAnimal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ownerAnimal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OwnerAnimalExists(ownerAnimal.Id))
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
            ViewData["AnimalId"] = new SelectList(_context.Animals, "Id", "AnimalName", ownerAnimal.AnimalId);
            ViewData["OwnerId"] = new SelectList(_context.Owners, "Id", "FirstName", ownerAnimal.OwnerId);
            return View(ownerAnimal);
        }

        // GET: OwnerAnimals/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ownerAnimal = await _context.OwnerAnimals
                .Include(o => o.Animal)
                .Include(o => o.Owner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ownerAnimal == null)
            {
                return NotFound();
            }

            return View(ownerAnimal);
        }

        // POST: OwnerAnimals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var ownerAnimal = await _context.OwnerAnimals.FindAsync(id);
            _context.OwnerAnimals.Remove(ownerAnimal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OwnerAnimalExists(Guid id)
        {
            return _context.OwnerAnimals.Any(e => e.Id == id);
        }
    }
}
