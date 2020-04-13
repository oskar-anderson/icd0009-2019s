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
    public class OwnersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OwnersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Owners
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OwnerDTO>>> GetOwners()
        {
            return await _context.Owners.Select(o => new OwnerDTO()
                {
                    Id = o.Id, FirstName = o.FirstName, LastName = o.LastName, AnimalCount = o.Animals.Count
                })
                .ToListAsync();
        }

        // GET: api/Owners/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OwnerDTO>> GetOwner(Guid id)
        {
            var owner = await _context.Owners
                .Select(o => new OwnerDTO()
                {
                    Id = o.Id, FirstName = o.FirstName, LastName = o.LastName, AnimalCount = o.Animals.Count
                }).FirstOrDefaultAsync(o => o.Id == id);

            if (owner == null)
            {
                return NotFound();
            }

            return owner;
        }

        // PUT: api/Owners/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOwner(Guid id, Owner owner)
        {
            if (id != owner.Id)
            {
                return BadRequest();
            }

            _context.Entry(owner).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OwnerExists(id))
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

        // POST: api/Owners
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Owner>> PostOwner(OwnerCreateDTO ownerCreateDTO)
        {
            var owner = new Owner()
            {
                FirstName = ownerCreateDTO.FirstName,
                LastName = ownerCreateDTO.LastName,
            };
            _context.Owners.Add(owner);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOwner", new {id = owner.Id}, owner);
        }

        // DELETE: api/Owners/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Owner>> DeleteOwner(Guid id)
        {
            var owner = await _context.Owners.FindAsync(id);
            if (owner == null)
            {
                return NotFound();
            }

            _context.Owners.Remove(owner);
            await _context.SaveChangesAsync();

            return owner;
        }

        private bool OwnerExists(Guid id)
        {
            return _context.Owners.Any(e => e.Id == id);
        }
    }
}