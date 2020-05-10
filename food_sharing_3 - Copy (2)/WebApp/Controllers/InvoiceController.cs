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
    public class InvoiceController : Controller
    {
        private readonly IAppBLL _bll;

        public InvoiceController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Invoice
        public async Task<IActionResult> Index()
        {
            var invoices = await _bll.Invoices.GetAllAsyncBase();
            return View(invoices);
        }

        // GET: Invoice/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _bll.Invoices.FirstOrDefaultAsync(id.Value);

            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // GET: Invoice/Create
        public async Task<IActionResult> Create()
        {
            ViewData["PaymentMethodId"] = new SelectList(await _bll.PaymentMethods.GetAllAsyncBase(), "Id", "Name");
            ViewData["PersonId"] = new SelectList(await _bll.Persons.GetAllAsyncBase(), "Id", "FirstName");
            ViewData["RestaurantId"] = new SelectList(await _bll.Restaurants.GetAllAsyncBase(), "Id", "Location");
            return View();
        }

        // POST: Invoice/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BLL.App.DTO.Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                _bll.Invoices.Add(invoice);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PaymentMethodId"] = new SelectList(await _bll.PaymentMethods.GetAllAsyncBase(), "Id", "Name", invoice.PaymentMethodId);
            ViewData["PersonId"] = new SelectList(await _bll.Persons.GetAllAsyncBase(), "Id", "FirstName", invoice.PersonId);
            ViewData["RestaurantId"] = new SelectList(await _bll.Restaurants.GetAllAsyncBase(), "Id", "Location", invoice.RestaurantId);
            return View(invoice);
        }

        // GET: Invoice/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _bll.Invoices.FirstOrDefaultAsync(id.Value);

            if (invoice == null)
            {
                return NotFound();
            }
            ViewData["PaymentMethodId"] = new SelectList(await _bll.PaymentMethods.GetAllAsyncBase(), "Id", "Name", invoice.PaymentMethodId);
            ViewData["PersonId"] = new SelectList(await _bll.Persons.GetAllAsyncBase(), "Id", "FirstName", invoice.PersonId);
            ViewData["RestaurantId"] = new SelectList(await _bll.Restaurants.GetAllAsyncBase(), "Id", "Location", invoice.RestaurantId);
            return View(invoice);
        }

        // POST: Invoice/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, BLL.App.DTO.Invoice invoice)
        {
            if (id != invoice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _bll.Invoices.UpdateAsync(invoice);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PaymentMethodId"] = new SelectList(await _bll.PaymentMethods.GetAllAsyncBase(), "Id", "Name", invoice.PaymentMethodId);
            ViewData["PersonId"] = new SelectList(await _bll.Persons.GetAllAsyncBase(), "Id", "FirstName", invoice.PersonId);
            ViewData["RestaurantId"] = new SelectList(await _bll.Restaurants.GetAllAsyncBase(), "Id", "Location", invoice.RestaurantId);
            return View(invoice);
        }

        // GET: Invoice/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _bll.Invoices.FirstOrDefaultAsync(id.Value);

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
            await _bll.Invoices.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
