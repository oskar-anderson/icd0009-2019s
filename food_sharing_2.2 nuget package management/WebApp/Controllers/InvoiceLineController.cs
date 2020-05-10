using System;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Extensions;

namespace WebApp.Controllers
{
    public class InvoiceLineController : Controller
    {
        private readonly IAppBLL _bll;

        public InvoiceLineController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: InvoiceLine
        public async Task<IActionResult> Index()
        {
            var invoiceLines = await _bll.InvoiceLines.GetAllAsyncBase();
            return View(invoiceLines);
        }

        // GET: InvoiceLine/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceLine = await _bll.InvoiceLines.FirstOrDefaultAsync(id.Value);

            if (invoiceLine == null)
            {
                return NotFound();
            }

            return View(invoiceLine);
        }

        // GET: InvoiceLine/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CartId"] = new SelectList(await _bll.Carts.GetAllAsyncBase(), "Id", "Id");
            ViewData["InvoiceId"] = new SelectList(await _bll.Invoices.GetAllAsyncBase(), "Id", "Id");
            return View();
        }

        // POST: InvoiceLine/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BLL.App.DTO.InvoiceLine invoiceLine)
        {
            if (ModelState.IsValid)
            {
                _bll.InvoiceLines.Add(invoiceLine);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CartId"] = new SelectList(await _bll.Carts.GetAllAsyncBase(), "Id", "Id", invoiceLine.CartId);
            ViewData["InvoiceId"] = new SelectList(await _bll.Invoices.GetAllAsyncBase(), "Id", "Id", invoiceLine.InvoiceId);
            return View(invoiceLine);
        }

        // GET: InvoiceLine/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceLine = await _bll.InvoiceLines.FirstOrDefaultAsync(id.Value);

            if (invoiceLine == null)
            {
                return NotFound();
            }
            ViewData["CartId"] = new SelectList(await _bll.Carts.GetAllAsyncBase(), "Id", "Id", invoiceLine.CartId);
            ViewData["InvoiceId"] = new SelectList(await _bll.Invoices.GetAllAsyncBase(), "Id", "Id", invoiceLine.InvoiceId);
            return View(invoiceLine);
        }

        // POST: InvoiceLine/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, BLL.App.DTO.InvoiceLine invoiceLine)
        {
            if (id != invoiceLine.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _bll.InvoiceLines.UpdateAsync(invoiceLine);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CartId"] = new SelectList(await _bll.Carts.GetAllAsyncBase(), "Id", "Id", invoiceLine.CartId);
            ViewData["InvoiceId"] = new SelectList(await _bll.Invoices.GetAllAsyncBase(), "Id", "Id", invoiceLine.InvoiceId);
            return View(invoiceLine);
        }

        // GET: InvoiceLine/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceLine = await _bll.InvoiceLines.FirstOrDefaultAsync(id.Value);

            if (invoiceLine == null)
            {
                return NotFound();
            }

            return View(invoiceLine);
        }

        // POST: InvoiceLine/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.InvoiceLines.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
