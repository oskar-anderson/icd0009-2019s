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
    public class PizzaTemplateController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public PizzaTemplateController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: PizzaTemplate
        public async Task<IActionResult> Index()
        {
            var pizzaTemplate = await _uow.PizzaTemplates.AllAsync(User.UserGuidId());
            return View(pizzaTemplate);
        }

        // GET: PizzaTemplate/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzaTemplate = await _uow.PizzaTemplates.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            
            if (pizzaTemplate == null)
            {
                return NotFound();
            }

            return View(pizzaTemplate);
        }

        // GET: PizzaTemplate/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CategoryId"] = new SelectList(await _uow.Categorys.AllAsync(User.UserGuidId()), "Id", "Name");
            return View();
        }

        // POST: PizzaTemplate/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PizzaTemplate pizzaTemplate)
        {
            if (ModelState.IsValid)
            {
                _uow.PizzaTemplates.Add(pizzaTemplate);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(await _uow.Categorys.AllAsync(User.UserGuidId()), "Id", "Name", pizzaTemplate.CategoryId);
            return View(pizzaTemplate);
        }

        // GET: PizzaTemplate/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzaTemplate = await _uow.PizzaTemplates.FirstOrDefaultAsync(id.Value, User.UserGuidId());

            if (pizzaTemplate == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(await _uow.Categorys.AllAsync(User.UserGuidId()), "Id", "Name", pizzaTemplate.CategoryId);
            return View(pizzaTemplate);
        }

        // POST: PizzaTemplate/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CategoryId,Name,Picture,Modifications,Extras,Description,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] PizzaTemplate pizzaTemplate)
        {
            if (id != pizzaTemplate.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.PizzaTemplates.Update(pizzaTemplate);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(await _uow.Categorys.AllAsync(User.UserGuidId()), "Id", "Name", pizzaTemplate.CategoryId);
            return View(pizzaTemplate);
        }

        // GET: PizzaTemplate/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzaTemplate = await _uow.PizzaTemplates.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            
            if (pizzaTemplate == null)
            {
                return NotFound();
            }

            return View(pizzaTemplate);
        }

        // POST: PizzaTemplate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.PizzaTemplates.DeleteAsync(id, User.UserGuidId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
