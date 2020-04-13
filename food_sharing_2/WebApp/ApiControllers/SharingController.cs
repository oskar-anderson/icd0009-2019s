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
    public class SharingController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SharingController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Sharing
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sharing>>> GetSharings()
        {
            return await _context.Sharings.ToListAsync();
        }

        // GET: api/Sharing/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sharing>> GetSharing(Guid id)
        {
            var sharing = await _context.Sharings.FindAsync(id);

            if (sharing == null)
            {
                return NotFound();
            }

            return sharing;
        }

        // PUT: api/Sharing/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSharing(Guid id, Sharing sharing)
        {
            if (id != sharing.Id)
            {
                return BadRequest();
            }

            _context.Entry(sharing).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SharingExists(id))
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

        // POST: api/Sharing
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Sharing>> PostSharing(Sharing sharing)
        {
            _context.Sharings.Add(sharing);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSharing", new { id = sharing.Id }, sharing);
        }

        // DELETE: api/Sharing/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Sharing>> DeleteSharing(Guid id)
        {
            var sharing = await _context.Sharings.FindAsync(id);
            if (sharing == null)
            {
                return NotFound();
            }

            _context.Sharings.Remove(sharing);
            await _context.SaveChangesAsync();

            return sharing;
        }

        private bool SharingExists(Guid id)
        {
            return _context.Sharings.Any(e => e.Id == id);
        }
    }
}
