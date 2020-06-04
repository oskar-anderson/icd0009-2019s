using System;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using DAL.App.EF;
using Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IAppBLL _bll;
        private readonly Object[] _paymentMethods = {
            new { Name = "Swedbank"},
            new { Name = "SEB"},
            new { Name = "LHV pank"},
            new { Name = "Pocopay"},
            new { Name = "Nordea"},
            new { Name = "Coop"},
            new { Name = "Danske"},
        };
        
        public CartController(AppDbContext context, IAppBLL bll)
        {
            _context = context;
            _bll = bll;
        }

        // GET: Cart
        public async Task<IActionResult> Index()
        {
            var carts = await _bll.Carts.GetAllForViewAsync(User.UserId());
            return View(carts);
        }

        // GET: Cart/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _bll.Carts.FirstOrDefaultViewAsync(id.Value, User.UserId());

            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // GET: Cart/Create
        public async Task<IActionResult> Create()
        {
            ViewData["PaymentMethod"] = new SelectList(_paymentMethods, "Name", "Name");
            ViewData["RestaurantId"] = new SelectList(await _bll.Restaurants.GetAllAsyncBase(User.UserId()), "Id", "Location");
            ViewData["UserLocationId"] = new SelectList(await _bll.UserLocations.GetAllAsyncBase(User.UserId()), "Id", "FullName");
            ViewData["AppUserId"] = new SelectList(_context.Users.Where(user => user.Id == User.UserId()), "Id", "FirstName");
            return View();
        }

        // POST: Cart/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BLL.App.DTO.Cart cart)
        {
            if (ModelState.IsValid)
            {
                _bll.Carts.Add(cart);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PaymentMethod"] = new SelectList(_paymentMethods, "Name", "Name");
            ViewData["RestaurantId"] = new SelectList(await _bll.Restaurants.GetAllAsyncBase(User.UserId()), "Id", "Location", cart.RestaurantId);
            ViewData["UserLocationId"] = new SelectList(await _bll.UserLocations.GetAllAsyncBase(User.UserId()), "Id", "FullName", cart.UserLocationId);
            ViewData["AppUserId"] = new SelectList(_context.Users.Where(user => user.Id == User.UserId()), "Id", "FirstName");
            return View(cart);
        }

        // GET: Cart/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _bll.Carts.FirstOrDefaultViewAsync(id.Value, User.UserId());

            if (cart == null)
            {
                return NotFound();
            }
            ViewData["PaymentMethod"] = new SelectList(_paymentMethods, "Name", "Name");
            ViewData["RestaurantId"] = new SelectList(await _bll.Restaurants.GetAllAsyncBase(User.UserId()), "Id", "Location", cart.RestaurantId);
            ViewData["UserLocationId"] = new SelectList(await _bll.UserLocations.GetAllAsyncBase(User.UserId()), "Id", "FullName", cart.UserLocationId);
            ViewData["AppUserId"] = new SelectList(_context.Users.Where(user => user.Id == User.UserId()), "Id", "FirstName");
            return View(cart);
        }

        // POST: Cart/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, BLL.App.DTO.Cart cart)
        {
            if (id != cart.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _bll.Carts.UpdateAsync(cart);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PaymentMethod"] = new SelectList(_paymentMethods, "Name", "Name");
            ViewData["RestaurantId"] = new SelectList(await _bll.Restaurants.GetAllAsyncBase(User.UserId()), "Id", "Location", cart.RestaurantId);
            ViewData["UserLocationId"] = new SelectList(await _bll.UserLocations.GetAllAsyncBase(User.UserId()), "Id", "FullName", cart.UserLocationId);
            ViewData["AppUserId"] = new SelectList(_context.Users.Where(user => user.Id == User.UserId()), "Id", "FirstName");
            return View(cart);
        }

        // GET: Cart/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _bll.Carts.FirstOrDefaultViewAsync(id.Value, User.UserId());

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
            await _bll.Carts.RemoveAsync(id, User.UserId());
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
