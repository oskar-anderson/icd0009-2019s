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
    public class ClientGroupController : Controller
    {
        private readonly AppDbContext _context;

        public ClientGroupController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ClientGroup
        public async Task<IActionResult> Index()
        {
            return View(await _context.ClientGroups.ToListAsync());
        }

        // GET: ClientGroup/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientGroup = await _context.ClientGroups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clientGroup == null)
            {
                return NotFound();
            }

            return View(clientGroup);
        }

        // GET: ClientGroup/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ClientGroup/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] ClientGroup clientGroup)
        {
            if (ModelState.IsValid)
            {
                clientGroup.Id = Guid.NewGuid();
                _context.Add(clientGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clientGroup);
        }

        // GET: ClientGroup/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientGroup = await _context.ClientGroups.FindAsync(id);
            if (clientGroup == null)
            {
                return NotFound();
            }
            return View(clientGroup);
        }

        // POST: ClientGroup/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Description,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] ClientGroup clientGroup)
        {
            if (id != clientGroup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clientGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientGroupExists(clientGroup.Id))
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
            return View(clientGroup);
        }

        // GET: ClientGroup/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientGroup = await _context.ClientGroups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clientGroup == null)
            {
                return NotFound();
            }

            return View(clientGroup);
        }

        // POST: ClientGroup/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var clientGroup = await _context.ClientGroups.FindAsync(id);
            _context.ClientGroups.Remove(clientGroup);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientGroupExists(Guid id)
        {
            return _context.ClientGroups.Any(e => e.Id == id);
        }
    }
}
