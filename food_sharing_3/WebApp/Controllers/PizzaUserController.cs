using System;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using DAL.App.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    [Authorize]
    public class PizzaUserController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IAppBLL _bll;

        public PizzaUserController(AppDbContext context, IAppBLL bll)
        {
            _context = context;
            _bll = bll;
        }

        // GET: PizzaUser
        public async Task<IActionResult> Index()
        {
            var pizzaUsers = await _bll.PizzaUsers.GetAllForViewAsync(User.UserId());
            return View(pizzaUsers);
        }

        // GET: PizzaUser/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzaUser = await _bll.PizzaUsers.FirstOrDefaultViewAsync(id.Value);
            
            if (pizzaUser == null)
            {
                return NotFound();
            }

            return View(pizzaUser);
        }

        // GET: PizzaUser/Create
        public async Task<IActionResult> Create()
        {
            ViewData["PizzaId"] = new SelectList(await _bll.Pizzas.GetAllAsyncBase(), "Id", "Name");
            ViewData["AppUserId"] = new SelectList(_context.Users.Where(user => user.Id == User.UserId()), "Id", "FirstName");
            return View();
        }

        // POST: PizzaUser/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BLL.App.DTO.PizzaUser pizzaUser)
        {
            if (ModelState.IsValid)
            {
                _bll.PizzaUsers.Add(pizzaUser);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PizzaId"] = new SelectList(await _bll.Pizzas.GetAllAsyncBase(), "Id", "Name", pizzaUser.PizzaId);
            ViewData["AppUserId"] = new SelectList(_context.Users.Where(user => user.Id == User.UserId()), "Id", "FirstName");
            return View(pizzaUser);
        }

        // GET: PizzaUser/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzaUser = await _bll.PizzaUsers.FirstOrDefaultViewAsync(id.Value);

            if (pizzaUser == null)
            {
                return NotFound();
            }
            ViewData["PizzaId"] = new SelectList(await _bll.Pizzas.GetAllAsyncBase(), "Id", "Name", pizzaUser.PizzaId);
            ViewData["AppUserId"] = new SelectList(_context.Users.Where(user => user.Id == User.UserId()), "Id", "FirstName");
            return View(pizzaUser);
        }

        // POST: PizzaUser/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, BLL.App.DTO.PizzaUser pizzaUser)
        {
            if (id != pizzaUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                
                await _bll.PizzaUsers.UpdateAsync(pizzaUser);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PizzaId"] = new SelectList(await _bll.Pizzas.GetAllAsyncBase(), "Id", "Name", pizzaUser.PizzaId);
            ViewData["AppUserId"] = new SelectList(_context.Users.Where(user => user.Id == User.UserId()), "Id", "FirstName");
            return View(pizzaUser);
        }

        // GET: PizzaUser/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzaUser = await _bll.PizzaUsers.FirstOrDefaultViewAsync(id.Value);
            
            if (pizzaUser == null)
            {
                return NotFound();
            }

            return View(pizzaUser);
        }

        // POST: PizzaUser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.PizzaUsers.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
