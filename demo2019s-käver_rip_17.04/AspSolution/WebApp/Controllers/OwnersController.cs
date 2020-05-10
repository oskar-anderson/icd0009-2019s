using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.App;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Extensions;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    [Authorize]
    public class OwnersController : Controller
    {
        private readonly IAppBLL _bll;

        public OwnersController(IAppBLL bll)
        {
            _bll = bll;
        }


        // GET: Owners
        public async Task<IActionResult> Index()
        {
            var owners = await _bll.Owners.AllAsync(User.UserGuidId());

            return View(owners);
        }


        // GET: Owners/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _bll.Owners.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (owner == null)
            {
                return NotFound();
            }

            return View(owner);
        }

        // GET: Owners/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Owners/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            BLL.App.DTO.Owner owner)
        {
            owner.AppUserId = User.UserGuidId();

            if (ModelState.IsValid)
            {
                //owner.Id = Guid.NewGuid();
                _bll.Owners.Add(owner);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(owner);
        }

        // GET: Owners/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _bll.Owners.FirstOrDefaultAsync(id.Value, User.UserGuidId());

            if (owner == null)
            {
                return NotFound();
            }

            return View(owner);
        }

        // POST: Owners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Owner owner)
        {

            /*
            var x = await _bll.Owners.FindAsync(id); // or use AsNoTracking
            x.FirstName = "kalamaja";
            _bll.Owners.Update(x); // this will not work! x is copy of copy of domain object
            await _bll.SaveChangesAsync();
            */
            
            owner.AppUserId = User.UserGuidId();

            if (id != owner.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bll.Owners.Update(owner);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await _bll.Owners.ExistsAsync(owner.Id, User.UserGuidId()))
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

            return View(owner);
        }

        // GET: Owners/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _bll.Owners.FirstOrDefaultAsync(id.Value, User.UserGuidId());

            if (owner == null)
            {
                return NotFound();
            }

            return View(owner);
        }

        // POST: Owners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.Owners.DeleteAsync(id, User.UserGuidId());
            await _bll.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }


    }
}