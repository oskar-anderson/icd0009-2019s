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
    public class OwnersController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public OwnersController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Owners
        public async Task<IActionResult> Index()
        {
            var owners = await _uow.Owners.AllAsync(User.UserGuidId());

            return View(owners);
        }


        // GET: Owners/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _uow.Owners.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (owner == null)
            {
                return NotFound();
            }

            return View(owner);
        }

        // GET: Owners/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Owners/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            Owner owner)
        {
            owner.AppUserId = User.UserGuidId();

            if (ModelState.IsValid)
            {
                //owner.Id = Guid.NewGuid();
                _uow.Owners.Add(owner);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(owner);
        }

        // GET: Owners/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _uow.Owners.FirstOrDefaultAsync(id.Value, User.UserGuidId());

            if (owner == null)
            {
                return NotFound();
            }

            return View(owner);
        }

        // POST: Owners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Owner owner)
        {
            owner.AppUserId = User.UserGuidId();

            if (id != owner.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.Owners.Update(owner);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await _uow.Owners.ExistsAsync(owner.Id, User.UserGuidId()))
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

            return View(owner);
        }

        // GET: Owners/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _uow.Owners.FirstOrDefaultAsync(id.Value, User.UserGuidId());

            if (owner == null)
            {
                return NotFound();
            }

            return View(owner);
        }

        // POST: Owners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.Owners.DeleteAsync(id, User.UserGuidId());
            await _uow.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }


    }
}