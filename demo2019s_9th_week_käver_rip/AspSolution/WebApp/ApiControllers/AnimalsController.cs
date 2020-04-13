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
    public class AnimalsController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public AnimalsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/Animals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnimalDTO>>> GetAnimals()
        {
            return Ok(await _uow.Animals.DTOAllAsync(User.UserGuidId()));
        }

        // GET: api/Animals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AnimalDTO>> GetAnimal(Guid id)
        {
            var animalDTO = await _uow.Animals.DTOFirstOrDefaultAsync(id, User.UserGuidId());

            if (animalDTO == null)
            {
                return NotFound();
            }

            return Ok(animalDTO);
        }

        // PUT: api/Animals/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnimal(Guid id, AnimalEditDTO animalEditDTO)
        {
            if (id != animalEditDTO.Id)
            {
                return BadRequest();
            }

            var animal = await _uow.Animals.FirstOrDefaultAsync(animalEditDTO.Id, User.UserGuidId());
            if (animal == null)
            {
                return BadRequest();
            }
            animal.AnimalName = animalEditDTO.AnimalName;
            animal.BirthYear = animalEditDTO.BirthYear;
            
            _uow.Animals.Update(animal);
            
            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _uow.Animals.ExistsAsync(id, User.UserGuidId()))
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
        public async Task<ActionResult<AnimalEditDTO>> PostAnimal(AnimalCreateDTO animalCreateDTO)
        {
            
            var animal = new Animal();
            animal.AnimalName = animalCreateDTO.AnimalName;
            animal.BirthYear = animalCreateDTO.BirthYear;
            animal.AppUserId = User.UserGuidId();

            _uow.Animals.Add(animal);
           
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetAnimal", new { id = animal.Id }, animal);
        }

        // DELETE: api/Animals/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Animal>> DeleteAnimal(Guid id)
        {
            var animal = await _uow.Animals.FirstOrDefaultAsync(id, User.UserGuidId());
            if (animal == null)
            {
                return NotFound();
            }

            _uow.Animals.Remove(animal);
            await _uow.SaveChangesAsync();
            return Ok(animal);
        }

    }
}
