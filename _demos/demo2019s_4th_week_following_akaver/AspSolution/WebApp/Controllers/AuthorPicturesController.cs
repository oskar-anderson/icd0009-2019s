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
    public class AuthorPicturesController : Controller
    {
        private readonly AppDbContext _context;

        public AuthorPicturesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: AuthorPictures
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.AuthorPictures.Include(a => a.Author);
            return View(await appDbContext.ToListAsync());
        }

        // GET: AuthorPictures/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorPicture = await _context.AuthorPictures
                .Include(a => a.Author)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (authorPicture == null)
            {
                return NotFound();
            }

            return View(authorPicture);
        }

        // GET: AuthorPictures/Create
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Id");
            return View();
        }

        // POST: AuthorPictures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PictureURL,AuthorId,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] AuthorPicture authorPicture)
        {
            if (ModelState.IsValid)
            {
                _context.Add(authorPicture);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Id", authorPicture.AuthorId);
            return View(authorPicture);
        }

        // GET: AuthorPictures/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorPicture = await _context.AuthorPictures.FindAsync(id);
            if (authorPicture == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Id", authorPicture.AuthorId);
            return View(authorPicture);
        }

        // POST: AuthorPictures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PictureURL,AuthorId,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] AuthorPicture authorPicture)
        {
            if (id != authorPicture.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(authorPicture);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorPictureExists(authorPicture.Id))
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
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Id", authorPicture.AuthorId);
            return View(authorPicture);
        }

        // GET: AuthorPictures/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorPicture = await _context.AuthorPictures
                .Include(a => a.Author)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (authorPicture == null)
            {
                return NotFound();
            }

            return View(authorPicture);
        }

        // POST: AuthorPictures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var authorPicture = await _context.AuthorPictures.FindAsync(id);
            _context.AuthorPictures.Remove(authorPicture);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorPictureExists(string id)
        {
            return _context.AuthorPictures.Any(e => e.Id == id);
        }
    }
}
