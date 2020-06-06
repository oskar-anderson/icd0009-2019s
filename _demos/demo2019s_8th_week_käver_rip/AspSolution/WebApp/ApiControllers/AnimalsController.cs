using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;
using PublicApi.DTO.v1;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AnimalsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Animals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnimalDTO>>> GetAnimals()
        {
            return await _context.Animals.Select(a => new AnimalDTO()
            {
                Id = a.Id, AnimalName = a.AnimalName, OwnerCount = a.Owners.Count
            }).ToListAsync();
        }

        // GET: api/Animals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AnimalDTO>> GetAnimal(Guid id)
        {
            var animal = await _context.Animals.Select(a => new AnimalDTO()
            {
                Id = a.Id, AnimalName = a.AnimalName, OwnerCount = a.Owners.Count
            }).FirstOrDefaultAsync(o => o.Id == id);

            if (animal == null)
            {
                return NotFound();
            }

            return animal;
        }

        // PUT: api/Animals/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnimal(Guid id, Animal animal)
        {
            if (id != animal.Id)
            {
                return BadRequest();
            }

            _context.Entry(animal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnimalExists(id))
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

        // POST: api/Animals
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Animal>> PostAnimal(Animal animal)
        {
            _context.Animals.Add(animal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAnimal", new {id = animal.Id}, animal);
        }

        // DELETE: api/Animals/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Animal>> DeleteAnimal(Guid id)
        {
            var animal = await _context.Animals.FindAsync(id);
            if (animal == null)
            {
                return NotFound();
            }

            _context.Animals.Remove(animal);
            await _context.SaveChangesAsync();

            return animal;
        }

        private bool AnimalExists(Guid id)
        {
            return _context.Animals.Any(e => e.Id == id);
        }
    }
}