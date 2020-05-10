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
    public class PizzaController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public PizzaController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Pizza
        public async Task<IActionResult> Index()
        {
            var pizzas = await _uow.Pizzas.AllAsync(User.UserGuidId());
            return View(pizzas);
        }

        // GET: Pizza/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizza = await _uow.Pizzas.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            
            if (pizza == null)
            {
                return NotFound();
            }

            return View(pizza);
        }

        // GET: Pizza/Create
        public async Task<IActionResult> Create()
        {
            ViewData["PizzaTemplateId"] = new SelectList(await _uow.PizzaTemplates.AllAsync(User.UserGuidId()), "Id", "Name");
            ViewData["SizeId"] = new SelectList(await _uow.Sizes.AllAsync(User.UserGuidId()), "Id", "Name");
            return View();
        }

        // POST: Pizza/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PizzaTemplateId,SizeId,Name,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Pizza pizza)
        {
            if (ModelState.IsValid)
            {
                _uow.Pizzas.Add(pizza);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PizzaTemplateId"] = new SelectList(await _uow.PizzaTemplates.AllAsync(User.UserGuidId()), "Id", "Name", pizza.PizzaTemplateId);
            ViewData["SizeId"] = new SelectList(await _uow.Sizes.AllAsync(User.UserGuidId()), "Id", "Name", pizza.SizeId);
            return View(pizza);
        }

        // GET: Pizza/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizza = await _uow.Pizzas.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            
            if (pizza == null)
            {
                return NotFound();
            }
            ViewData["PizzaTemplateId"] = new SelectList(await _uow.PizzaTemplates.AllAsync(User.UserGuidId()), "Id", "Name", pizza.PizzaTemplateId);
            ViewData["SizeId"] = new SelectList(await _uow.Sizes.AllAsync(User.UserGuidId()), "Id", "Name", pizza.SizeId);
            return View(pizza);
        }

        // POST: Pizza/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PizzaTemplateId,SizeId,Name,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Pizza pizza)
        {
            if (id != pizza.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.Pizzas.Update(pizza);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PizzaTemplateId"] = new SelectList(await _uow.PizzaTemplates.AllAsync(User.UserGuidId()), "Id", "Name", pizza.PizzaTemplateId);
            ViewData["SizeId"] = new SelectList(await _uow.Sizes.AllAsync(User.UserGuidId()), "Id", "Name", pizza.SizeId);
            return View(pizza);
        }

        // GET: Pizza/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizza = await _uow.Pizzas.FirstOrDefaultAsync(id.Value, User.UserGuidId());

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
            await _uow.Pizzas.DeleteAsync(id, User.UserGuidId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
