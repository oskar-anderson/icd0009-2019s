using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;
using Extensions;

namespace WebApp.Controllers
{
    public class UserClientGroupController : Controller
    {
        private readonly AppDbContext _context;

        public UserClientGroupController(AppDbContext context)
        {
            _context = context;
        }

        // GET: UserClientGroup
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.UserClientGroups.Include(u => u.AppUser).Include(u => u.ClientGroup);
            return View(await appDbContext.ToListAsync());
        }

        // GET: UserClientGroup/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userClientGroup = await _context.UserClientGroups
                .Include(u => u.AppUser)
                .Include(u => u.ClientGroup)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userClientGroup == null)
            {
                return NotFound();
            }

            return View(userClientGroup);
        }

        // GET: UserClientGroup/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["ClientGroupId"] = new SelectList(_context.ClientGroups, "Id", "Id");
            return View();
        }

        // POST: UserClientGroup/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppUserId,ClientGroupId,Since,Until,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] UserClientGroup userClientGroup)
        {
            userClientGroup.AppUserId = User.GetUserId();
            
            if (ModelState.IsValid)
            {
                _context.Add(userClientGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", userClientGroup.AppUserId);
            ViewData["ClientGroupId"] = new SelectList(_context.ClientGroups, "Id", "Id", userClientGroup.ClientGroupId);
            return View(userClientGroup);
        }

        // GET: UserClientGroup/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userClientGroup = await _context.UserClientGroups.FindAsync(id);
            if (userClientGroup == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", userClientGroup.AppUserId);
            ViewData["ClientGroupId"] = new SelectList(_context.ClientGroups, "Id", "Id", userClientGroup.ClientGroupId);
            return View(userClientGroup);
        }

        // POST: UserClientGroup/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("AppUserId,ClientGroupId,Since,Until,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] UserClientGroup userClientGroup)
        {
            if (id != userClientGroup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userClientGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserClientGroupExists(userClientGroup.Id))
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
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", userClientGroup.AppUserId);
            ViewData["ClientGroupId"] = new SelectList(_context.ClientGroups, "Id", "Id", userClientGroup.ClientGroupId);
            return View(userClientGroup);
        }

        // GET: UserClientGroup/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userClientGroup = await _context.UserClientGroups
                .Include(u => u.AppUser)
                .Include(u => u.ClientGroup)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userClientGroup == null)
            {
                return NotFound();
            }

            return View(userClientGroup);
        }

        // POST: UserClientGroup/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var userClientGroup = await _context.UserClientGroups.FindAsync(id);
            _context.UserClientGroups.Remove(userClientGroup);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserClientGroupExists(string id)
        {
            return _context.UserClientGroups.Any(e => e.Id == id);
        }
    }
}
