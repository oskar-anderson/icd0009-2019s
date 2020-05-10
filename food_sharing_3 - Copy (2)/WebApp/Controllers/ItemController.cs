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
    public class ItemController : Controller
    {
        private readonly IAppBLL _bll;

        public ItemController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Item
        public async Task<IActionResult> Index()
        {
            var items = await _bll.Items.GetAllAsyncBase();
            return View(items);
        }

        // GET: Item/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _bll.Items.FirstOrDefaultAsync(id.Value);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Item/Create
        public async Task<IActionResult> Create()
        {
            ViewData["InvoiceLineId"] = new SelectList(await _bll.InvoiceLines.GetAllAsyncBase(), "Id", "Name");
            ViewData["SharingId"] = new SelectList(await _bll.Sharings.GetAllAsyncBase(), "Id", "Name");
            return View();
        }

        // POST: Item/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BLL.App.DTO.Item item)
        {
            if (ModelState.IsValid)
            {
                _bll.Items.Add(item);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InvoiceLineId"] = new SelectList(await _bll.InvoiceLines.GetAllAsyncBase(), "Id", "Name", item.InvoiceLineId);
            ViewData["SharingId"] = new SelectList(await _bll.Sharings.GetAllAsyncBase(), "Id", "Name", item.SharingId);
            return View(item);
        }

        // GET: Item/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _bll.Items.FirstOrDefaultAsync(id.Value);

            if (item == null)
            {
                return NotFound();
            }
            ViewData["InvoiceLineId"] = new SelectList(await _bll.InvoiceLines.GetAllAsyncBase(), "Id", "Name", item.InvoiceLineId);
            ViewData["SharingId"] = new SelectList(await _bll.Sharings.GetAllAsyncBase(), "Id", "Name", item.SharingId);
            return View(item);
        }

        // POST: Item/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, BLL.App.DTO.Item item)
        {
            if (id != item.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _bll.Items.UpdateAsync(item);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InvoiceLineId"] = new SelectList(await _bll.InvoiceLines.GetAllAsyncBase(), "Id", "Name", item.InvoiceLineId);
            ViewData["SharingId"] = new SelectList(await _bll.Sharings.GetAllAsyncBase(), "Id", "Name", item.SharingId);
            return View(item);
        }

        // GET: Item/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _bll.Items.FirstOrDefaultAsync(id.Value);

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
            await _bll.Items.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}