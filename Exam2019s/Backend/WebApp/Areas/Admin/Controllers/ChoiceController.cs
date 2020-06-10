using System;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PublicApi.DTO.v1.Mappers;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChoiceController : Controller
    {
        private readonly IAppUnitOfWork _uow;
        private readonly ChoiceMapper _mapper = new ChoiceMapper();

        public ChoiceController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Choice
        public async Task<IActionResult> Index()
        {
            var choices = await _uow.Choices.GetAllForViewAsync();
            return View(choices);
        }

        // GET: Choice/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var choice = await _uow.Choices.FirstOrDefaultViewAsync(id.Value);

            if (choice == null)
            {
                return NotFound();
            }

            return View(choice);
        }

        // GET: Choice/Create
        public async Task<IActionResult> Create()
        {
            ViewData["QuestionId"] = new SelectList(await _uow.Questions.GetAllAsyncBase(), "Id", "QuestionName");
            return View();
        }

        // POST: Choice/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DAL.App.DTO.Choice choice)
        {
            if (ModelState.IsValid)
            {
                _uow.Choices.Add(choice);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["QuestionId"] = new SelectList(await _uow.Questions.GetAllAsyncBase(), "Id", "QuestionName", choice.QuestionId);
            return View(choice);
        }

        // GET: Choice/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var choice = await _uow.Choices.FirstOrDefaultViewAsync(id.Value);

            if (choice == null)
            {
                return NotFound();
            }
            ViewData["QuestionId"] = new SelectList(await _uow.Questions.GetAllAsyncBase(), "Id", "QuestionName", choice.Question);
            return View(choice);
        }

        // POST: Choice/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, DAL.App.DTO.Choice choice)
        {
            if (id != choice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _uow.Choices.UpdateAsync(choice);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["QuestionId"] = new SelectList(await _uow.Questions.GetAllAsyncBase(), "Id", "QuestionName", choice.Question);
            return View(choice);
        }

        // GET: Choice/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var choice = await _uow.Choices.FirstOrDefaultViewAsync(id.Value);

            if (choice == null)
            {
                return NotFound();
            }

            return View(choice);
        }

        // POST: Choice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.Choices.RemoveAsync(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
