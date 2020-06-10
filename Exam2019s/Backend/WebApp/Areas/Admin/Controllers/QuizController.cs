using System;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using DAL.App.EF;
using Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class QuizController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IAppUnitOfWork _uow;

        public QuizController(AppDbContext context, IAppUnitOfWork uow)
        {
            _context = context;
            _uow = uow;
        }

        // GET: Quiz
        public async Task<IActionResult> Index()
        {
            var quizzes = await _uow.Quizzes.GetAllForViewAsync();
            return View(quizzes);
        }

        // GET: Quiz/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quiz = await _uow.Quizzes.FirstOrDefaultViewAsync(id.Value);

            if (quiz == null)
            {
                return NotFound();
            }

            return View(quiz);
        }

        // GET: Quiz/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: Quiz/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DAL.App.DTO.Quiz quiz)
        {
            quiz.AppUserId = User.UserId();
            
            if (ModelState.IsValid)
            {
                _uow.Quizzes.Add(quiz);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(quiz);
        }

        // GET: Quiz/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quiz = await _uow.Quizzes.FirstOrDefaultViewAsync(id.Value);

            if (quiz == null)
            {
                return NotFound();
            }
            return View(quiz);
        }

        // POST: Quiz/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, DAL.App.DTO.Quiz quiz)
        {
            if (id != quiz.Id)
            {
                return NotFound();
            }
            
            quiz.AppUserId = User.UserId();

            if (ModelState.IsValid)
            {
                await _uow.Quizzes.UpdateAsync(quiz);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(quiz);
        }

        // GET: Quiz/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quiz = await _uow.Quizzes.FirstOrDefaultViewAsync(id.Value);
            
            if (quiz == null)
            {
                return NotFound();
            }

            return View(quiz);
        }

        // POST: Quiz/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.Quizzes.RemoveAsync(id, User.UserId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
