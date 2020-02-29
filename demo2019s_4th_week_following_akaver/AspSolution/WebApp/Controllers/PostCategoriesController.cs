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
    public class PostCategoriesController : Controller
    {
        private readonly AppDbContext _context;

        public PostCategoriesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: PostCategories
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.PostCategories.Include(p => p.Category).Include(p => p.Post);
            return View(await appDbContext.ToListAsync());
        }

        // GET: PostCategories/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postCategory = await _context.PostCategories
                .Include(p => p.Category)
                .Include(p => p.Post)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (postCategory == null)
            {
                return NotFound();
            }

            return View(postCategory);
        }

        // GET: PostCategories/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id");
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Id");
            return View();
        }

        // POST: PostCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PostId,CategoryId,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] PostCategory postCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(postCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", postCategory.CategoryId);
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Id", postCategory.PostId);
            return View(postCategory);
        }

        // GET: PostCategories/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postCategory = await _context.PostCategories.FindAsync(id);
            if (postCategory == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", postCategory.CategoryId);
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Id", postCategory.PostId);
            return View(postCategory);
        }

        // POST: PostCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PostId,CategoryId,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] PostCategory postCategory)
        {
            if (id != postCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(postCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostCategoryExists(postCategory.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", postCategory.CategoryId);
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Id", postCategory.PostId);
            return View(postCategory);
        }

        // GET: PostCategories/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postCategory = await _context.PostCategories
                .Include(p => p.Category)
                .Include(p => p.Post)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (postCategory == null)
            {
                return NotFound();
            }

            return View(postCategory);
        }

        // POST: PostCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var postCategory = await _context.PostCategories.FindAsync(id);
            _context.PostCategories.Remove(postCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostCategoryExists(string id)
        {
            return _context.PostCategories.Any(e => e.Id == id);
        }
    }
}
