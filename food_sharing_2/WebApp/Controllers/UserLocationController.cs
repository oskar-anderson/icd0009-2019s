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
    public class UserLocationController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public UserLocationController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: UserLocation
        public async Task<IActionResult> Index()
        {
            var userLocations = await _uow.UserLocations.AllAsync(User.UserGuidId());
            return View(userLocations);
        }

        // GET: UserLocation/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userLocation = await _uow.UserLocations.FirstOrDefaultAsync(id.Value, User.UserGuidId());

            if (userLocation == null)
            {
                return NotFound();
            }

            return View(userLocation);
        }

        // GET: UserLocation/Create
        public async Task<IActionResult> Create()
        {
            ViewData["AppUserId"] = new SelectList(await _uow.UserLocations.AllAsync(User.UserGuidId()), "Id", "FirstName");
            return View();
        }

        // POST: UserLocation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppUserId,District,StreetName,BuildingNumber,ApartmentNumber,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] UserLocation userLocation)
        {
            if (ModelState.IsValid)
            {
                _uow.UserLocations.Add(userLocation);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(await _uow.UserLocations.AllAsync(User.UserGuidId()), "Id", "FirstName", userLocation.AppUserId);
            return View(userLocation);
        }

        // GET: UserLocation/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userLocation = await _uow.UserLocations.FirstOrDefaultAsync(id.Value, User.UserGuidId());

            if (userLocation == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(await _uow.UserLocations.AllAsync(User.UserGuidId()), "Id", "FirstName", userLocation.AppUserId);
            return View(userLocation);
        }

        // POST: UserLocation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AppUserId,District,StreetName,BuildingNumber,ApartmentNumber,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] UserLocation userLocation)
        {
            if (id != userLocation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.UserLocations.Update(userLocation);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(await _uow.UserLocations.AllAsync(User.UserGuidId()), "Id", "FirstName", userLocation.AppUserId);
            return View(userLocation);
        }

        // GET: UserLocation/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userLocation = await _uow.UserLocations.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            
            if (userLocation == null)
            {
                return NotFound();
            }

            return View(userLocation);
        }

        // POST: UserLocation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.UserLocations.DeleteAsync(id, User.UserGuidId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
