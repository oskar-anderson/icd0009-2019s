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
    [Authorize(Roles = "User")]
    public class RestaurantFoodController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public RestaurantFoodController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: RestaurantFood
        public async Task<IActionResult> Index()
        {
            var restaurantFoods = await _uow.RestaurantFoods.AllAsync();
            return View(restaurantFoods);
        }

        // GET: RestaurantFood/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurantFood = await _uow.RestaurantFoods.FirstOrDefaultAsync(id.Value, User.UserGuidId());

            if (restaurantFood == null)
            {
                return NotFound();
            }

            return View(restaurantFood);
        }

        // GET: RestaurantFood/Create
        public async Task<IActionResult> Create()
        {
            ViewData["MealId"] = new SelectList(await _uow.Pizzas.AllAsync(User.UserGuidId()),
                nameof(Pizza.Id),
                nameof(Pizza.Name));
            ViewData["PizzaId"] = new SelectList(await _uow.Meals.AllAsync(User.UserGuidId()),
                nameof(Meal.Id),
                nameof(Meal.Name));
            ViewData["RestaurantId"] = new SelectList(await _uow.Restaurants.AllAsync(User.UserGuidId()),
                nameof(Restaurant.Id),
                nameof(Restaurant.Name));
            return View();
        }

        // POST: RestaurantFood/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MealId,PizzaId,RestaurantId,Name,Tax,Gross,Since,Until,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] RestaurantFood restaurantFood)
        {
            if (ModelState.IsValid)
            {
                restaurantFood.Id = Guid.NewGuid();
                _uow.RestaurantFoods.Add(restaurantFood);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MealId"] = new SelectList(await _uow.Meals.AllAsync(User.UserGuidId()), "Id", "Name", restaurantFood.MealId);
            ViewData["PizzaId"] = new SelectList(await _uow.Pizzas.AllAsync(User.UserGuidId()), "Id", "Name", restaurantFood.PizzaId);
            ViewData["RestaurantId"] = new SelectList(await _uow.Restaurants.AllAsync(User.UserGuidId()), "Id", "Name", restaurantFood.RestaurantId);
            return View(restaurantFood);
        }

        // GET: RestaurantFood/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurantFood = await _uow.RestaurantFoods.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (restaurantFood == null)
            {
                return NotFound();
            }
            ViewData["MealId"] = new SelectList(await _uow.Meals.AllAsync(User.UserGuidId()), "Id", "Name", restaurantFood.MealId);
            ViewData["PizzaId"] = new SelectList(await _uow.Pizzas.AllAsync(User.UserGuidId()), "Id", "Name", restaurantFood.PizzaId);
            ViewData["RestaurantId"] = new SelectList(await _uow.Restaurants.AllAsync(User.UserGuidId()), "Id", "Name", restaurantFood.RestaurantId);
            return View(restaurantFood);
        }

        // POST: RestaurantFood/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("MealId,PizzaId,RestaurantId,Name,Tax,Gross,Since,Until,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] RestaurantFood restaurantFood)
        {
            if (id != restaurantFood.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.RestaurantFoods.Update(restaurantFood);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _uow.RestaurantFoods.ExistsAsync(id, User.UserGuidId()))
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
            ViewData["MealId"] = new SelectList(await _uow.Meals.AllAsync(User.UserGuidId()), "Id", "Name", restaurantFood.MealId);
            ViewData["PizzaId"] = new SelectList(await _uow.Pizzas.AllAsync(User.UserGuidId()), "Id", "Name", restaurantFood.PizzaId);
            ViewData["RestaurantId"] = new SelectList(await _uow.Restaurants.AllAsync(User.UserGuidId()), "Id", "Name", restaurantFood.RestaurantId);
            return View(restaurantFood);
        }

        // GET: RestaurantFood/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurantFood = await _uow.RestaurantFoods.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (restaurantFood == null)
            {
                return NotFound();
            }

            return View(restaurantFood);
        }

        // POST: RestaurantFood/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.RestaurantFoods.DeleteAsync(id, User.UserGuidId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
