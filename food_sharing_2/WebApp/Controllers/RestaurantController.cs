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
    public class RestaurantController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public RestaurantController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Restaurant
        public async Task<IActionResult> Index()
        {
            var restaurants = await _uow.Restaurants.AllAsync();
            return View(restaurants);
        }

        // GET: Restaurant/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var restaurant = await _uow.Restaurants
                .FirstOrDefaultAsync(id.Value, User.UserGuidId());
            
            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }

        // GET: Restaurant/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Restaurant/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Location,Telephone,OpenTime,OpenNotification,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                _uow.Restaurants.Add(restaurant);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(restaurant);
        }

        // GET: Restaurant/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = await _uow.Restaurants.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            
            if (restaurant == null)
            {
                return NotFound();
            }
            return View(restaurant);
        }

        // POST: Restaurant/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Location,Telephone,OpenTime,OpenNotification,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Restaurant restaurant)
        {
            if (id != restaurant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.Restaurants.Update(restaurant);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(restaurant);
        }

        // GET: Restaurant/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = await _uow.Restaurants
                .FirstOrDefaultAsync(id.Value, User.UserGuidId());
            
            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }

        // POST: Restaurant/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.Restaurants.DeleteAsync(id, User.UserGuidId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
