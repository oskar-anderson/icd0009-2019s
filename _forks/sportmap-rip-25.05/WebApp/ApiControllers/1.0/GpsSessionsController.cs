using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PublicApi.DTO.v1.Mappers;
using V1DTO=PublicApi.DTO.v1;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Gps sessions
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class GpsSessionsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly GpsSessionMapper _mapper = new GpsSessionMapper();
        
        /// <summary>
        /// Constructor
        /// </summary>
        public GpsSessionsController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// get all the GpsSessions
        /// </summary>
        /// <returns>Array of GpsSessions</returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.GpsSessionView>))]
        public async Task<ActionResult<IEnumerable<V1DTO.GpsSessionView>>> GetGpsSessions(int minLocationsCount = 10,
            double minDuration = 60, double minDistance = 10, DateTime? fromDateTime = null,
            DateTime? toDateTime = null)
        {
            var result = await _bll.GpsSessions.GetAllForViewAsync(minLocationsCount, minDuration, minDistance, fromDateTime, toDateTime);
            return Ok(result.Select(e => _mapper.MapGpsSessionView(e)));
        }

        /// <summary>
        /// Get a single GpsSession
        /// </summary>
        /// <param name="id">GpSession Id</param>
        /// <returns>GpsSession object</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.GpsSession))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.GpsSession>> GetGpsSession(Guid id)
        {
            var gpsSession = await _bll.GpsSessions.FirstOrDefaultAsync(id);

            if (gpsSession == null)
            {
                return NotFound(new V1DTO.MessageDTO($"gpsSession with id {id} not found"));
            }

            return Ok(_mapper.Map( gpsSession));
        }


        /// <summary>
        /// Update the GpsSession
        /// </summary>
        /// <param name="id">Session Id</param>
        /// <param name="gpsSession">gpsSession object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutGpsSession(Guid id, V1DTO.GpsSession gpsSession)
        {
            if (id != gpsSession.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("Id and gpsSession.id do not match"));
            }

            if (!await _bll.GpsSessions.ExistsAsync(gpsSession.Id, User.UserId()))
            {
                return NotFound(new V1DTO.MessageDTO($"Current user does not have session with this id {id}"));
            }

            gpsSession.AppUserId = User.UserId();
            await _bll.GpsSessions.UpdateAsync(_mapper.Map(gpsSession));
            await _bll.SaveChangesAsync();
            
            return NoContent();
        }

        /// <summary>
        /// Post the new GpsSession
        /// </summary>
        /// <param name="gpsSession"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.GpsSession))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.GpsSession>> PostGpsSession(V1DTO.GpsSession gpsSession)
        {
            gpsSession.AppUserId = User.UserId();
            if (gpsSession.RecordedAt == DateTime.MinValue)
            {
                gpsSession.RecordedAt = DateTime.Now;
            }

            if (gpsSession.PaceMin < 60)
            {
                return BadRequest(new V1DTO.MessageDTO("PaceMin must be >= 60"));
            }

            if (gpsSession.PaceMin >= gpsSession.PaceMax)
            {
                return BadRequest(new V1DTO.MessageDTO("PaceMax must be > then MinSpeed"));
            }

            if (gpsSession.GpsSessionTypeId == Guid.Empty)
            {
                gpsSession.GpsSessionTypeId = Guid.Parse("00000000-0000-0000-0000-000000000001");
            }

            var bllEntity = _mapper.Map(gpsSession);
            _bll.GpsSessions.Add(bllEntity);
            await _bll.SaveChangesAsync();
            gpsSession.Id = bllEntity.Id;

            return CreatedAtAction(nameof(GetGpsSession),
                new {id = gpsSession.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                gpsSession);
        }


        /// <summary>
        /// Delete the GpsSession. Also deletes all the GpsLocations for this session.
        /// </summary>
        /// <param name="id">Session Id to delete.</param>
        /// <returns>GpSession just deleted</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.GpsSession))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.GpsSession>> DeleteGpsSession(Guid id)
        {
            //GpsSession? gpsSession = null;

            var userIdTKey = User.IsInRole("admin") ? null : (Guid?) User.UserId();

            var gpsSession =
                await _bll.GpsSessions.FirstOrDefaultAsync(id, userIdTKey);

            
            if (gpsSession == null)
            {
                return NotFound(new V1DTO.MessageDTO($"GpsSession with id {id} not found!"));
            }

            await _bll.GpsSessions.RemoveAsync(gpsSession, userIdTKey);
            await _bll.SaveChangesAsync();

            return Ok(gpsSession);
        }


    }
}