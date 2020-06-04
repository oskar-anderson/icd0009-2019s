#pragma warning disable 1591
using System;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using DAL.App.EF;
using Domain.App;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class GpsSessionTypesController : Controller
    {
        private readonly IAppBLL _bll;

        public GpsSessionTypesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: GpsSessionTypes
        public async Task<IActionResult> Index()
        {
            var res = await _bll.GpsSessionTypes.GetAllAsync();

            /*
            var appDbContext = _context.GpsSessionTypes
                .Include(g => g.Description)
                .ThenInclude(g => g!.Translations)
                .Include(g => g.Name)
                .ThenInclude(g => g!.Translations);
                */
            return View(res);
        }

        // GET: GpsSessionTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gpsSessionType = await _bll.GpsSessionTypes.FirstOrDefaultAsync(id.Value);
            if (gpsSessionType == null)
            {
                return NotFound();
            }

            return View(gpsSessionType);
        }

        // GET: GpsSessionTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GpsSessionTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BLL.App.DTO.GpsSessionType gpsSessionType)
        {
            if (ModelState.IsValid)
            {
                _bll.GpsSessionTypes.Add(gpsSessionType);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(gpsSessionType);
        }

        // GET: GpsSessionTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gpsSessionType = await _bll.GpsSessionTypes.FirstOrDefaultAsync(id.Value);
            if (gpsSessionType == null)
            {
                return NotFound();
            }

            return View(gpsSessionType);
        }

        // POST: GpsSessionTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, BLL.App.DTO.GpsSessionType gpsSessionType)
        {
            if (id != gpsSessionType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _bll.GpsSessionTypes.UpdateAsync(gpsSessionType);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(gpsSessionType);
        }

        // GET: GpsSessionTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gpsSessionType = await _bll.GpsSessionTypes.FirstOrDefaultAsync(id.Value);
            
            if (gpsSessionType == null)
            {
                return NotFound();
            }

            return View(gpsSessionType);
        }

        // POST: GpsSessionTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var gpsSessionType = await _bll.GpsSessionTypes.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}