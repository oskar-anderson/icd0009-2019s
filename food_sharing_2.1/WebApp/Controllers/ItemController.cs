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
    public class ItemController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public ItemController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Item
        public async Task<IActionResult> Index()
        {
            var items = await _uow.Items.AllAsync(User.UserGuidId());
            return View(items);
        }

        // GET: Item/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _uow.Items.FirstOrDefaultAsync(id.Value, User.UserGuidId());

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Item/Create
        public async Task<IActionResult> Create()
        {
            ViewData["InvoiceLineId"] = new SelectList(await _uow.InvoiceLines.AllAsync(User.UserGuidId()), "Id", "Name");
            ViewData["SharingId"] = new SelectList(await _uow.Sharings.AllAsync(User.UserGuidId()), "Id", "Name");
            return View();
        }

        // POST: Item/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SharingId,InvoiceLineId,Name,Net,Tax,Gross,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Item item)
        {
            if (ModelState.IsValid)
            {
                _uow.Items.Add(item);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InvoiceLineId"] = new SelectList(await _uow.InvoiceLines.AllAsync(User.UserGuidId()), "Id", "Name", item.InvoiceLineId);
            ViewData["SharingId"] = new SelectList(await _uow.Sharings.AllAsync(User.UserGuidId()), "Id", "Name", item.SharingId);
            return View(item);
        }

        // GET: Item/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _uow.Items.FirstOrDefaultAsync(id.Value, User.UserGuidId());

            if (item == null)
            {
                return NotFound();
            }
            ViewData["InvoiceLineId"] = new SelectList(await _uow.InvoiceLines.AllAsync(User.UserGuidId()), "Id", "Name", item.InvoiceLineId);
            ViewData["SharingId"] = new SelectList(await _uow.Sharings.AllAsync(User.UserGuidId()), "Id", "Name", item.SharingId);
            return View(item);
        }

        // POST: Item/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("SharingId,InvoiceLineId,Name,Net,Tax,Gross,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Item item)
        {
            if (id != item.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.Items.Update(item);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InvoiceLineId"] = new SelectList(await _uow.InvoiceLines.AllAsync(User.UserGuidId()), "Id", "Name", item.InvoiceLineId);
            ViewData["SharingId"] = new SelectList(await _uow.Sharings.AllAsync(User.UserGuidId()), "Id", "Name", item.SharingId);
            return View(item);
        }

        // GET: Item/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _uow.Items.FirstOrDefaultAsync(id.Value, User.UserGuidId());

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Item/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.Items.DeleteAsync(id, User.UserGuidId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
