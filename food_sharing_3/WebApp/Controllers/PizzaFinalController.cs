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
    [Authorize]
    public class PizzaFinalController : Controller
    {
        private readonly IAppBLL _bll;

        public PizzaFinalController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: PizzaFinal
        public async Task<IActionResult> Index()
        {
            var pizzaFinals = await _bll.PizzaFinals.GetAllAsyncBase();
            return View(pizzaFinals);
        }

        // GET: PizzaFinal/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzaFinal = await _bll.PizzaFinals.FirstOrDefaultAsync(id.Value);
            
            if (pizzaFinal == null)
            {
                return NotFound();
            }

            return View(pizzaFinal);
        }

        // GET: PizzaFinal/Create
        public async Task<IActionResult> Create()
        {
            ViewData["PizzaId"] = new SelectList(await _bll.PizzaFinals.GetAllAsyncBase(), "Id", "Name");
            return View();
        }

        // POST: PizzaFinal/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BLL.App.DTO.PizzaFinal pizzaFinal)
        {
            if (ModelState.IsValid)
            {
                _bll.PizzaFinals.Add(pizzaFinal);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PizzaId"] = new SelectList(await _bll.PizzaFinals.GetAllAsyncBase(), "Id", "Name", pizzaFinal.PizzaId);
            return View(pizzaFinal);
        }

        // GET: PizzaFinal/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzaFinal = await _bll.PizzaFinals.FirstOrDefaultAsync(id.Value);

            if (pizzaFinal == null)
            {
                return NotFound();
            }
            ViewData["PizzaId"] = new SelectList(await _bll.PizzaFinals.GetAllAsyncBase(), "Id", "Name", pizzaFinal.PizzaId);
            return View(pizzaFinal);
        }

        // POST: PizzaFinal/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, BLL.App.DTO.PizzaFinal pizzaFinal)
        {
            if (id != pizzaFinal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                
                await _bll.PizzaFinals.UpdateAsync(pizzaFinal);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PizzaId"] = new SelectList(await _bll.PizzaFinals.GetAllAsyncBase(), "Id", "Name", pizzaFinal.PizzaId);
            return View(pizzaFinal);
        }

        // GET: PizzaFinal/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzaFinal = await _bll.PizzaFinals.FirstOrDefaultAsync(id.Value);
            
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
            await _bll.PizzaFinals.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
