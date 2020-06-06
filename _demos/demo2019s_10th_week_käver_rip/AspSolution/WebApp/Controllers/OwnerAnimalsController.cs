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
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [Authorize]
    public class OwnerAnimalsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public OwnerAnimalsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: OwnerAnimals
        public async Task<IActionResult> Index()
        {
            var ownerAnimals = await _uow.OwnerAnimals.AllAsync();

            return View(ownerAnimals);
        }

        // GET: OwnerAnimals/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ownerAnimal = await _uow.OwnerAnimals.FirstOrDefaultAsync(id.Value, User.UserGuidId());

            if (ownerAnimal == null)
            {
                return NotFound();
            }

            return View(ownerAnimal);
        }


        // GET: OwnerAnimals/Create
        public async Task<IActionResult> Create()
        {
            var vm = new OwnerAnimalCreateEditVM();
            
            vm.AnimalSelectList = new SelectList(await _uow.Animals.AllAsync(User.UserGuidId()), nameof(Animal.Id),
                nameof(Animal.AnimalName));
            vm.OwnerSelectList = new SelectList(await _uow.Owners.AllAsync(User.UserGuidId()), nameof(Owner.Id),
                nameof(Owner.FirstLastName));

            return View(vm);
        }

        // POST: OwnerAnimals/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OwnerAnimalCreateEditVM vm)
        {
            if (ModelState.IsValid)
            {
                _uow.OwnerAnimals.Add(vm.OwnerAnimal);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            vm.AnimalSelectList = new SelectList(await _uow.Animals.AllAsync(User.UserGuidId()), nameof(Animal.Id),
                nameof(Animal.AnimalName), vm.OwnerAnimal.AnimalId);
            vm.OwnerSelectList = new SelectList(await _uow.Owners.AllAsync(User.UserGuidId()), nameof(Owner.Id),
                nameof(Owner.FirstLastName), vm.OwnerAnimal.OwnerId);

            return View(vm);
        }


        // GET: OwnerAnimals/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vm = new OwnerAnimalCreateEditVM();

            vm.OwnerAnimal = await _uow.OwnerAnimals.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (vm.OwnerAnimal == null)
            {
                return NotFound();
            }

            vm.AnimalSelectList = new SelectList(await _uow.Animals.AllAsync(User.UserGuidId()), nameof(Animal.Id),
                nameof(Animal.AnimalName), vm.OwnerAnimal.AnimalId);
            vm.OwnerSelectList = new SelectList(await _uow.Owners.AllAsync(User.UserGuidId()), nameof(Owner.Id),
                nameof(Owner.FirstLastName), vm.OwnerAnimal.OwnerId);

            return View(vm);
        }

        // POST: OwnerAnimals/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, OwnerAnimalCreateEditVM vm)
        {
            if (id != vm.OwnerAnimal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.OwnerAnimals.Update(vm.OwnerAnimal);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _uow.OwnerAnimals.ExistsAsync(id, User.UserGuidId()))
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


            vm.AnimalSelectList = new SelectList(await _uow.Animals.AllAsync(User.UserGuidId()), nameof(Animal.Id),
                nameof(Animal.AnimalName), vm.OwnerAnimal.AnimalId);
            vm.OwnerSelectList = new SelectList(await _uow.Owners.AllAsync(User.UserGuidId()), nameof(Owner.Id),
                nameof(Owner.FirstLastName), vm.OwnerAnimal.OwnerId);

            return View(vm);
        }


        // GET: OwnerAnimals/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ownerAnimal = await _uow.OwnerAnimals.FirstOrDefaultAsync(id.Value, User.UserGuidId());
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
            await _uow.OwnerAnimals.DeleteAsync(id, User.UserGuidId());
            await _uow.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
    }
}