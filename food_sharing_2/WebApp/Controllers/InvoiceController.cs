using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;

namespace WebApp.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly AppDbContext _context;

        public InvoiceController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Invoice
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Invoices.Include(i => i.PaymentMethod).Include(i => i.Person).Include(i => i.Restaurant);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Invoice/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices
                .Include(i => i.PaymentMethod)
                .Include(i => i.Person)
                .Include(i => i.Restaurant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // GET: Invoice/Create
        public IActionResult Create()
        {
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentOptions, "Id", "Id");
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "Id");
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Id");
            return View();
        }

        // POST: Invoice/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonId,RestaurantId,PaymentMethodId,InvoiceCode,TotalNet,TotalTax,TotalGross,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(invoice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentOptions, "Id", "Id", invoice.PaymentMethodId);
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "Id", invoice.PersonId);
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Id", invoice.RestaurantId);
            return View(invoice);
        }

        // GET: Invoice/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentOptions, "Id", "Id", invoice.PaymentMethodId);
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "Id", invoice.PersonId);
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Id", invoice.RestaurantId);
            return View(invoice);
        }

        // POST: Invoice/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PersonId,RestaurantId,PaymentMethodId,InvoiceCode,TotalNet,TotalTax,TotalGross,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] Invoice invoice)
        {
            if (id != invoice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceExists(invoice.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentOptions, "Id", "Id", invoice.PaymentMethodId);
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "Id", invoice.PersonId);
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Id", invoice.RestaurantId);
            return View(invoice);
        }

        // GET: Invoice/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices
                .Include(i => i.PaymentMethod)
                .Include(i => i.Person)
                .Include(i => i.Restaurant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // POST: Invoice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceExists(string id)
        {
            return _context.Invoices.Any(e => e.Id == id);
        }
    }
}
