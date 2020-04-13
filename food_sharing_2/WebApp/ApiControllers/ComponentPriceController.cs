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
    public class ComponentPriceController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ComponentPriceController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ComponentPrice
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComponentPrice>>> GetComponentPrices()
        {
            return await _context.ComponentPrices.ToListAsync();
        }

        // GET: api/ComponentPrice/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ComponentPrice>> GetComponentPrice(Guid id)
        {
            var componentPrice = await _context.ComponentPrices.FindAsync(id);

            if (componentPrice == null)
            {
                return NotFound();
            }

            return componentPrice;
        }

        // PUT: api/ComponentPrice/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComponentPrice(Guid id, ComponentPrice componentPrice)
        {
            if (id != componentPrice.Id)
            {
                return BadRequest();
            }

            _context.Entry(componentPrice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComponentPriceExists(id))
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

        // POST: api/ComponentPrice
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<ComponentPrice>> PostComponentPrice(ComponentPrice componentPrice)
        {
            _context.ComponentPrices.Add(componentPrice);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComponentPrice", new { id = componentPrice.Id }, componentPrice);
        }

        // DELETE: api/ComponentPrice/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ComponentPrice>> DeleteComponentPrice(Guid id)
        {
            var componentPrice = await _context.ComponentPrices.FindAsync(id);
            if (componentPrice == null)
            {
                return NotFound();
            }

            _context.ComponentPrices.Remove(componentPrice);
            await _context.SaveChangesAsync();

            return componentPrice;
        }

        private bool ComponentPriceExists(Guid id)
        {
            return _context.ComponentPrices.Any(e => e.Id == id);
        }
    }
}
