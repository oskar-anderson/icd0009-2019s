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
    public class PizzaController : Controller
    {
        private readonly IAppBLL _bll;

        public PizzaController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Pizza
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var pizzas = await _bll.Pizzas.GetAllForViewAsync();
            return View(pizzas);
        }

        // GET: Pizza/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizza = await _bll.Pizzas.FirstOrDefaultViewAsync(id.Value);
            
            if (pizza == null)
            {
                return NotFound();
            }

            return View(pizza);
        }

        // GET: Pizza/Create
        public async Task<IActionResult> Create()
        {
            ViewData["PizzaTemplateId"] = new SelectList(await _bll.PizzaTemplates.GetAllAsyncBase(), "Id", "Name");
            return View();
        }

        // POST: Pizza/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BLL.App.DTO.Pizza pizza)
        {
            if (ModelState.IsValid)
            {
                _bll.Pizzas.Add(pizza);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PizzaTemplateId"] = new SelectList(await _bll.PizzaTemplates.GetAllAsyncBase(), "Id", "Name", pizza.PizzaTemplateId);
            return View(pizza);
        }

        // GET: Pizza/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizza = await _bll.Pizzas.FirstOrDefaultViewAsync(id.Value);
            
            if (pizza == null)
            {
                return NotFound();
            }
            ViewData["PizzaTemplateId"] = new SelectList(await _bll.PizzaTemplates.GetAllAsyncBase(), "Id", "Name", pizza.PizzaTemplateId);
            return View(pizza);
        }

        // POST: Pizza/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, BLL.App.DTO.Pizza pizza)
        {
            if (id != pizza.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _bll.Pizzas.UpdateAsync(pizza);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PizzaTemplateId"] = new SelectList(await _bll.PizzaTemplates.GetAllAsyncBase(), "Id", "Name", pizza.PizzaTemplateId);
            return View(pizza);
        }

        // GET: Pizza/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizza = await _bll.Pizzas.FirstOrDefaultViewAsync(id.Value);

            if (pizza == null)
            {
                return NotFound();
            }

            return View(pizza);
        }

        // POST: Pizza/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.Pizzas.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
