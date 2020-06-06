using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;
using Extensions;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    [Authorize]
    public class AnimalsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public AnimalsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Animals
        public async Task<IActionResult> Index()
        {
            var animals = await _uow.Animals.AllAsync(User.UserGuidId());
            return View(animals);
        }

        // GET: Animals/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = await _uow.Animals.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        // GET: Animals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Animals/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Animal animal)
        {
            animal.AppUserId = User.UserGuidId();

            if (ModelState.IsValid)
            {
                _uow.Animals.Add(animal);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(animal);
        }

        // GET: Animals/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = await _uow.Animals.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (animal == null)
            {
                return NotFound();
            }
            return View(animal);
        }

        // POST: Animals/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Animal animal)
        {
            if (id != animal.Id)
            {
                return NotFound();
            }
            animal.AppUserId = User.UserGuidId();

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.Animals.Update(animal);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _uow.Animals.ExistsAsync(id, User.UserGuidId()))
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
            return View(animal);
        }

        // GET: Animals/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = await _uow.Animals.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        // POST: Animals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.Animals.DeleteAsync(id, User.UserGuidId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
