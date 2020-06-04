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
    public class ComponentPizzaTemplateController : Controller
    {
        private readonly IAppBLL _bll;

        public ComponentPizzaTemplateController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: PizzaComponent
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var componentPizzaTemplates = await _bll.ComponentPizzaTemplates.GetAllForViewAsync();
            return View(componentPizzaTemplates);
        }

        // GET: PizzaComponent/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var componentPizzaTemplates = await _bll.ComponentPizzaTemplates.FirstOrDefaultViewAsync(id.Value);
            
            if (componentPizzaTemplates == null)
            {
                return NotFound();
            }

            return View(componentPizzaTemplates);
        }

        // GET: PizzaComponent/Create
        public async Task<IActionResult> Create()
        {
            ViewData["ComponentId"] = new SelectList(await _bll.Components.GetAllAsyncBase(), "Id", "Name");
            ViewData["PizzaTemplateId"] = new SelectList(await _bll.PizzaTemplates.GetAllAsyncBase(), "Id", "Name");
            return View();
        }

        // POST: PizzaComponent/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BLL.App.DTO.ComponentPizzaTemplate componentPizzaTemplates)
        {
            if (ModelState.IsValid)
            {
                _bll.ComponentPizzaTemplates.Add(componentPizzaTemplates);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ComponentId"] = new SelectList(await _bll.Components.GetAllAsyncBase(), "Id", "Name", componentPizzaTemplates.ComponentId);
            ViewData["PizzaTemplateId"] = new SelectList(await _bll.PizzaTemplates.GetAllAsyncBase(), "Id", "Name", componentPizzaTemplates.PizzaTemplateId);
            return View(componentPizzaTemplates);
        }

        // GET: PizzaComponent/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzaComponent = await _bll.ComponentPizzaTemplates.FirstOrDefaultViewAsync(id.Value);
            
            if (pizzaComponent == null)
            {
                return NotFound();
            }
            ViewData["ComponentId"] = new SelectList(await _bll.Components.GetAllAsyncBase(), "Id", "Name", pizzaComponent.ComponentId);
            ViewData["PizzaTemplateId"] = new SelectList(await _bll.PizzaTemplates.GetAllAsyncBase(), "Id", "Name", pizzaComponent.PizzaTemplateId);
            return View(pizzaComponent);
        }

        // POST: PizzaComponent/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, BLL.App.DTO.ComponentPizzaTemplate componentPizzaTemplates)
        {
            if (id != componentPizzaTemplates.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _bll.ComponentPizzaTemplates.UpdateAsync(componentPizzaTemplates);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            ViewData["ComponentId"] = new SelectList(await _bll.Components.GetAllAsyncBase(), "Id", "Name", componentPizzaTemplates.ComponentId);
            ViewData["PizzaTemplateId"] = new SelectList(await _bll.PizzaTemplates.GetAllAsyncBase(), "Id", "Name", componentPizzaTemplates.PizzaTemplateId);
            return View(componentPizzaTemplates);
        }

        // GET: PizzaComponent/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var componentPizzaTemplates = await _bll.ComponentPizzaTemplates.FirstOrDefaultViewAsync(id.Value);
            
            if (componentPizzaTemplates == null)
            {
                return NotFound();
            }

            return View(componentPizzaTemplates);
        }

        // POST: PizzaComponent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.ComponentPizzaTemplates.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
    }
}
