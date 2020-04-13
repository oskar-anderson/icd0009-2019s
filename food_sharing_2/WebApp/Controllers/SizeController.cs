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
    public class SizeController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public SizeController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Size
        public async Task<IActionResult> Index()
        {
            var Sizes = await _uow.Sizes.AllAsync(User.UserGuidId());
            return View(Sizes);
        }

        // GET: Size/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var size = await _uow.Sizes.FirstOrDefaultAsync(id.Value, User.UserGuidId());

            if (size == null)
            {
                return NotFound();
            }

            return View(size);
        }

        // GET: Size/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Size/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Size size)
        {
            if (ModelState.IsValid)
            {
                _uow.Sizes.Add(size);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(size);
        }

        // GET: Size/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var size = await _uow.Sizes.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            
            if (size == null)
            {
                return NotFound();
            }
            return View(size);
        }

        // POST: Size/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Size size)
        {
            if (id != size.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.Sizes.Update(size);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(size);
        }

        // GET: Size/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var size = await _uow.Sizes.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            
            if (size == null)
            {
                return NotFound();
            }

            return View(size);
        }

        // POST: Size/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.Sizes.DeleteAsync(id, User.UserGuidId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
