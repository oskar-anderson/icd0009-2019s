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

namespace WebApp.Controllers
{
    public class ComponentPriceController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public ComponentPriceController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: ComponentPrice
        public async Task<IActionResult> Index()
        {
            var componentPrices = await _uow.ComponentPrices.AllAsync(User.UserGuidId());
            return View(componentPrices);
        }

        // GET: ComponentPrice/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var componentPrice = await _uow.ComponentPrices.FirstOrDefaultAsync(id.Value, User.UserGuidId());

            if (componentPrice == null)
            {
                return NotFound();
            }

            return View(componentPrice);
        }

        // GET: ComponentPrice/Create
        public async Task<IActionResult> Create()
        {
            ViewData["ComponentId"] = new SelectList(await _uow.Components.AllAsync(User.UserGuidId()), "Id", "Name");
            ViewData["RestaurantId"] = new SelectList(await _uow.Restaurants.AllAsync(User.UserGuidId()), "Id", "Location");
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
                _uow.ComponentPrices.Add(componentPrice);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ComponentId"] = new SelectList(await _uow.Components.AllAsync(User.UserGuidId()), "Id", "Name", componentPrice.ComponentId);
            ViewData["RestaurantId"] = new SelectList(await _uow.Restaurants.AllAsync(User.UserGuidId()), "Id", "Location", componentPrice.RestaurantId);
            return View(componentPrice);
        }

        // GET: ComponentPrice/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var componentPrice = await _uow.ComponentPrices.FirstOrDefaultAsync(id.Value, User.UserGuidId());

            if (componentPrice == null)
            {
                return NotFound();
            }
            ViewData["ComponentId"] = new SelectList(await _uow.Components.AllAsync(User.UserGuidId()), "Id", "Name", componentPrice.ComponentId);
            ViewData["RestaurantId"] = new SelectList(await _uow.Restaurants.AllAsync(User.UserGuidId()), "Id", "Location", componentPrice.RestaurantId);
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
                _uow.ComponentPrices.Update(componentPrice);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ComponentId"] = new SelectList(await _uow.Components.AllAsync(User.UserGuidId()), "Id", "Name", componentPrice.ComponentId);
            ViewData["RestaurantId"] = new SelectList(await _uow.Restaurants.AllAsync(User.UserGuidId()), "Id", "Location", componentPrice.RestaurantId);
            return View(componentPrice);
        }

        // GET: ComponentPrice/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var componentPrice = await _uow.ComponentPrices.FirstOrDefaultAsync(id.Value, User.UserGuidId());

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
            await _uow.ComponentPrices.DeleteAsync(id, User.UserGuidId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
