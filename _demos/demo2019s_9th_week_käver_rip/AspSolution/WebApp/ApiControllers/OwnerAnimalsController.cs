using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using PublicApi.DTO.v1;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OwnerAnimalsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IAppUnitOfWork _uow;

        public OwnerAnimalsController(AppDbContext context, IAppUnitOfWork uow)
        {
            _context = context;
            _uow = uow;
        }

        // GET: api/OwnerAnimals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OwnerAnimalDTO>>> GetOwnerAnimals()
        {
            return Ok(await _uow.OwnerAnimals.DTOAllAsync(User.UserGuidId()));
        }

        // GET: api/OwnerAnimals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OwnerAnimal>> GetOwnerAnimal(Guid id)
        {
            var ownerAnimal = await _context.OwnerAnimals.FindAsync(id);

            if (ownerAnimal == null)
            {
                return NotFound();
            }

            return ownerAnimal;
        }

        // PUT: api/OwnerAnimals/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOwnerAnimal(Guid id, OwnerAnimal ownerAnimal)
        {
            if (id != ownerAnimal.Id)
            {
                return BadRequest();
            }

            _context.Entry(ownerAnimal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OwnerAnimalExists(id))
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

        // POST: api/OwnerAnimals
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<OwnerAnimal>> PostOwnerAnimal(OwnerAnimal ownerAnimal)
        {
            _context.OwnerAnimals.Add(ownerAnimal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOwnerAnimal", new { id = ownerAnimal.Id }, ownerAnimal);
        }

        // DELETE: api/OwnerAnimals/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<OwnerAnimal>> DeleteOwnerAnimal(Guid id)
        {
            var ownerAnimal = await _context.OwnerAnimals.FindAsync(id);
            if (ownerAnimal == null)
            {
                return NotFound();
            }

            _context.OwnerAnimals.Remove(ownerAnimal);
            await _context.SaveChangesAsync();

            return ownerAnimal;
        }

        private bool OwnerAnimalExists(Guid id)
        {
            return _context.OwnerAnimals.Any(e => e.Id == id);
        }
    }
}
