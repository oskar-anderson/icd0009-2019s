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
    public class PizzaComponentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PizzaComponentController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/PizzaComponent
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PizzaComponent>>> GetPizzaComponents()
        {
            return await _context.PizzaComponents.ToListAsync();
        }

        // GET: api/PizzaComponent/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PizzaComponent>> GetPizzaComponent(Guid id)
        {
            var pizzaComponent = await _context.PizzaComponents.FindAsync(id);

            if (pizzaComponent == null)
            {
                return NotFound();
            }

            return pizzaComponent;
        }

        // PUT: api/PizzaComponent/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPizzaComponent(Guid id, PizzaComponent pizzaComponent)
        {
            if (id != pizzaComponent.Id)
            {
                return BadRequest();
            }

            _context.Entry(pizzaComponent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PizzaComponentExists(id))
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

        // POST: api/PizzaComponent
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<PizzaComponent>> PostPizzaComponent(PizzaComponent pizzaComponent)
        {
            _context.PizzaComponents.Add(pizzaComponent);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPizzaComponent", new { id = pizzaComponent.Id }, pizzaComponent);
        }

        // DELETE: api/PizzaComponent/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PizzaComponent>> DeletePizzaComponent(Guid id)
        {
            var pizzaComponent = await _context.PizzaComponents.FindAsync(id);
            if (pizzaComponent == null)
            {
                return NotFound();
            }

            _context.PizzaComponents.Remove(pizzaComponent);
            await _context.SaveChangesAsync();

            return pizzaComponent;
        }

        private bool PizzaComponentExists(Guid id)
        {
            return _context.PizzaComponents.Any(e => e.Id == id);
        }
    }
}
