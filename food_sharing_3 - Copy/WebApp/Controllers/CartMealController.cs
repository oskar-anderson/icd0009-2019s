using System;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Extensions;

namespace WebApp.Controllers
{
    public class CartMealController : Controller
    {
        private readonly IAppBLL _bll;

        public CartMealController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: CartMeal
        public async Task<IActionResult> Index()
        {
            var cartMeals = await _bll.CartMeals.GetAllAsyncBase();
            return View(cartMeals);
        }

        // GET: CartMeal/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartMeal = await _bll.CartMeals.FirstOrDefaultAsync(id.Value);

            if (cartMeal == null)
            {
                return NotFound();
            }

            return View(cartMeal);
        }

        // GET: CartMeal/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CartId"] = new SelectList(await _bll.Carts.GetAllAsyncBase(), "Id", "Id");
            ViewData["MealId"] = new SelectList(await _bll.Meals.GetAllAsyncBase(), "Id", "Name");
            ViewData["PizzaFinalId"] = new SelectList(await _bll.PizzaFinals.GetAllAsyncBase(), "Id", "Changes");
            return View();
        }

        // POST: CartMeal/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BLL.App.DTO.CartMeal cartMeal)
        {
            if (ModelState.IsValid)
            {
                _bll.CartMeals.Add(cartMeal);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CartId"] = new SelectList(await _bll.Carts.GetAllAsyncBase(), "Id", "Id", cartMeal.CartId);
            ViewData["MealId"] = new SelectList(await _bll.Meals.GetAllAsyncBase(), "Id", "Name", cartMeal.MealId);
            ViewData["PizzaFinalId"] = new SelectList(await _bll.PizzaFinals.GetAllAsyncBase(), "Id", "Changes", cartMeal.PizzaFinalId);
            return View(cartMeal);
        }

        // GET: CartMeal/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartMeal = await _bll.CartMeals.FirstOrDefaultAsync(id.Value);

            if (cartMeal == null)
            {
                return NotFound();
            }
            ViewData["CartId"] = new SelectList(await _bll.Carts.GetAllAsyncBase(), "Id", "Id", cartMeal.CartId);
            ViewData["MealId"] = new SelectList(await _bll.Meals.GetAllAsyncBase(), "Id", "Name", cartMeal.MealId);
            ViewData["PizzaFinalId"] = new SelectList(await _bll.PizzaFinals.GetAllAsyncBase(), "Id", "Changes", cartMeal.PizzaFinalId);
            return View(cartMeal);
        }

        // POST: CartMeal/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, BLL.App.DTO.CartMeal cartMeal)
        {
            if (id != cartMeal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _bll.CartMeals.UpdateAsync(cartMeal);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CartId"] = new SelectList(await _bll.Carts.GetAllAsyncBase(), "Id", "Id", cartMeal.CartId);
            ViewData["MealId"] = new SelectList(await _bll.Meals.GetAllAsyncBase(), "Id", "Name", cartMeal.MealId);
            ViewData["PizzaFinalId"] = new SelectList(await _bll.PizzaFinals.GetAllAsyncBase(), "Id", "Changes", cartMeal.PizzaFinalId);
            return View(cartMeal);
        }

        // GET: CartMeal/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartMeal = await _bll.CartMeals.FirstOrDefaultAsync(id.Value);

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
            await _bll.CartMeals.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
