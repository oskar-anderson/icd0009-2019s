using System;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Areas.Admin.Controllers 
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class ComponentController : Controller
    {
        private readonly IAppBLL _bll;

        public ComponentController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Component
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var components = await _bll.Components.GetAllAsyncBase();
            return View(components);
        }

        // GET: Component/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var component = await _bll.Components.FirstOrDefaultAsync(id.Value);

            if (component == null)
            {
                return NotFound();
            }

            return View(component);
        }

        // GET: Component/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Component/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BLL.App.DTO.Component component)
        {
            if (ModelState.IsValid)
            {
                _bll.Components.Add(component);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(component);
        }

        // GET: Component/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var component = await _bll.Components.FirstOrDefaultAsync(id.Value);

            if (component == null)
            {
                return NotFound();
            }
            return View(component);
        }

        // POST: Component/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, BLL.App.DTO.Component component)
        {
            if (id != component.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _bll.Components.UpdateAsync(component);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(component);
        }

        // GET: Component/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var component = await _bll.Components.FirstOrDefaultAsync(id.Value);

            if (component == null)
            {
                return NotFound();
            }

            return View(component);
        }

        // POST: Component/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.Components.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
