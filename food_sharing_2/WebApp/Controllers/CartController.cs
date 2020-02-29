using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly AppDbContext _context;

        public CartController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Cart
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Carts.Include(c => c.AppUser).Include(c => c.HandoverType).Include(c => c.Restaurant).Include(c => c.UserLocation);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Cart/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Carts
                .Include(c => c.AppUser)
                .Include(c => c.HandoverType)
                .Include(c => c.Restaurant)
                .Include(c => c.UserLocation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // GET: Cart/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["HandoverTypeId"] = new SelectList(_context.HandoverTypes, "Id", "Id");
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Id");
            ViewData["UserLocationId"] = new SelectList(_context.UserLocations, "Id", "Id");
            return View();
        }

        // POST: Cart/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppUserId,HandoverTypeId,UserLocationId,RestaurantId,Total,ReadyBy,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] Cart cart)
        {
            cart.AppUserId = User.GetUserId();
            
            if (ModelState.IsValid)
            {
                _context.Add(cart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", cart.AppUserId);
            ViewData["HandoverTypeId"] = new SelectList(_context.HandoverTypes, "Id", "Id", cart.HandoverTypeId);
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Id", cart.RestaurantId);
            ViewData["UserLocationId"] = new SelectList(_context.UserLocations, "Id", "Id", cart.UserLocationId);
            return View(cart);
        }

        // GET: Cart/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Carts.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", cart.AppUserId);
            ViewData["HandoverTypeId"] = new SelectList(_context.HandoverTypes, "Id", "Id", cart.HandoverTypeId);
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Id", cart.RestaurantId);
            ViewData["UserLocationId"] = new SelectList(_context.UserLocations, "Id", "Id", cart.UserLocationId);
            return View(cart);
        }

        // POST: Cart/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("AppUserId,HandoverTypeId,UserLocationId,RestaurantId,Total,ReadyBy,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] Cart cart)
        {
            if (id != cart.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartExists(cart.Id))
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
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", cart.AppUserId);
            ViewData["HandoverTypeId"] = new SelectList(_context.HandoverTypes, "Id", "Id", cart.HandoverTypeId);
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Id", cart.RestaurantId);
            ViewData["UserLocationId"] = new SelectList(_context.UserLocations, "Id", "Id", cart.UserLocationId);
            return View(cart);
        }

        // GET: Cart/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Carts
                .Include(c => c.AppUser)
                .Include(c => c.HandoverType)
                .Include(c => c.Restaurant)
                .Include(c => c.UserLocation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // POST: Cart/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var cart = await _context.Carts.FindAsync(id);
            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartExists(string id)
        {
            return _context.Carts.Any(e => e.Id == id);
        }
    }
}
