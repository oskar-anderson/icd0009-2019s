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
    public class CartController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public CartController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Cart
        public async Task<IActionResult> Index()
        {
            var cart = await _uow.Carts.AllAsync(User.UserGuidId());
            return View(cart);
        }

        // GET: Cart/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _uow.Carts.FirstOrDefaultAsync(id.Value, User.UserGuidId());

            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // GET: Cart/Create
        public async Task<IActionResult> Create()
        {
            ViewData["RestaurantId"] = new SelectList(await _uow.Restaurants.AllAsync(User.UserGuidId()), "Id", "Location");
            ViewData["UserLocationId"] = new SelectList(await _uow.UserLocations.AllAsync(User.UserGuidId()), "Id", "BuildingNumber");
            return View();
        }

        // POST: Cart/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppUserId,AsDelivery,UserLocationId,RestaurantId,Total,ReadyBy,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                _uow.Carts.Add(cart);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RestaurantId"] = new SelectList(await _uow.Restaurants.AllAsync(User.UserGuidId()), "Id", "Location", cart.RestaurantId);
            ViewData["UserLocationId"] = new SelectList(await _uow.UserLocations.AllAsync(User.UserGuidId()), "Id", "BuildingNumber", cart.UserLocationId);
            return View(cart);
        }

        // GET: Cart/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _uow.Carts.FirstOrDefaultAsync(id.Value, User.UserGuidId());

            if (cart == null)
            {
                return NotFound();
            }
            ViewData["RestaurantId"] = new SelectList(await _uow.Restaurants.AllAsync(User.UserGuidId()), "Id", "Location", cart.RestaurantId);
            ViewData["UserLocationId"] = new SelectList(await _uow.UserLocations.AllAsync(User.UserGuidId()), "Id", "BuildingNumber", cart.UserLocationId);
            return View(cart);
        }

        // POST: Cart/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AppUserId,AsDelivery,UserLocationId,RestaurantId,Total,ReadyBy,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Cart cart)
        {
            if (id != cart.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.Carts.Update(cart);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RestaurantId"] = new SelectList(await _uow.Restaurants.AllAsync(User.UserGuidId()), "Id", "Location", cart.RestaurantId);
            ViewData["UserLocationId"] = new SelectList(await _uow.UserLocations.AllAsync(User.UserGuidId()), "Id", "BuildingNumber", cart.UserLocationId);
            return View(cart);
        }

        // GET: Cart/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _uow.Carts.FirstOrDefaultAsync(id.Value, User.UserGuidId());

            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // POST: Cart/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.Carts.DeleteAsync(id, User.UserGuidId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
