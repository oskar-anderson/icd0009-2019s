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
    public class ResultController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public ResultController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Result
        public async Task<IActionResult> Index()
        {
            var results = await _uow.Results.GetAllForViewAsync();
            return View(results);
        }

        // GET: Result/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _uow.Results.FirstOrDefaultViewAsync(id.Value);

            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        // GET: Result/Create
        public async Task<IActionResult> Create()
        {
            ViewData["QuizId"] = new SelectList(await _uow.Quizzes.GetAllAsyncBase(), "Id", "Name");
            return View();
        }

        // POST: Result/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DAL.App.DTO.Result result)
        {
            if (ModelState.IsValid)
            {
                _uow.Results.Add(result);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["QuizId"] = new SelectList(await _uow.Quizzes.GetAllAsyncBase(), "Id", "Name", result.QuizId);
            return View(result);
        }

        // GET: Result/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _uow.Results.FirstOrDefaultViewAsync(id.Value);

            if (result == null)
            {
                return NotFound();
            }
            ViewData["QuizId"] = new SelectList(await _uow.Quizzes.GetAllAsyncBase(), "Id", "Name", result.QuizId);
            return View(result);
        }

        // POST: Result/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, DAL.App.DTO.Result result)
        {
            if (id != result.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _uow.Results.UpdateAsync(result);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["QuizId"] = new SelectList(await _uow.Quizzes.GetAllAsyncBase(), "Id", "Name", result.QuizId);
            return View(result);
        }

        // GET: Result/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _uow.Results.FirstOrDefaultViewAsync(id.Value);

            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        // POST: Result/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.Results.RemoveAsync(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
