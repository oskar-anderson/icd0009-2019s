using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.App.EF;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.ApiControllers._1._0
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}[controller]")]
    [ApiController]
    public class PizzaTemplateController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PizzaTemplateController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/PizzaTemplate
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PizzaTemplate>>> GetPizzaTemplates()
        {
            return await _context.PizzaTemplates.ToListAsync();
        }

        // GET: api/PizzaTemplate/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PizzaTemplate>> GetPizzaTemplate(Guid id)
        {
            var pizzaTemplate = await _context.PizzaTemplates.FindAsync(id);

            if (pizzaTemplate == null)
            {
                return NotFound();
            }

            return pizzaTemplate;
        }

        // PUT: api/PizzaTemplate/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPizzaTemplate(Guid id, PizzaTemplate pizzaTemplate)
        {
            if (id != pizzaTemplate.Id)
            {
                return BadRequest();
            }

            _context.Entry(pizzaTemplate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PizzaTemplateExists(id))
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

        // POST: api/PizzaTemplate
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<PizzaTemplate>> PostPizzaTemplate(PizzaTemplate pizzaTemplate)
        {
            _context.PizzaTemplates.Add(pizzaTemplate);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPizzaTemplate", new { id = pizzaTemplate.Id }, pizzaTemplate);
        }

        // DELETE: api/PizzaTemplate/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PizzaTemplate>> DeletePizzaTemplate(Guid id)
        {
            var pizzaTemplate = await _context.PizzaTemplates.FindAsync(id);
            if (pizzaTemplate == null)
            {
                return NotFound();
            }

            _context.PizzaTemplates.Remove(pizzaTemplate);
            await _context.SaveChangesAsync();

            return pizzaTemplate;
        }

        private bool PizzaTemplateExists(Guid id)
        {
            return _context.PizzaTemplates.Any(e => e.Id == id);
        }
    }
}
