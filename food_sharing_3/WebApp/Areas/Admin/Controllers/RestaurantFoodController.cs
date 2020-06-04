using System;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class RestaurantFoodController : Controller
    {
        private readonly IAppBLL _bll;

        public RestaurantFoodController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: RestaurantFood
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var restaurantFoods = await _bll.RestaurantFoods.GetAllForViewAsync();
            return View(restaurantFoods);
        }

        // GET: RestaurantFood/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurantFood = await _bll.RestaurantFoods.FirstOrDefaultViewAsync(id.Value);

            if (restaurantFood == null)
            {
                return NotFound();
            }

            return View(restaurantFood);
        }

        // GET: RestaurantFood/Create
        public async Task<IActionResult> Create()
        {
            ViewData[ViewDataIdConstants.MealId] = new SelectList(await _bll.PizzaTemplates.GetAllAsyncBase(),
                ViewDataConstants.Id,
                ViewDataConstants.Name);
            ViewData[ViewDataIdConstants.PizzaId] = new SelectList(await _bll.Pizzas.GetAllAsyncBase(),
                ViewDataConstants.Id,
                ViewDataConstants.Name);
            ViewData[ViewDataIdConstants.RestaurantId] = new SelectList(await _bll.Restaurants.GetAllAsyncBase(),
                ViewDataConstants.Id,
                ViewDataConstants.Name);
            return View();
        }

        // POST: RestaurantFood/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.micˇrosoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BLL.App.DTO.RestaurantFood restaurantFood)
        {
            if (ModelState.IsValid)
            {
                restaurantFood.Id = Guid.NewGuid();
                _bll.RestaurantFoods.Add(restaurantFood);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData[ViewDataIdConstants.PizzaId] = new SelectList(await _bll.Pizzas.GetAllAsyncBase(),
                ViewDataConstants.Id,
                ViewDataConstants.Name,
                restaurantFood.PizzaId);
            ViewData[ViewDataIdConstants.RestaurantId] = new SelectList(await _bll.Restaurants.GetAllAsyncBase(), 
                ViewDataConstants.Id,
                ViewDataConstants.Name,
                restaurantFood.RestaurantId);
            return View(restaurantFood);
        }

        // GET: RestaurantFood/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurantFood = await _bll.RestaurantFoods.FirstOrDefaultViewAsync(id.Value);
            if (restaurantFood == null)
            {
                return NotFound();
            }
            ViewData[ViewDataIdConstants.PizzaId] = new SelectList(await _bll.Pizzas.GetAllAsyncBase(),
                ViewDataConstants.Id,
                ViewDataConstants.Name,
                restaurantFood.PizzaId);
            ViewData[ViewDataIdConstants.RestaurantId] = new SelectList(await _bll.Restaurants.GetAllAsyncBase(), 
                ViewDataConstants.Id,
                ViewDataConstants.Name,
                restaurantFood.RestaurantId);
            return View(restaurantFood);
        }

        // POST: RestaurantFood/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, BLL.App.DTO.RestaurantFood restaurantFood)
        {
            if (id != restaurantFood.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _bll.RestaurantFoods.UpdateAsync(restaurantFood);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData[ViewDataIdConstants.PizzaId] = new SelectList(await _bll.Pizzas.GetAllAsyncBase(), 
                ViewDataConstants.Id,
                ViewDataConstants.Name,
                restaurantFood.PizzaId);
            ViewData[ViewDataIdConstants.RestaurantId] = new SelectList(await _bll.Restaurants.GetAllAsyncBase(), 
                ViewDataConstants.Id,
                ViewDataConstants.Name,
                restaurantFood.RestaurantId);
            return View(restaurantFood);
        }

        // GET: RestaurantFood/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurantFood = await _bll.RestaurantFoods.FirstOrDefaultViewAsync(id.Value);
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
            await _bll.RestaurantFoods.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
