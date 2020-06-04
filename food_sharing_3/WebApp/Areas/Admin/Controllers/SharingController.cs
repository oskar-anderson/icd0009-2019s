using System;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using DAL.App.EF;
using Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class SharingController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IAppBLL _bll;

        public SharingController(AppDbContext context, IAppBLL bll)
        {
            _context = context;
            _bll = bll;
        }

        // GET: Sharing
        public async Task<IActionResult> Index()
        {
            var sharings = await _bll.Sharings.GetAllForViewAsync(User.UserId());
            return View(sharings);
        }

        // GET: Sharing/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sharing = await _bll.Sharings.FirstOrDefaultViewAsync(id.Value, User.UserId());
                
            if (sharing == null)
            {
                return NotFound();
            }

            return View(sharing);
        }

        // GET: Sharing/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Users.Where(user => user.Id == User.UserId()), "Id", "FirstName");
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
            ViewData["AppUserId"] = new SelectList(_context.Users.Where(user => user.Id == User.UserId()), "Id", "FirstName");
            return View(sharing);
        }

        // GET: Sharing/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sharing = await _bll.Sharings.FirstOrDefaultViewAsync(id.Value, User.UserId());
            
            if (sharing == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.Users.Where(user => user.Id == User.UserId()), "Id", "FirstName");
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
            
            ViewData["AppUserId"] = new SelectList(_context.Users.Where(user => user.Id == User.UserId()), "Id", "FirstName");
            return View(sharing);
        }

        // GET: Sharing/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sharing = await _bll.Sharings.FirstOrDefaultViewAsync(id.Value, User.UserId());
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
