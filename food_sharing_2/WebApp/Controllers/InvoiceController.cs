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
    public class InvoiceController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public InvoiceController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Invoice
        public async Task<IActionResult> Index()
        {
            var invoices = await _uow.Invoices.AllAsync(User.UserGuidId());
            return View(invoices);
        }

        // GET: Invoice/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _uow.Invoices.FirstOrDefaultAsync(id.Value, User.UserGuidId());

            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // GET: Invoice/Create
        public async Task<IActionResult> Create()
        {
            ViewData["PaymentMethodId"] = new SelectList(await _uow.PaymentMethods.AllAsync(User.UserGuidId()), "Id", "Name");
            ViewData["PersonId"] = new SelectList(await _uow.Persons.AllAsync(User.UserGuidId()), "Id", "FirstName");
            ViewData["RestaurantId"] = new SelectList(await _uow.Restaurants.AllAsync(User.UserGuidId()), "Id", "Location");
            return View();
        }

        // POST: Invoice/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonId,RestaurantId,PaymentMethodId,TotalNet,TotalTax,TotalGross,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                _uow.Invoices.Add(invoice);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PaymentMethodId"] = new SelectList(await _uow.PaymentMethods.AllAsync(User.UserGuidId()), "Id", "Name", invoice.PaymentMethodId);
            ViewData["PersonId"] = new SelectList(await _uow.Persons.AllAsync(User.UserGuidId()), "Id", "FirstName", invoice.PersonId);
            ViewData["RestaurantId"] = new SelectList(await _uow.Restaurants.AllAsync(User.UserGuidId()), "Id", "Location", invoice.RestaurantId);
            return View(invoice);
        }

        // GET: Invoice/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _uow.Invoices.FirstOrDefaultAsync(id.Value, User.UserGuidId());

            if (invoice == null)
            {
                return NotFound();
            }
            ViewData["PaymentMethodId"] = new SelectList(await _uow.PaymentMethods.AllAsync(User.UserGuidId()), "Id", "Name", invoice.PaymentMethodId);
            ViewData["PersonId"] = new SelectList(await _uow.Persons.AllAsync(User.UserGuidId()), "Id", "FirstName", invoice.PersonId);
            ViewData["RestaurantId"] = new SelectList(await _uow.Restaurants.AllAsync(User.UserGuidId()), "Id", "Location", invoice.RestaurantId);
            return View(invoice);
        }

        // POST: Invoice/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PersonId,RestaurantId,PaymentMethodId,TotalNet,TotalTax,TotalGross,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Invoice invoice)
        {
            if (id != invoice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.Invoices.Update(invoice);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PaymentMethodId"] = new SelectList(await _uow.PaymentMethods.AllAsync(User.UserGuidId()), "Id", "Name", invoice.PaymentMethodId);
            ViewData["PersonId"] = new SelectList(await _uow.Persons.AllAsync(User.UserGuidId()), "Id", "FirstName", invoice.PersonId);
            ViewData["RestaurantId"] = new SelectList(await _uow.Restaurants.AllAsync(User.UserGuidId()), "Id", "Location", invoice.RestaurantId);
            return View(invoice);
        }

        // GET: Invoice/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _uow.Invoices.FirstOrDefaultAsync(id.Value, User.UserGuidId());

            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // POST: Invoice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.Invoices.DeleteAsync(id, User.UserGuidId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
