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
    public class OwnersController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public OwnersController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/Owners
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OwnerDTO>>> GetOwners()
        {
            Console.WriteLine(User.UserGuidId());
            var ownerDTOs = await _uow.Owners.DTOAllAsync(User.UserGuidId());
            
            return Ok(ownerDTOs);
        }

        // GET: api/Owners/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OwnerDTO>> GetOwner(Guid id)
        {
            var owner = await _uow.Owners.DTOFirstOrDefaultAsync(id, User.UserGuidId());

            if (owner == null)
            {
                return NotFound();
            }

            return Ok(owner);
        }

        // PUT: api/Owners/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOwner(Guid id, OwnerEditDTO OwnerEditDTO)
        {
            if (id != OwnerEditDTO.Id)
            {
                return BadRequest();
            }

            var owner = await _uow.Owners.FirstOrDefaultAsync(OwnerEditDTO.Id, User.UserGuidId());
            if (owner == null)
            {
                return BadRequest();
            }

            owner.FirstName = OwnerEditDTO.FirstName;
            owner.LastName = OwnerEditDTO.LastName;
            
            _uow.Owners.Update(owner);


            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _uow.Owners.ExistsAsync(id, User.UserGuidId()))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: api/Owners
        [HttpPost]
        public async Task<ActionResult<Owner>> PostOwner(OwnerCreateDTO ownerCreateDTO)
        {
            var owner = new Owner
            {
                AppUserId = User.UserGuidId(),
                FirstName = ownerCreateDTO.FirstName,
                LastName = ownerCreateDTO.LastName
            };

            _uow.Owners.Add(owner);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetOwner", new {id = owner.Id}, owner);
        }

        // DELETE: api/Owners/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Owner>> DeleteOwner(Guid id)
        {
            Console.WriteLine(User.UserGuidId());
            var owner = await _uow.Owners.FirstOrDefaultAsync(id, User.UserGuidId());
            if (owner == null)
            {
                return NotFound();
            }

            _uow.Owners.Remove(owner);
            await _uow.SaveChangesAsync();

            return Ok(owner);
        }
    }
}