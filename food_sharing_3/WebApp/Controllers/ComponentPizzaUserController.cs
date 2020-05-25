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
    public class ComponentPizzaUserController : Controller
    {
        private readonly IAppBLL _bll;

        public ComponentPizzaUserController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: PizzaComponent
        public async Task<IActionResult> Index()
        {
            var componentPizzaUsers = await _bll.ComponentPizzaUsers.GetAllForViewAsync();
            return View(componentPizzaUsers);
        }

        // GET: PizzaComponent/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var componentPizzaUser = await _bll.ComponentPizzaUsers.FirstOrDefaultViewAsync(id.Value);
            
            if (componentPizzaUser == null)
            {
                return NotFound();
            }

            return View(componentPizzaUser);
        }

        // GET: PizzaComponent/Create
        public async Task<IActionResult> Create()
        {
            ViewData["ComponentId"] = new SelectList(await _bll.Components.GetAllAsyncBase(), "Id", "Name");
            ViewData["PizzaFinalId"] = new SelectList(await _bll.PizzaUsers.GetAllAsyncBase(), "Id", "Id");
            return View();
        }

        // POST: PizzaComponent/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BLL.App.DTO.ComponentPizzaUser componentPizzaUser)
        {
            if (ModelState.IsValid)
            {
                _bll.ComponentPizzaUsers.Add(componentPizzaUser);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ComponentId"] = new SelectList(await _bll.Components.GetAllAsyncBase(), "Id", "Name", componentPizzaUser.ComponentId);
            ViewData["PizzaFinalId"] = new SelectList(await _bll.PizzaUsers.GetAllAsyncBase(), "Id", "Id", componentPizzaUser.PizzaUserId);
            return View(componentPizzaUser);
        }

        // GET: PizzaComponent/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var componentPizzaUser = await _bll.ComponentPizzaUsers.FirstOrDefaultViewAsync(id.Value);
            
            if (componentPizzaUser == null)
            {
                return NotFound();
            }
            ViewData["ComponentId"] = new SelectList(await _bll.Components.GetAllAsyncBase(), "Id", "Name", componentPizzaUser.ComponentId);
            ViewData["PizzaFinalId"] = new SelectList(await _bll.PizzaUsers.GetAllAsyncBase(), "Id", "Id", componentPizzaUser.PizzaUserId);
            return View(componentPizzaUser);
        }

        // POST: PizzaComponent/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, BLL.App.DTO.ComponentPizzaUser componentPizzaUser)
        {
            if (id != componentPizzaUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _bll.ComponentPizzaUsers.UpdateAsync(componentPizzaUser);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            ViewData["ComponentId"] = new SelectList(await _bll.Components.GetAllAsyncBase(), "Id", "Name", componentPizzaUser.ComponentId);
            ViewData["PizzaFinalId"] = new SelectList(await _bll.PizzaUsers.GetAllAsyncBase(), "Id", "Id", componentPizzaUser.PizzaUserId);
            return View(componentPizzaUser);
        }

        // GET: PizzaComponent/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var componentPizzaUser = await _bll.ComponentPizzaUsers.FirstOrDefaultViewAsync(id.Value);
            
            if (componentPizzaUser == null)
            {
                return NotFound();
            }

            return View(componentPizzaUser);
        }

        // POST: PizzaComponent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.ComponentPizzaUsers.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
    }
}
