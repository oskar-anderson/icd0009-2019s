using System;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class PizzaTemplateController : Controller
    {
        private readonly IAppBLL _bll;

        public PizzaTemplateController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: PizzaTemplate
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var pizzaTemplates = await _bll.PizzaTemplates.GetAllForViewAsync();
            // var pizzaTemplates = await _bll.PizzaTemplates.GetAllAsyncBase();
            return View(pizzaTemplates);
        }

        // GET: PizzaTemplate/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var pizzaTemplate = await _bll.PizzaTemplates.FirstOrDefaultAsync(id.Value);
            var pizzaTemplate = await _bll.PizzaTemplates.FirstOrDefaultViewAsync(id.Value);
            
            if (pizzaTemplate == null)
            {
                return NotFound();
            }

            return View(pizzaTemplate);
        }

        // GET: PizzaTemplate/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CategoryId"] = new SelectList(
                await _bll.Categorys.GetAllAsyncBase(), 
                "Id", 
                "Name");
            return View();
        }

        // POST: PizzaTemplate/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BLL.App.DTO.PizzaTemplate pizzaTemplate)
        {
            if (ModelState.IsValid)
            {
                _bll.PizzaTemplates.Add(pizzaTemplate);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(
                await _bll.Categorys.GetAllAsyncBase(), 
                "Id", 
                "Name", 
                pizzaTemplate.CategoryId);
            return View(pizzaTemplate);
        }

        // GET: PizzaTemplate/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzaTemplate = await _bll.PizzaTemplates.FirstOrDefaultAsync(id.Value, User.UserId());

            if (pizzaTemplate == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(
                await _bll.Categorys.GetAllAsyncBase(), 
                "Id", 
                "Name", 
                pizzaTemplate.CategoryId);
            return View(pizzaTemplate);
        }

        // POST: PizzaTemplate/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, BLL.App.DTO.PizzaTemplate pizzaTemplate)
        {
            if (id != pizzaTemplate.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _bll.PizzaTemplates.UpdateAsync(pizzaTemplate);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(
                await _bll.Categorys.GetAllAsyncBase(),
                "Id", 
                "Name",
                pizzaTemplate.CategoryId);
            return View(pizzaTemplate);
        }

        // GET: PizzaTemplate/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var pizzaTemplate = await _bll.PizzaTemplates.FirstOrDefaultAsync(id.Value, User.UserId());
            var pizzaTemplate = await _bll.PizzaTemplates.FirstOrDefaultViewAsync(id.Value, User.UserId());
            
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
            await _bll.PizzaTemplates.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
