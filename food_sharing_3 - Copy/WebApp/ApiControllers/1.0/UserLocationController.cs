using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Domain.Base.App.EF;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;
using V1DTO = PublicApi.DTO.v1;

namespace WebApp.ApiControllers._1._0
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserLocationController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly UserLocationMapper _mapper = new UserLocationMapper();

        public UserLocationController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/UserLocation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<V1DTO.UserLocationDTO>>> GetUserLocations()
        {
            return Ok(await _bll.UserLocations.GetAllAsyncBase(User.UserId()));
        }

        // GET: api/UserLocation/5
        [HttpGet("{id}")]
        public async Task<ActionResult<V1DTO.UserLocationDTO>> GetUserLocation(Guid id)
        {
            var userLocation = await _bll.UserLocations.FirstOrDefaultAsync(id, User.UserId());
            
            if (userLocation == null)
            {
                return NotFound(new V1DTO.MessageDTO($"GetUserLocation with id {id} not found"));
            }

            return Ok(userLocation);
        }

        // PUT: api/UserLocation/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserLocation(Guid id, UserLocationDTO userLocation)
        {
            userLocation.AppUserId = User.UserId();

            if (id != userLocation.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and userLocation.id do not match"));
            }

            await _bll.UserLocations.UpdateAsync(_mapper.Map(userLocation), User.UserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/UserLocation
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<V1DTO.UserLocationDTO>> PostUserLocation(UserLocationDTO userLocation)
        {
            var bllEntity = _mapper.Map(userLocation);
            _bll.UserLocations.Add(bllEntity);
            await _bll.SaveChangesAsync();
            userLocation.Id = bllEntity.Id;

            return CreatedAtAction("GetUserLocation", new { id = userLocation.Id }, userLocation);
        }

        // DELETE: api/UserLocation/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<V1DTO.UserLocationDTO>> DeleteUserLocation(Guid id)
        {
            var userLocation = await _bll.UserLocations.FirstOrDefaultAsync(id, User.UserId());
            if (userLocation == null)
            {
                return NotFound();
            }

            await _bll.UserLocations.RemoveAsync(userLocation);
            await _bll.SaveChangesAsync();

            return Ok(userLocation);
        }
    }
}
