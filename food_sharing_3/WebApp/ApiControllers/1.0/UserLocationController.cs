using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Domain.Base.App.EF;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;
using V1DTO = PublicApi.DTO.v1;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// UserLocation Api Controller
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class UserLocationController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly UserLocationMapper _mapper = new UserLocationMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public UserLocationController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get a list of all the UserLocations
        /// </summary>
        /// <returns>List of UserLocation</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.UserLocationDTO>))]
        public async Task<ActionResult<IEnumerable<V1DTO.UserLocationDTO>>> GetUserLocations()
        {
            return Ok((await _bll.UserLocations.GetAllForApiAsync(User.UserId())).Select(e => _mapper.MapUserLocationView(e)));
        }

        /// <summary>
        /// Get single UserLocation
        /// </summary>
        /// <param name="id">UserLocation Id</param>
        /// <returns>request UserLocation</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.UserLocationDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.UserLocationDTO>> GetUserLocation(Guid id)
        {
            var userLocation = await _bll.UserLocations.FirstOrDefaultApiAsync(id, User.UserId());
            
            if (userLocation == null)
            {
                return NotFound(new V1DTO.MessageDTO($"GetUserLocation with id {id} not found"));
            }

            return Ok(_mapper.MapUserLocationView(userLocation));
        }

        /// <summary>
        /// Update the UserLocation
        /// </summary>
        /// <param name="id">UserLocation id</param>
        /// <param name="userLocation">UserLocation object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
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

        /// <summary>
        /// Create a new UserLocation
        /// </summary>
        /// <param name="userLocation">UserLocation object</param>
        /// <returns>created UserLocation object</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.UserLocationDTO))]
        public async Task<ActionResult<V1DTO.UserLocationDTO>> PostUserLocation(UserLocationDTO userLocation)
        {
            userLocation.AppUserId = User.UserId();
            
            var bllEntity = _mapper.Map(userLocation);
            _bll.UserLocations.Add(bllEntity);
            await _bll.SaveChangesAsync();
            userLocation.Id = bllEntity.Id;

            return CreatedAtAction("GetUserLocation", 
                new { id = userLocation.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"}, 
                userLocation);
        }

        /// <summary>
        /// Delete the UserLocation
        /// </summary>
        /// <param name="id">UserLocation Id</param>
        /// <returns>deleted UserLocation object</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.UserLocationDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.UserLocationDTO>> DeleteUserLocation(Guid id)
        {
            var userLocation = await _bll.UserLocations.FirstOrDefaultApiAsync(id, User.UserId());
            if (userLocation == null)
            {
                return NotFound(new {message = "UserLocation not found"});
            }

            await _bll.UserLocations.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(userLocation);
        }
    }
}
