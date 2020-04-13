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
    public class InvoiceLineController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public InvoiceLineController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: InvoiceLine
        public async Task<IActionResult> Index()
        {
            IEnumerable<InvoiceLine> invoiceLines = await _uow.InvoiceLines.AllAsync(User.UserGuidId());
            return View(invoiceLines);
        }

        // GET: InvoiceLine/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceLine = await _uow.InvoiceLines.FirstOrDefaultAsync(id.Value, User.UserGuidId());

            if (invoiceLine == null)
            {
                return NotFound();
            }

            return View(invoiceLine);
        }

        // GET: InvoiceLine/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CartId"] = new SelectList(await _uow.Carts.AllAsync(User.UserGuidId()), "Id", "Id");
            ViewData["InvoiceId"] = new SelectList(await _uow.Invoices.AllAsync(User.UserGuidId()), "Id", "Id");
            return View();
        }

        // POST: InvoiceLine/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CartId,InvoiceId,Name,Quantity,Net,Tax,Gross,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] InvoiceLine invoiceLine)
        {
            if (ModelState.IsValid)
            {
                _uow.InvoiceLines.Add(invoiceLine);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CartId"] = new SelectList(await _uow.Carts.AllAsync(User.UserGuidId()), "Id", "Id", invoiceLine.CartId);
            ViewData["InvoiceId"] = new SelectList(await _uow.Invoices.AllAsync(User.UserGuidId()), "Id", "Id", invoiceLine.InvoiceId);
            return View(invoiceLine);
        }

        // GET: InvoiceLine/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceLine = await _uow.InvoiceLines.FirstOrDefaultAsync(id.Value, User.UserGuidId());

            if (invoiceLine == null)
            {
                return NotFound();
            }
            ViewData["CartId"] = new SelectList(await _uow.Carts.AllAsync(User.UserGuidId()), "Id", "Id", invoiceLine.CartId);
            ViewData["InvoiceId"] = new SelectList(await _uow.Invoices.AllAsync(User.UserGuidId()), "Id", "Id", invoiceLine.InvoiceId);
            return View(invoiceLine);
        }

        // POST: InvoiceLine/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CartId,InvoiceId,Name,Quantity,Net,Tax,Gross,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] InvoiceLine invoiceLine)
        {
            if (id != invoiceLine.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.InvoiceLines.Update(invoiceLine);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CartId"] = new SelectList(await _uow.Carts.AllAsync(User.UserGuidId()), "Id", "Id", invoiceLine.CartId);
            ViewData["InvoiceId"] = new SelectList(await _uow.Invoices.AllAsync(User.UserGuidId()), "Id", "Id", invoiceLine.InvoiceId);
            return View(invoiceLine);
        }

        // GET: InvoiceLine/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceLine = await _uow.InvoiceLines.FirstOrDefaultAsync(id.Value, User.UserGuidId());

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
            await _uow.InvoiceLines.DeleteAsync(id, User.UserGuidId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
