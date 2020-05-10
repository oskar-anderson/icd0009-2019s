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
    public class UserLocationController : Controller
    {
        private readonly IAppBLL _bll;

        public UserLocationController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: UserLocation
        public async Task<IActionResult> Index()
        {
            var userLocations = await _bll.UserLocations.GetAllAsyncBase(User.UserId());
            return View(userLocations);
        }

        // GET: UserLocation/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userLocation = await _bll.UserLocations.FirstOrDefaultAsync(id.Value, User.UserId());

            if (userLocation == null)
            {
                return NotFound();
            }

            return View(userLocation);
        }

        // GET: UserLocation/Create
        public async Task<IActionResult> Create()
        {
            ViewData["AppUserId"] = new SelectList(await _bll.UserLocations.GetAllAsyncBase(User.UserId()), "Id", "FirstName");
            return View();
        }

        // POST: UserLocation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BLL.App.DTO.UserLocation userLocation)
        {
            if (ModelState.IsValid)
            {
                _bll.UserLocations.Add(userLocation);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(await _bll.UserLocations.GetAllAsyncBase(User.UserId()), "Id", "FirstName", userLocation.AppUserId);
            return View(userLocation);
        }

        // GET: UserLocation/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userLocation = await _bll.UserLocations.FirstOrDefaultAsync(id.Value, User.UserId());

            if (userLocation == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(await _bll.UserLocations.GetAllAsyncBase(User.UserId()), "Id", "FirstName", userLocation.AppUserId);
            return View(userLocation);
        }

        // POST: UserLocation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, BLL.App.DTO.UserLocation userLocation)
        {
            if (id != userLocation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _bll.UserLocations.UpdateAsync(userLocation);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(await _bll.UserLocations.GetAllAsyncBase(User.UserId()), "Id", "FirstName", userLocation.AppUserId);
            return View(userLocation);
        }

        // GET: UserLocation/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userLocation = await _bll.UserLocations.FirstOrDefaultAsync(id.Value, User.UserId());
            
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
            await _bll.UserLocations.RemoveAsync(id, User.UserId());
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
