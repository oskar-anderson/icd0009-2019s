using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SharingItemController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SharingItemController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/SharingItem
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SharingItem>>> GetSharingItems()
        {
            return await _context.SharingItems.ToListAsync();
        }

        // GET: api/SharingItem/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SharingItem>> GetSharingItem(Guid id)
        {
            var sharingItem = await _context.SharingItems.FindAsync(id);

            if (sharingItem == null)
            {
                return NotFound();
            }

            return sharingItem;
        }

        // PUT: api/SharingItem/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSharingItem(Guid id, SharingItem sharingItem)
        {
            if (id != sharingItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(sharingItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SharingItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/SharingItem
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<SharingItem>> PostSharingItem(SharingItem sharingItem)
        {
            _context.SharingItems.Add(sharingItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSharingItem", new { id = sharingItem.Id }, sharingItem);
        }

        // DELETE: api/SharingItem/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SharingItem>> DeleteSharingItem(Guid id)
        {
            var sharingItem = await _context.SharingItems.FindAsync(id);
            if (sharingItem == null)
            {
                return NotFound();
            }

            _context.SharingItems.Remove(sharingItem);
            await _context.SaveChangesAsync();

            return sharingItem;
        }

        private bool SharingItemExists(Guid id)
        {
            return _context.SharingItems.Any(e => e.Id == id);
        }
    }
}
