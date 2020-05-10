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
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1.Mappers;
using V1DTO = PublicApi.DTO.v1;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// TrackPoints Controller
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class TrackPointsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly TrackPointMapper _mapper = new TrackPointMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public TrackPointsController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get all the TrackPoints
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.Track>))]
        public async Task<ActionResult<IEnumerable<V1DTO.TrackPoint>>> GetTrackPoints()
        {
            return Ok((await _bll.TrackPoints.GetAllAsyncBase()).Select(e => _mapper.Map(e)));
        }

        /// <summary>
        /// Get single TrackPoint
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.TrackPoint))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.TrackPoint>> GetTrackPoint(Guid id)
        {
            var trackPoint = await _bll.TrackPoints.FirstOrDefaultAsync(id);

            if (trackPoint == null)
            {
                return NotFound(new V1DTO.MessageDTO("TrackPoint not found"));
            }

            return Ok(_mapper.Map(trackPoint));
        }

        /// <summary>
        /// Update TrackPoint
        /// </summary>
        /// <param name="id"></param>
        /// <param name="trackPoint"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutTrackPoint(Guid id, V1DTO.TrackPoint trackPoint)
        {
            if (id != trackPoint.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("Id and trackPoint.id do not match"));
            }

            if (!await _bll.TrackPoints.ExistsAsync(trackPoint.Id, User.UserId()))
            {
                return NotFound(new V1DTO.MessageDTO($"Current user does not have trackpoint with this id {id}"));
            }

            trackPoint.AppUserId = User.UserId();
            await _bll.TrackPoints.UpdateAsync(_mapper.Map(trackPoint));
            await _bll.SaveChangesAsync();
            
            return NoContent();
        }

        /// <summary>
        /// Create TrackPoint
        /// </summary>
        /// <param name="trackPoint"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.TrackPoint))]
        [HttpPost]
        public async Task<ActionResult<V1DTO.TrackPoint>> PostTrackPoint(V1DTO.TrackPoint trackPoint)
        {
            trackPoint.AppUserId = User.UserId();


            var bllEntity = _mapper.Map(trackPoint);
            _bll.TrackPoints.Add(bllEntity);
            await _bll.SaveChangesAsync();
            trackPoint.Id = bllEntity.Id;
            
            return CreatedAtAction("GetTrackPoint",
                new {id = trackPoint.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                trackPoint);
        }

        /// <summary>
        /// Delete TrackPoint
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.TrackPoint))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [HttpDelete("{id}")]
        public async Task<ActionResult<V1DTO.TrackPoint>> DeleteTrackPoint(Guid id)
        {
            var trackPoint = await _bll.TrackPoints.FirstOrDefaultAsync(id, User.UserId());
            if (trackPoint == null)
            {
                return NotFound(new V1DTO.MessageDTO($"TrackPoint with id {id} not found"));
            }

            await _bll.TrackPoints.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(trackPoint);
        }

    }
}