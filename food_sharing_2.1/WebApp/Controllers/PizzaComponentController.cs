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
    public class PizzaComponentController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public PizzaComponentController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: PizzaComponent
        public async Task<IActionResult> Index()
        {
            var pizzaComponents = await _uow.PizzaComponents.AllAsync(User.UserGuidId());
            return View(pizzaComponents);
        }

        // GET: PizzaComponent/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzaComponent = await _uow.PizzaComponents.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            
            if (pizzaComponent == null)
            {
                return NotFound();
            }

            return View(pizzaComponent);
        }

        // GET: PizzaComponent/Create
        public async Task<IActionResult> Create()
        {
            ViewData["ComponentId"] = new SelectList(await _uow.Components.AllAsync(User.UserGuidId()), "Id", "Name");
            ViewData["PizzaFinalId"] = new SelectList(await _uow.PizzaFinals.AllAsync(User.UserGuidId()), "Id", "Changes");
            ViewData["PizzaTemplateId"] = new SelectList(await _uow.PizzaTemplates.AllAsync(User.UserGuidId()), "Id", "Name");
            return View();
        }

        // POST: PizzaComponent/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ComponentId,PizzaFinalId,PizzaTemplateId,Amount,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] PizzaComponent pizzaComponent)
        {
            if (ModelState.IsValid)
            {
                _uow.PizzaComponents.Add(pizzaComponent);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ComponentId"] = new SelectList(await _uow.Components.AllAsync(User.UserGuidId()), "Id", "Name", pizzaComponent.ComponentId);
            ViewData["PizzaFinalId"] = new SelectList(await _uow.PizzaFinals.AllAsync(User.UserGuidId()), "Id", "Changes", pizzaComponent.PizzaFinalId);
            ViewData["PizzaTemplateId"] = new SelectList(await _uow.PizzaTemplates.AllAsync(User.UserGuidId()), "Id", "Name", pizzaComponent.PizzaTemplateId);
            return View(pizzaComponent);
        }

        // GET: PizzaComponent/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzaComponent = await _uow.PizzaComponents.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            
            if (pizzaComponent == null)
            {
                return NotFound();
            }
            ViewData["ComponentId"] = new SelectList(await _uow.Components.AllAsync(User.UserGuidId()), "Id", "Name", pizzaComponent.ComponentId);
            ViewData["PizzaFinalId"] = new SelectList(await _uow.PizzaFinals.AllAsync(User.UserGuidId()), "Id", "Changes", pizzaComponent.PizzaFinalId);
            ViewData["PizzaTemplateId"] = new SelectList(await _uow.PizzaTemplates.AllAsync(User.UserGuidId()), "Id", "Name", pizzaComponent.PizzaTemplateId);
            return View(pizzaComponent);
        }

        // POST: PizzaComponent/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ComponentId,PizzaFinalId,PizzaTemplateId,Amount,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] PizzaComponent pizzaComponent)
        {
            if (id != pizzaComponent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.PizzaComponents.Update(pizzaComponent);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            ViewData["ComponentId"] = new SelectList(await _uow.Components.AllAsync(User.UserGuidId()), "Id", "Name", pizzaComponent.ComponentId);
            ViewData["PizzaFinalId"] = new SelectList(await _uow.PizzaFinals.AllAsync(User.UserGuidId()), "Id", "Changes", pizzaComponent.PizzaFinalId);
            ViewData["PizzaTemplateId"] = new SelectList(await _uow.PizzaTemplates.AllAsync(User.UserGuidId()), "Id", "Name", pizzaComponent.PizzaTemplateId);
            return View(pizzaComponent);
        }

        // GET: PizzaComponent/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzaComponent = await _uow.PizzaComponents.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            
            if (pizzaComponent == null)
            {
                return NotFound();
            }

            return View(pizzaComponent);
        }

        // POST: PizzaComponent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.PizzaComponents.DeleteAsync(id, User.UserGuidId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
    }
}
