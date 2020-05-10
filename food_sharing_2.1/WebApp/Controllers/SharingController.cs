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
    public class SharingController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public SharingController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Sharing
        public async Task<IActionResult> Index()
        {
            var sharings = await _uow.Sharings.AllAsync(User.UserGuidId());
            return View(sharings);
        }

        // GET: Sharing/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sharing = await _uow.Sharings.FirstOrDefaultAsync(id.Value, User.UserGuidId());
                
            if (sharing == null)
            {
                return NotFound();
            }

            return View(sharing);
        }

        // GET: Sharing/Create
        public IActionResult Create()
        {
            
            return View();
        }

        // POST: Sharing/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Sharing sharing)
        {
            sharing.AppUserId = User.UserGuidId();
            if (ModelState.IsValid)
            {
                _uow.Sharings.Add(sharing);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(sharing);
        }

        // GET: Sharing/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sharing = await _uow.Sharings.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            
            if (sharing == null)
            {
                return NotFound();
            }
            
            return View(sharing);
        }

        // POST: Sharing/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AppUserId,Name,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Sharing sharing)
        {
            sharing.AppUserId = User.UserGuidId();
            
            if (id != sharing.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.Sharings.Update(sharing);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(sharing);
        }

        // GET: Sharing/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sharing = await _uow.Sharings
                .FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (sharing == null)
            {
                return NotFound();
            }

            return View(sharing);
        }

        // POST: Sharing/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.Sharings.DeleteAsync(id, User.UserGuidId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
