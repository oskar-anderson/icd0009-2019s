using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.App;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PublicApi.DTO.v1.Mappers;
using V1DTO = PublicApi.DTO.v1;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// GPS locations received from gps handheld-s
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class GpsLocationsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly GpsLocationMapper _mapper = new GpsLocationMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public GpsLocationsController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get gps locations for single session 
        /// </summary>
        /// <param name="gpsSessionId">Get locations for this specific session</param>
        /// <returns>GpsLocations for session</returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.GpsLocation>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<IEnumerable<V1DTO.GpsLocation>>> GetGpsLocations(Guid? gpsSessionId)
        {
            if (gpsSessionId == null)
            {
                return BadRequest(new V1DTO.MessageDTO("GpsSessionId cannot be null"));
            }

            if (!await _bll.GpsSessions.ExistsAsync(gpsSessionId.Value))
            {
                return NotFound(new V1DTO.MessageDTO($"GpsSession with id {gpsSessionId} was not found"));
            }

            return Ok((await _bll.GpsLocations.GetAllAsync(gpsSessionId.Value)).Select(e => _mapper.Map(e)));
        }

        /// <summary>
        /// Get a single Gps location
        /// </summary>
        /// <param name="id">id for GpsLocation</param>
        /// <returns>GpsLocation</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.GpsLocation))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.GpsLocation>> GetGpsLocation(Guid id)
        {
            var gpsLocation = await _bll.GpsLocations.FirstOrDefaultAsync(id);

            if (gpsLocation == null)
            {
                return NotFound(new V1DTO.MessageDTO($"GpsLocation with id {id} not found"));
            }

            return Ok(_mapper.Map(gpsLocation));
        }

        /// <summary>
        /// Update gps location
        /// </summary>
        /// <param name="id"></param>
        /// <param name="gpsLocation"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGpsLocation(Guid id, V1DTO.GpsLocation gpsLocation)
        {
            gpsLocation.AppUserId = User.UserId();

            if (id != gpsLocation.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and gpsLocation.id do not match"));
            }

            await _bll.GpsLocations.UpdateAsync(_mapper.Map(gpsLocation), User.UserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }


        /// <summary>
        /// Create/add a new GpsLocation
        /// </summary>
        /// <param name="gpsLocation">Location info</param>
        /// <returns></returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.GpsLocation))]
        [HttpPost]
        public async Task<ActionResult<V1DTO.GpsLocation>> PostGpsLocation(V1DTO.GpsLocation gpsLocation)
        {
            gpsLocation.AppUserId = User.UserId();
            if (gpsLocation.RecordedAt == DateTime.MinValue)
            {
                gpsLocation.RecordedAt = DateTime.Now;
            }

            var bllEntity = _mapper.Map(gpsLocation);
            await _bll.GpsLocations.AddAndUpdateSessionAsync(bllEntity);
            await _bll.SaveChangesAsync();
            gpsLocation.Id = bllEntity.Id;

            return CreatedAtAction("GetGpsLocation",
                new {id = gpsLocation.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                gpsLocation);
        }


        /// <summary>
        /// Create/add a GpsLocation collection
        /// </summary>
        /// <param name="gpsSessionId">Id of session where to add the locations</param>
        /// <param name="gpsLocations">Collection of Location info-s</param>
        /// <returns></returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.GpsLocationCreateBulkResponse))]
        [HttpPost("bulkupload/{gpsSessionId}")]
        public async Task<ActionResult<V1DTO.GpsLocation>> PostBulkGpsLocation(Guid gpsSessionId,
            V1DTO.GpsLocationCreate[] gpsLocations)
        {
            foreach (var gpsLocation in gpsLocations)
            {
                if (gpsLocation.RecordedAt == DateTime.MinValue)
                {
                    continue;
                }

                var bllGpsLocation = new GpsLocation()
                {
                    RecordedAt = gpsLocation.RecordedAt,
                    AppUserId = User.UserId(),
                    Latitude = gpsLocation.Latitude,
                    Longitude = gpsLocation.Longitude,
                    Accuracy = gpsLocation.Accuracy,
                    Altitude = gpsLocation.Altitude,
                    VerticalAccuracy = gpsLocation.VerticalAccuracy,
                    GpsSessionId = gpsSessionId,
                    GpsLocationTypeId = gpsLocation.GpsLocationTypeId
                };
                _bll.GpsLocations.Add(bllGpsLocation);
            }

            var resultCount = await _bll.SaveChangesAsync();
            return Ok(new V1DTO.GpsLocationCreateBulkResponse() {LocationsAdded = resultCount, LocationsReceived = gpsLocations.Length, GpsSessionId = gpsSessionId});
        }


        /// <summary>
        /// Deletes the GpsLocation
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.GpsLocation))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [HttpDelete("{id}")]
        public async Task<ActionResult<V1DTO.GpsLocation>> DeleteGpsLocation(Guid id)
        {
            var gpsLocation = await _bll.GpsLocations.FirstOrDefaultAsync(id, User.UserId());
            if (gpsLocation == null)
            {
                return NotFound(new V1DTO.MessageDTO($"GpsLocation with id {id} not found"));
            }

            await _bll.GpsLocations.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(gpsLocation);
        }
    }
}