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
    public class PizzaFinalController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PizzaFinalController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/PizzaFinal
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PizzaFinal>>> GetPizzaFinals()
        {
            return await _context.PizzaFinals.ToListAsync();
        }

        // GET: api/PizzaFinal/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PizzaFinal>> GetPizzaFinal(Guid id)
        {
            var pizzaFinal = await _context.PizzaFinals.FindAsync(id);

            if (pizzaFinal == null)
            {
                return NotFound();
            }

            return pizzaFinal;
        }

        // PUT: api/PizzaFinal/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPizzaFinal(Guid id, PizzaFinal pizzaFinal)
        {
            if (id != pizzaFinal.Id)
            {
                return BadRequest();
            }

            _context.Entry(pizzaFinal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PizzaFinalExists(id))
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

        // POST: api/PizzaFinal
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<PizzaFinal>> PostPizzaFinal(PizzaFinal pizzaFinal)
        {
            _context.PizzaFinals.Add(pizzaFinal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPizzaFinal", new { id = pizzaFinal.Id }, pizzaFinal);
        }

        // DELETE: api/PizzaFinal/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PizzaFinal>> DeletePizzaFinal(Guid id)
        {
            var pizzaFinal = await _context.PizzaFinals.FindAsync(id);
            if (pizzaFinal == null)
            {
                return NotFound();
            }

            _context.PizzaFinals.Remove(pizzaFinal);
            await _context.SaveChangesAsync();

            return pizzaFinal;
        }

        private bool PizzaFinalExists(Guid id)
        {
            return _context.PizzaFinals.Any(e => e.Id == id);
        }
    }
}
