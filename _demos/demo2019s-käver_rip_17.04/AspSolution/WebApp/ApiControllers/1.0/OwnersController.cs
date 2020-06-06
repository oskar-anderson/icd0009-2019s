using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Domain;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;
using Owner = PublicApi.DTO.v1.Owner;

namespace WebApp.ApiControllers._1._0
{
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OwnersController : ControllerBase
    {
        //private readonly IAppUnitOfWork _uow;
        private readonly IAppBLL _bll;

        public OwnersController(IAppBLL bll)
        {
            _bll = bll;
        }


        // GET: api/Owners
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Owner>>> GetOwners()
        {
            var owners = (await _bll.Owners.AllAsync(User.UserGuidId()))
                .Select(bllEntity => new Owner()
            {
                Id = bllEntity.Id,
                FirstName = bllEntity.FirstName,
                LastName = bllEntity.LastName,
                AnimalCount = bllEntity.AnimalCount,
            }) ;
            
            return Ok(owners);
        }

        /// <summary>
        ///  Find and return owner from datasource
        /// </summary>
        /// <param name="id">owner id - guid</param>
        /// <returns>Owner object based on id</returns>
        /// <response code="200">The owner was successfully retrieved.</response>
        /// <response code="404">The owner does not exist.</response>
        [ProducesResponseType( typeof( Owner ), 200 )]	
        [ProducesResponseType( 404 )]	
        [HttpGet("{id}")]
        public async Task<ActionResult<Owner>> GetOwner(Guid id)
        {
            var owner = await _bll.Owners.FirstOrDefaultAsync(id, User.UserGuidId());

            if (owner == null)
            {
                return NotFound();
            }

            return Ok(owner);
        }

        // PUT: api/Owners/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOwner(Guid id, OwnerEdit OwnerEditDTO)
        {
            if (id != OwnerEditDTO.Id)
            {
                return BadRequest();
            }

            var owner = await _bll.Owners.FirstOrDefaultAsync(OwnerEditDTO.Id, User.UserGuidId());
            if (owner == null)
            {
                return BadRequest();
            }

            owner.FirstName = OwnerEditDTO.FirstName;
            owner.LastName = OwnerEditDTO.LastName;
            
            _bll.Owners.Update(owner);


            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.Owners.ExistsAsync(id, User.UserGuidId()))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: api/Owners
        [HttpPost]
        public async Task<ActionResult<Owner>> PostOwner(OwnerCreate ownerCreateDTO)
        {
            var owner = new BLL.App.DTO.Owner
            {
                AppUserId = User.UserGuidId(),
                FirstName = ownerCreateDTO.FirstName,
                LastName = ownerCreateDTO.LastName
            };

            _bll.Owners.Add(owner);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetOwner", new {id = owner.Id}, owner);
        }

        // DELETE: api/Owners/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Owner>> DeleteOwner(Guid id)
        {
            var owner = await _bll.Owners.FirstOrDefaultAsync(id, User.UserGuidId());
            if (owner == null)
            {
                return NotFound();
            }

            _bll.Owners.Remove(owner);
            await _bll.SaveChangesAsync();

            return Ok(owner);
        }
    }
}