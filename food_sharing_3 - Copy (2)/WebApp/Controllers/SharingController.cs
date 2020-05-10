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
    public class SharingController : Controller
    {
        private readonly IAppBLL _bll;

        public SharingController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Sharing
        public async Task<IActionResult> Index()
        {
            var sharings = await _bll.Sharings.GetAllAsyncBase(User.UserId());
            return View(sharings);
        }

        // GET: Sharing/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sharing = await _bll.Sharings.FirstOrDefaultAsync(id.Value, User.UserId());
                
            if (sharing == null)
            {
                return NotFound();
            }

            return View(sharing);
        }

        // GET: Sharing/Create
        public IActionResult Create()
        {
            
            return View();
        }

        // POST: Sharing/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BLL.App.DTO.Sharing sharing)
        {
            sharing.AppUserId = User.UserId();
            if (ModelState.IsValid)
            {
                _bll.Sharings.Add(sharing);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(sharing);
        }

        // GET: Sharing/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sharing = await _bll.Sharings.FirstOrDefaultAsync(id.Value, User.UserId());
            
            if (sharing == null)
            {
                return NotFound();
            }
            
            return View(sharing);
        }

        // POST: Sharing/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, BLL.App.DTO.Sharing sharing)
        {
            sharing.AppUserId = User.UserId();
            
            if (id != sharing.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _bll.Sharings.UpdateAsync(sharing);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(sharing);
        }

        // GET: Sharing/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sharing = await _bll.Sharings
                .FirstOrDefaultAsync(id.Value, User.UserId());
            if (sharing == null)
            {
                return NotFound();
            }

            return View(sharing);
        }

        // POST: Sharing/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.Sharings.RemoveAsync(id, User.UserId());
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
