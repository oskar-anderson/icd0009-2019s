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
    public class SharingItemController : Controller
    {
        private readonly AppDbContext _context;

        public SharingItemController(AppDbContext context)
        {
            _context = context;
        }

        // GET: SharingItem
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.SharingItems.Include(s => s.Friend).Include(s => s.Item).Include(s => s.Sharing);
            return View(await appDbContext.ToListAsync());
        }

        // GET: SharingItem/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sharingItem = await _context.SharingItems
                .Include(s => s.Friend)
                .Include(s => s.Item)
                .Include(s => s.Sharing)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sharingItem == null)
            {
                return NotFound();
            }

            return View(sharingItem);
        }

        // GET: SharingItem/Create
        public IActionResult Create()
        {
            ViewData["FriendId"] = new SelectList(_context.Friends, "Id", "Id");
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Id");
            ViewData["SharingId"] = new SelectList(_context.Sharings, "Id", "Id");
            return View();
        }

        // POST: SharingItem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SharingId,ItemId,FriendId,Percent,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] SharingItem sharingItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sharingItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FriendId"] = new SelectList(_context.Friends, "Id", "Id", sharingItem.FriendId);
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Id", sharingItem.ItemId);
            ViewData["SharingId"] = new SelectList(_context.Sharings, "Id", "Id", sharingItem.SharingId);
            return View(sharingItem);
        }

        // GET: SharingItem/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sharingItem = await _context.SharingItems.FindAsync(id);
            if (sharingItem == null)
            {
                return NotFound();
            }
            ViewData["FriendId"] = new SelectList(_context.Friends, "Id", "Id", sharingItem.FriendId);
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Id", sharingItem.ItemId);
            ViewData["SharingId"] = new SelectList(_context.Sharings, "Id", "Id", sharingItem.SharingId);
            return View(sharingItem);
        }

        // POST: SharingItem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("SharingId,ItemId,FriendId,Percent,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] SharingItem sharingItem)
        {
            if (id != sharingItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sharingItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SharingItemExists(sharingItem.Id))
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
            ViewData["FriendId"] = new SelectList(_context.Friends, "Id", "Id", sharingItem.FriendId);
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Id", sharingItem.ItemId);
            ViewData["SharingId"] = new SelectList(_context.Sharings, "Id", "Id", sharingItem.SharingId);
            return View(sharingItem);
        }

        // GET: SharingItem/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sharingItem = await _context.SharingItems
                .Include(s => s.Friend)
                .Include(s => s.Item)
                .Include(s => s.Sharing)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sharingItem == null)
            {
                return NotFound();
            }

            return View(sharingItem);
        }

        // POST: SharingItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var sharingItem = await _context.SharingItems.FindAsync(id);
            _context.SharingItems.Remove(sharingItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SharingItemExists(string id)
        {
            return _context.SharingItems.Any(e => e.Id == id);
        }
    }
}
