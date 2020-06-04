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
    [Authorize]
    public class SharingItemController : Controller
    {
        private readonly IAppBLL _bll;

        public SharingItemController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: SharingItem
        public async Task<IActionResult> Index()
        {
            var sharingItems = await _bll.SharingItems.GetAllForViewAsync();
            return View(sharingItems);
        }

        // GET: SharingItem/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sharingItem = await _bll.SharingItems.FirstOrDefaultViewAsync(id.Value);
            
            if (sharingItem == null)
            {
                return NotFound();
            }

            return View(sharingItem);
        }

        // GET: SharingItem/Create
        public async Task<IActionResult> Create()
        {
            ViewData["ItemId"] = new SelectList(await _bll.Items.GetAllAsyncBase(), "Id", "Name");
            ViewData["SharingId"] = new SelectList(await _bll.Sharings.GetAllAsyncBase(), "Id", "Name");
            return View();
        }

        // POST: SharingItem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BLL.App.DTO.SharingItem sharingItem)
        {
            if (ModelState.IsValid)
            {
                _bll.SharingItems.Add(sharingItem);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ItemId"] = new SelectList(await _bll.Items.GetAllAsyncBase(), "Id", "Name", sharingItem.ItemId);
            ViewData["SharingId"] = new SelectList(await _bll.Sharings.GetAllAsyncBase(), "Id", "Name", sharingItem.SharingId);
            return View(sharingItem);
        }

        // GET: SharingItem/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sharingItem = await _bll.SharingItems.FirstOrDefaultViewAsync(id.Value);
            if (sharingItem == null)
            {
                return NotFound();
            }
            ViewData["ItemId"] = new SelectList(await _bll.Items.GetAllAsyncBase(), "Id", "Name", sharingItem.ItemId);
            ViewData["SharingId"] = new SelectList(await _bll.Sharings.GetAllAsyncBase(), "Id", "Name", sharingItem.SharingId);
            return View(sharingItem);
        }

        // POST: SharingItem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, BLL.App.DTO.SharingItem sharingItem)
        {
            if (id != sharingItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _bll.SharingItems.UpdateAsync(sharingItem);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ItemId"] = new SelectList(await _bll.Items.GetAllAsyncBase(User.UserId()), "Id", "Name", sharingItem.ItemId);
            ViewData["SharingId"] = new SelectList(await _bll.Sharings.GetAllAsyncBase(User.UserId()), "Id", "Name", sharingItem.SharingId);
            return View(sharingItem);
        }

        // GET: SharingItem/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sharingItem = await _bll.SharingItems.FirstOrDefaultViewAsync(id.Value, User.UserId());
            
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
            await _bll.SharingItems.RemoveAsync(id, User.UserId());
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
