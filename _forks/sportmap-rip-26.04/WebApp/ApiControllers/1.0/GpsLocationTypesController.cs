using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PublicApi.DTO.v1.Mappers;
using V1DTO=PublicApi.DTO.v1;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// GPS location types - Location, WayPoint, CheckPoint
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class GpsLocationTypesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly GpsLocationTypeMapper _mapper = new GpsLocationTypeMapper();
        
        /// <summary>
        /// Constructor
        /// </summary>
        public GpsLocationTypesController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get the predefined GpsLocationType collection.
        /// Regular location update, WayPoint, CheckPoint
        /// </summary>
        /// <returns>List of available GpsLocationTypes</returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.GpsLocationType>))]
        public async Task<ActionResult<IEnumerable<V1DTO.GpsLocationType>>> GetLocationTypes()
        {
            return Ok((await _bll.LocationTypes.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        /// <summary>
        /// Get single GpsLocationType
        /// </summary>
        /// <param name="id">GpsLocationType Id</param>
        /// <returns>request GpsLocationType</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.GpsLocationType))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.GpsLocationType>> GetGpsLocationType(Guid id)
        {
            var gpsLocationType = await _bll.LocationTypes.FirstOrDefaultAsync(id);

            if (gpsLocationType == null)
            {
                return NotFound(new V1DTO.MessageDTO("GpsLocationType not found"));
            }

            return Ok(_mapper.Map(gpsLocationType));
        }

        /// <summary>
        /// Update the GpsLocationType
        /// </summary>
        /// <param name="id">GpsLocationType id</param>
        /// <param name="gpsLocationType">GpsLocationType object</param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutGpsLocationType(Guid id, V1DTO.GpsLocationType gpsLocationType)
        {
            if (id != gpsLocationType.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and gpsLocationType.id do not match"));
            }

            await _bll.LocationTypes.UpdateAsync(_mapper.Map(gpsLocationType));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create a new GpsLocationType
        /// </summary>
        /// <param name="gpsLocationType">GpsLocationType object</param>
        /// <returns>created GpsLocationType object</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.GpsLocationType))]
        public async Task<ActionResult<V1DTO.GpsLocationType>> PostGpsLocationType(
            V1DTO.GpsLocationType gpsLocationType)
        {
            var bllEntity = _mapper.Map(gpsLocationType);
            _bll.LocationTypes.Add(bllEntity);
            await _bll.SaveChangesAsync();
            gpsLocationType.Id = bllEntity.Id;

            // FIXME - how to get back the object just now added to database.....
            // we need this: gpsLocationType = _uow.GetUpdatedEntity(gpsLocationType);
            return CreatedAtAction("GetGpsLocationType",
                new {id = gpsLocationType.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                gpsLocationType);
        }

        /// <summary>
        /// Delete the GpsLocationType
        /// </summary>
        /// <param name="id">GpsLocationType Id</param>
        /// <returns>deleted GpsLocationType object</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.GpsLocationType))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.GpsLocationType>> DeleteGpsLocationType(Guid id)
        {
            var gpsLocationType = await _bll.LocationTypes.FirstOrDefaultAsync(id);
            if (gpsLocationType == null)
            {
                return NotFound(new V1DTO.MessageDTO("GpsLocationType not found"));
            }

            await _bll.LocationTypes.RemoveAsync(gpsLocationType);
            await _bll.SaveChangesAsync();

            return Ok(gpsLocationType);
        }
    }
}