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
    public class MealController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public MealController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Meal
        public async Task<IActionResult> Index()
        {
            var meal = await _uow.Meals.AllAsync(User.UserGuidId());
            return View(meal);
        }

        // GET: Meal/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meal = await _uow.Meals.FirstOrDefaultAsync(id.Value, User.UserGuidId());

            if (meal == null)
            {
                return NotFound();
            }

            return View(meal);
        }

        // GET: Meal/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CategoryId"] = new SelectList(await _uow.Categorys.AllAsync(User.UserGuidId()), "Id", "Name");
            return View();
        }

        // POST: Meal/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,Name,Picture,Description,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Meal meal)
        {
            if (ModelState.IsValid)
            {
                _uow.Meals.Add(meal);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(await _uow.Categorys.AllAsync(User.UserGuidId()), "Id", "Name", meal.CategoryId);
            return View(meal);
        }

        // GET: Meal/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meal = await _uow.Meals.FirstOrDefaultAsync(id.Value, User.UserGuidId());

            if (meal == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(await _uow.Categorys.AllAsync(User.UserGuidId()), "Id", "Name", meal.CategoryId);
            return View(meal);
        }

        // POST: Meal/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CategoryId,Name,Picture,Description,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Meal meal)
        {
            if (id != meal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.Meals.Update(meal);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(await _uow.Categorys.AllAsync(User.UserGuidId()), "Id", "Name", meal.CategoryId);
            return View(meal);
        }

        // GET: Meal/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meal = await _uow.Meals.FirstOrDefaultAsync(id.Value, User.UserGuidId());

            if (meal == null)
            {
                return NotFound();
            }

            return View(meal);
        }

        // POST: Meal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.Meals.DeleteAsync(id, User.UserGuidId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
