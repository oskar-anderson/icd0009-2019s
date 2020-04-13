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
    public class SizeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SizeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Size
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Size>>> GetSizes()
        {
            return await _context.Sizes.ToListAsync();
        }

        // GET: api/Size/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Size>> GetSize(Guid id)
        {
            var size = await _context.Sizes.FindAsync(id);

            if (size == null)
            {
                return NotFound();
            }

            return size;
        }

        // PUT: api/Size/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSize(Guid id, Size size)
        {
            if (id != size.Id)
            {
                return BadRequest();
            }

            _context.Entry(size).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SizeExists(id))
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

        // POST: api/Size
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Size>> PostSize(Size size)
        {
            _context.Sizes.Add(size);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSize", new { id = size.Id }, size);
        }

        // DELETE: api/Size/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Size>> DeleteSize(Guid id)
        {
            var size = await _context.Sizes.FindAsync(id);
            if (size == null)
            {
                return NotFound();
            }

            _context.Sizes.Remove(size);
            await _context.SaveChangesAsync();

            return size;
        }

        private bool SizeExists(Guid id)
        {
            return _context.Sizes.Any(e => e.Id == id);
        }
    }
}
