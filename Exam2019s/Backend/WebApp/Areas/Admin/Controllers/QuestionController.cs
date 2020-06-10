using System;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class QuestionController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public QuestionController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Question
        public async Task<IActionResult> Index()
        {
            var questions = await _uow.Questions.GetAllForViewAsync();
            return View(questions);
        }

        // GET: Question/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _uow.Questions.FirstOrDefaultViewAsync(id.Value);

            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        // GET: Question/Create
        public async Task<IActionResult> Create()
        {
            ViewData["QuizId"] = new SelectList(await _uow.Quizzes.GetAllAsyncBase(), "Id", "Name");
            return View();
        }

        // POST: Question/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DAL.App.DTO.Question question)
        {
            if (ModelState.IsValid)
            {
                _uow.Questions.Add(question);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["QuizId"] = new SelectList(await _uow.Quizzes.GetAllAsyncBase(), "Id", "Name", question.QuizId);
            return View(question);
        }

        // GET: Question/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _uow.Questions.FirstOrDefaultViewAsync(id.Value);

            if (question == null)
            {
                return NotFound();
            }
            ViewData["QuizId"] = new SelectList(await _uow.Quizzes.GetAllAsyncBase(), "Id", "Name", question.QuizId);
            return View(question);
        }

        // POST: Question/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, DAL.App.DTO.Question question)
        {
            if (id != question.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _uow.Questions.UpdateAsync(question);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["QuizId"] = new SelectList(await _uow.Quizzes.GetAllAsyncBase(), "Id", "Name", question.QuizId);
            return View(question);
        }

        // GET: Question/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _uow.Questions.FirstOrDefaultViewAsync(id.Value);

            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        // POST: Question/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.Questions.RemoveAsync(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
