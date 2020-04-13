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
    public class CartMealController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public CartMealController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: CartMeal
        public async Task<IActionResult> Index()
        {
            var cartMeal = await _uow.CartMeals.AllAsync(User.UserGuidId());
            return View(cartMeal);
        }

        // GET: CartMeal/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartMeal = await _uow.CartMeals.FirstOrDefaultAsync(id.Value, User.UserGuidId());

            if (cartMeal == null)
            {
                return NotFound();
            }

            return View(cartMeal);
        }

        // GET: CartMeal/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CartId"] = new SelectList(await _uow.Carts.AllAsync(User.UserGuidId()), "Id", "Id");
            ViewData["MealId"] = new SelectList(await _uow.Meals.AllAsync(User.UserGuidId()), "Id", "Name");
            ViewData["PizzaFinalId"] = new SelectList(await _uow.PizzaFinals.AllAsync(User.UserGuidId()), "Id", "Changes");
            return View();
        }

        // POST: CartMeal/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CartId,MealId,PizzaFinalId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] CartMeal cartMeal)
        {
            if (ModelState.IsValid)
            {
                _uow.CartMeals.Add(cartMeal);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CartId"] = new SelectList(await _uow.Carts.AllAsync(User.UserGuidId()), "Id", "Id", cartMeal.CartId);
            ViewData["MealId"] = new SelectList(await _uow.Meals.AllAsync(User.UserGuidId()), "Id", "Name", cartMeal.MealId);
            ViewData["PizzaFinalId"] = new SelectList(await _uow.PizzaFinals.AllAsync(User.UserGuidId()), "Id", "Changes", cartMeal.PizzaFinalId);
            return View(cartMeal);
        }

        // GET: CartMeal/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartMeal = await _uow.CartMeals.FirstOrDefaultAsync(id.Value, User.UserGuidId());

            if (cartMeal == null)
            {
                return NotFound();
            }
            ViewData["CartId"] = new SelectList(await _uow.Carts.AllAsync(User.UserGuidId()), "Id", "Id", cartMeal.CartId);
            ViewData["MealId"] = new SelectList(await _uow.Meals.AllAsync(User.UserGuidId()), "Id", "Name", cartMeal.MealId);
            ViewData["PizzaFinalId"] = new SelectList(await _uow.PizzaFinals.AllAsync(User.UserGuidId()), "Id", "Changes", cartMeal.PizzaFinalId);
            return View(cartMeal);
        }

        // POST: CartMeal/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CartId,MealId,PizzaFinalId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] CartMeal cartMeal)
        {
            if (id != cartMeal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.CartMeals.Update(cartMeal);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CartId"] = new SelectList(await _uow.Carts.AllAsync(User.UserGuidId()), "Id", "Id", cartMeal.CartId);
            ViewData["MealId"] = new SelectList(await _uow.Meals.AllAsync(User.UserGuidId()), "Id", "Name", cartMeal.MealId);
            ViewData["PizzaFinalId"] = new SelectList(await _uow.PizzaFinals.AllAsync(User.UserGuidId()), "Id", "Changes", cartMeal.PizzaFinalId);
            return View(cartMeal);
        }

        // GET: CartMeal/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartMeal = await _uow.CartMeals.FirstOrDefaultAsync(id.Value, User.UserGuidId());

            if (cartMeal == null)
            {
                return NotFound();
            }

            return View(cartMeal);
        }

        // POST: CartMeal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.CartMeals.DeleteAsync(id, User.UserGuidId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
