using System;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    [Authorize(Roles = "admin")]
    public class PizzaComponentController : Controller
    {
        private readonly IAppBLL _bll;

        public PizzaComponentController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: PizzaComponent
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var pizzaComponents = await _bll.PizzaComponents.GetAllAsyncBase();
            return View(pizzaComponents);
        }

        // GET: PizzaComponent/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzaComponent = await _bll.PizzaComponents.FirstOrDefaultAsync(id.Value);
            
            if (pizzaComponent == null)
            {
                return NotFound();
            }

            return View(pizzaComponent);
        }

        // GET: PizzaComponent/Create
        public async Task<IActionResult> Create()
        {
            ViewData["ComponentId"] = new SelectList(await _bll.Components.GetAllAsyncBase(), "Id", "Name");
            ViewData["PizzaFinalId"] = new SelectList(await _bll.PizzaFinals.GetAllAsyncBase(), "Id", "Changes");
            ViewData["PizzaTemplateId"] = new SelectList(await _bll.PizzaTemplates.GetAllAsyncBase(), "Id", "Name");
            return View();
        }

        // POST: PizzaComponent/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BLL.App.DTO.PizzaComponent pizzaComponent)
        {
            if (ModelState.IsValid)
            {
                _bll.PizzaComponents.Add(pizzaComponent);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ComponentId"] = new SelectList(await _bll.Components.GetAllAsyncBase(), "Id", "Name", pizzaComponent.ComponentId);
            ViewData["PizzaFinalId"] = new SelectList(await _bll.PizzaFinals.GetAllAsyncBase(), "Id", "Changes", pizzaComponent.PizzaFinalId);
            ViewData["PizzaTemplateId"] = new SelectList(await _bll.PizzaTemplates.GetAllAsyncBase(), "Id", "Name", pizzaComponent.PizzaTemplateId);
            return View(pizzaComponent);
        }

        // GET: PizzaComponent/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzaComponent = await _bll.PizzaComponents.FirstOrDefaultAsync(id.Value);
            
            if (pizzaComponent == null)
            {
                return NotFound();
            }
            ViewData["ComponentId"] = new SelectList(await _bll.Components.GetAllAsyncBase(), "Id", "Name", pizzaComponent.ComponentId);
            ViewData["PizzaFinalId"] = new SelectList(await _bll.PizzaFinals.GetAllAsyncBase(), "Id", "Changes", pizzaComponent.PizzaFinalId);
            ViewData["PizzaTemplateId"] = new SelectList(await _bll.PizzaTemplates.GetAllAsyncBase(), "Id", "Name", pizzaComponent.PizzaTemplateId);
            return View(pizzaComponent);
        }

        // POST: PizzaComponent/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, BLL.App.DTO.PizzaComponent pizzaComponent)
        {
            if (id != pizzaComponent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _bll.PizzaComponents.UpdateAsync(pizzaComponent);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            ViewData["ComponentId"] = new SelectList(await _bll.Components.GetAllAsyncBase(), "Id", "Name", pizzaComponent.ComponentId);
            ViewData["PizzaFinalId"] = new SelectList(await _bll.PizzaFinals.GetAllAsyncBase(), "Id", "Changes", pizzaComponent.PizzaFinalId);
            ViewData["PizzaTemplateId"] = new SelectList(await _bll.PizzaTemplates.GetAllAsyncBase(), "Id", "Name", pizzaComponent.PizzaTemplateId);
            return View(pizzaComponent);
        }

        // GET: PizzaComponent/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzaComponent = await _bll.PizzaComponents.FirstOrDefaultAsync(id.Value);
            
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
            await _bll.PizzaComponents.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
    }
}
