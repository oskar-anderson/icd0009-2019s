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
    public class PaymentMethodController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public PaymentMethodController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: PaymentMethod
        public async Task<IActionResult> Index()
        {
            var paymentMethods = await _uow.PaymentMethods.AllAsync(User.UserGuidId());
            return View(paymentMethods);
        }

        // GET: PaymentMethod/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentMethod = await _uow.PaymentMethods.FirstOrDefaultAsync(id.Value, User.UserGuidId());

            if (paymentMethod == null)
            {
                return NotFound();
            }

            return View(paymentMethod);
        }

        // GET: PaymentMethod/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PaymentMethod/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Since,Until,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] PaymentMethod paymentMethod)
        {
            if (ModelState.IsValid)
            {
                _uow.PaymentMethods.Add(paymentMethod);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(paymentMethod);
        }

        // GET: PaymentMethod/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentMethod = await _uow.PaymentMethods.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            
            if (paymentMethod == null)
            {
                return NotFound();
            }
            return View(paymentMethod);
        }

        // POST: PaymentMethod/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Since,Until,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] PaymentMethod paymentMethod)
        {
            if (id != paymentMethod.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.PaymentMethods.Update(paymentMethod);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(paymentMethod);
        }

        // GET: PaymentMethod/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentMethod = await _uow.PaymentMethods.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            
            if (paymentMethod == null)
            {
                return NotFound();
            }

            return View(paymentMethod);
        }

        // POST: PaymentMethod/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.PaymentMethods.DeleteAsync(id, User.UserGuidId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
