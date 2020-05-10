using System;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Extensions;

namespace WebApp.Controllers
{
    public class ComponentPriceController : Controller
    {
        private readonly IAppBLL _bll;

        public ComponentPriceController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: ComponentPrice
        public async Task<IActionResult> Index()
        {
            var componentPrices = await _bll.ComponentPrices.GetAllAsyncBase();
            return View(componentPrices);
        }

        // GET: ComponentPrice/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var componentPrice = await _bll.ComponentPrices.FirstOrDefaultAsync(id.Value);

            if (componentPrice == null)
            {
                return NotFound();
            }

            return View(componentPrice);
        }

        // GET: ComponentPrice/Create
        public async Task<IActionResult> Create()
        {
            ViewData["ComponentId"] = new SelectList(await _bll.Components.GetAllAsyncBase(), "Id", "Name");
            ViewData["RestaurantId"] = new SelectList(await _bll.Restaurants.GetAllAsyncBase(), "Id", "Location");
            return View();
        }

        // POST: ComponentPrice/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BLL.App.DTO.ComponentPrice componentPrice)
        {
            if (ModelState.IsValid)
            {
                _bll.ComponentPrices.Add(componentPrice);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ComponentId"] = new SelectList(await _bll.Components.GetAllAsyncBase(), "Id", "Name", componentPrice.ComponentId);
            ViewData["RestaurantId"] = new SelectList(await _bll.Restaurants.GetAllAsyncBase(), "Id", "Location", componentPrice.RestaurantId);
            return View(componentPrice);
        }

        // GET: ComponentPrice/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var componentPrice = await _bll.ComponentPrices.FirstOrDefaultAsync(id.Value);

            if (componentPrice == null)
            {
                return NotFound();
            }
            ViewData["ComponentId"] = new SelectList(await _bll.Components.GetAllAsyncBase(), "Id", "Name", componentPrice.ComponentId);
            ViewData["RestaurantId"] = new SelectList(await _bll.Restaurants.GetAllAsyncBase(), "Id", "Location", componentPrice.RestaurantId);
            return View(componentPrice);
        }

        // POST: ComponentPrice/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, BLL.App.DTO.ComponentPrice componentPrice)
        {
            if (id != componentPrice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _bll.ComponentPrices.UpdateAsync(componentPrice);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ComponentId"] = new SelectList(await _bll.Components.GetAllAsyncBase(), "Id", "Name", componentPrice.ComponentId);
            ViewData["RestaurantId"] = new SelectList(await _bll.Restaurants.GetAllAsyncBase(), "Id", "Location", componentPrice.RestaurantId);
            return View(componentPrice);
        }

        // GET: ComponentPrice/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var componentPrice = await _bll.ComponentPrices.FirstOrDefaultAsync(id.Value);

            if (componentPrice == null)
            {
                return NotFound();
            }

            return View(componentPrice);
        }

        // POST: ComponentPrice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.ComponentPrices.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
