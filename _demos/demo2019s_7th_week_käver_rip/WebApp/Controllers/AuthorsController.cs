using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using DAL.App.EF.Repositories;
using Domain;

namespace WebApp.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IAuthorRepository _authorRepository;

        public AuthorsController(AppDbContext context)
        {
            _context = context;
            _authorRepository = new AuthorRepository(_context);
        }

        // GET: Authors
        public async Task<IActionResult> Index()
        {
            return View(await _authorRepository.AllAsync());
        }

        // GET: Authors/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _authorRepository.FindAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // GET: Authors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("FirstName,LastName,AppUserId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")]
            Author author)
        {
            if (ModelState.IsValid)
            {
                //author.Id = Guid.NewGuid();
                _authorRepository.Add(author);
                await _authorRepository.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(author);
        }

        // GET: Authors/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _authorRepository.FindAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,
            [Bind("FirstName,LastName,AppUserId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")]
            Author author)
        {
            if (id != author.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _authorRepository.Update(author);
                await _authorRepository.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }

            return View(author);
        }

        // GET: Authors/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _authorRepository.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var author = _authorRepository.Remove(id);
            await _authorRepository.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}