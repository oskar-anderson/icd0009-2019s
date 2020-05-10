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
    public class SharingItemController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public SharingItemController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: SharingItem
        public async Task<IActionResult> Index()
        {
            var sharingItems = await _uow.SharingItems.AllAsync(User.UserGuidId());
            return View(sharingItems);
        }

        // GET: SharingItem/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sharingItem = await _uow.SharingItems
                .FirstOrDefaultAsync(id.Value, User.UserGuidId());
            
            if (sharingItem == null)
            {
                return NotFound();
            }

            return View(sharingItem);
        }

        // GET: SharingItem/Create
        public async Task<IActionResult> Create()
        {
            ViewData["ItemId"] = new SelectList(await _uow.Items.AllAsync(User.UserGuidId()), "Id", "Name");
            ViewData["SharingId"] = new SelectList(await _uow.Sharings.AllAsync(User.UserGuidId()), "Id", "Name");
            return View();
        }

        // POST: SharingItem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SharingId,ItemId,FriendName,Percent,FriendOwns,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] SharingItem sharingItem)
        {
            if (ModelState.IsValid)
            {
                _uow.SharingItems.Add(sharingItem);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ItemId"] = new SelectList(await _uow.Items.AllAsync(User.UserGuidId()), "Id", "Name", sharingItem.ItemId);
            ViewData["SharingId"] = new SelectList(await _uow.Sharings.AllAsync(User.UserGuidId()), "Id", "Name", sharingItem.SharingId);
            return View(sharingItem);
        }

        // GET: SharingItem/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sharingItem = await _uow.SharingItems.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (sharingItem == null)
            {
                return NotFound();
            }
            ViewData["ItemId"] = new SelectList(await _uow.Items.AllAsync(User.UserGuidId()), "Id", "Name", sharingItem.ItemId);
            ViewData["SharingId"] = new SelectList(await _uow.Sharings.AllAsync(User.UserGuidId()), "Id", "Name", sharingItem.SharingId);
            return View(sharingItem);
        }

        // POST: SharingItem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("SharingId,ItemId,FriendName,Percent,FriendOwns,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] SharingItem sharingItem)
        {
            if (id != sharingItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.SharingItems.Update(sharingItem);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ItemId"] = new SelectList(await _uow.Items.AllAsync(User.UserGuidId()), "Id", "Name", sharingItem.ItemId);
            ViewData["SharingId"] = new SelectList(await _uow.Sharings.AllAsync(User.UserGuidId()), "Id", "Name", sharingItem.SharingId);
            return View(sharingItem);
        }

        // GET: SharingItem/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sharingItem = await _uow.SharingItems
                .FirstOrDefaultAsync(id.Value, User.UserGuidId());
            
            if (sharingItem == null)
            {
                return NotFound();
            }

            return View(sharingItem);
        }

        // POST: SharingItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.SharingItems.DeleteAsync(id, User.UserGuidId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
