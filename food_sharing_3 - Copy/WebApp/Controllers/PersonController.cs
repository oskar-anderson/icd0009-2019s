using System;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Extensions;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    [Authorize(Roles = "User")]
    public class PersonController : Controller
    {
        private readonly IAppBLL _bll;

        public PersonController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Person
        public async Task<IActionResult> Index()
        {
            var persons = await _bll.Persons.GetAllAsyncBase(User.UserId());
            return View(persons);
        }

        // GET: Person/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _bll.Persons.FirstOrDefaultAsync(id.Value, User.UserId());
            
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: Person/Create
        public IActionResult Create()
        {
            
            return View();
        }

        // POST: Person/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BLL.App.DTO.Person person)
        {
            person.AppUserId = User.UserId();
            // AppUser appUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == person.AppUserId);
            // person.Phone = appUser.Phone;
            // person.FirstName = appUser.FirstName;
            // person.LastName = appUser.LastName;

            if (ModelState.IsValid)
            {
                _bll.Persons.Add(person);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: Person/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _bll.Persons.FirstOrDefaultAsync(id.Value, User.UserId());
            
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // POST: Person/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, BLL.App.DTO.Person person)
        {
            person.AppUserId = User.UserId();
            
            if (id != person.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _bll.Persons.UpdateAsync(person);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: Person/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _bll.Persons.FirstOrDefaultAsync(id.Value, User.UserId());
            
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: Person/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.Persons.RemoveAsync(id, User.UserId());
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
    }
}
