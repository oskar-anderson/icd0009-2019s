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
    public class PizzaFinalController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public PizzaFinalController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: PizzaFinal
        public async Task<IActionResult> Index()
        {
            var pizzaFinal = await _uow.PizzaFinals.AllAsync(User.UserGuidId());
            return View(pizzaFinal);
        }

        // GET: PizzaFinal/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzaFinal = await _uow.PizzaFinals.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            
            if (pizzaFinal == null)
            {
                return NotFound();
            }

            return View(pizzaFinal);
        }

        // GET: PizzaFinal/Create
        public async Task<IActionResult> Create()
        {
            ViewData["PizzaId"] = new SelectList(await _uow.PizzaFinals.AllAsync(User.UserGuidId()), "Id", "Name");
            return View();
        }

        // POST: PizzaFinal/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PizzaId,Tax,Gross,Changes,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] PizzaFinal pizzaFinal)
        {
            if (ModelState.IsValid)
            {
                _uow.PizzaFinals.Add(pizzaFinal);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PizzaId"] = new SelectList(await _uow.PizzaFinals.AllAsync(User.UserGuidId()), "Id", "Name", pizzaFinal.PizzaId);
            return View(pizzaFinal);
        }

        // GET: PizzaFinal/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzaFinal = await _uow.PizzaFinals.FirstOrDefaultAsync(id.Value, User.UserGuidId());

            if (pizzaFinal == null)
            {
                return NotFound();
            }
            ViewData["PizzaId"] = new SelectList(await _uow.PizzaFinals.AllAsync(User.UserGuidId()), "Id", "Name", pizzaFinal.PizzaId);
            return View(pizzaFinal);
        }

        // POST: PizzaFinal/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PizzaId,Tax,Gross,Changes,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] PizzaFinal pizzaFinal)
        {
            if (id != pizzaFinal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                
                _uow.PizzaFinals.Update(pizzaFinal);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PizzaId"] = new SelectList(await _uow.PizzaFinals.AllAsync(User.UserGuidId()), "Id", "Name", pizzaFinal.PizzaId);
            return View(pizzaFinal);
        }

        // GET: PizzaFinal/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzaFinal = await _uow.PizzaFinals.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            
            if (pizzaFinal == null)
            {
                return NotFound();
            }

            return View(pizzaFinal);
        }

        // POST: PizzaFinal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.PizzaFinals.DeleteAsync(id, User.UserGuidId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
