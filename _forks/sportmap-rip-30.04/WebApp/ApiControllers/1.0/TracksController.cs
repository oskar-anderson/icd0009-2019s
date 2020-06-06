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
    /// Tracks Controller
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class TracksController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly TrackMapper _mapper = new TrackMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public TracksController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get all Track-s
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.Track>))]
        public async Task<ActionResult<IEnumerable<V1DTO.Track>>> GetTracks()
        {
            return Ok((await _bll.Tracks.GetAllAsyncBase()).Select(e => _mapper.Map(e)));
        }

        /// <summary>
        /// Get single Track
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Track))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.Track>> GetTrack(Guid id)
        {
            var track = await _bll.Tracks.FirstOrDefaultAsync(id);

            if (track == null)
            {
                return NotFound(new V1DTO.MessageDTO("Track not found"));
            }

            return Ok(_mapper.Map(track));
        }

        /// <summary>
        /// Update Track
        /// </summary>
        /// <param name="id"></param>
        /// <param name="track"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutTrack(Guid id, V1DTO.Track track)
        {
            if (id != track.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("Id and track.id do not match"));
            }

            if (!await _bll.Tracks.ExistsAsync(track.Id, User.UserId()))
            {
                return NotFound(new V1DTO.MessageDTO($"Current user does not have track with this id {id}"));
            }

            track.AppUserId = User.UserId();
            await _bll.Tracks.UpdateAsync(_mapper.Map(track));
            await _bll.SaveChangesAsync();
            
            return NoContent();
        }

        /// <summary>
        /// Post Track
        /// </summary>
        /// <param name="track"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.Track))]
        [HttpPost]
        public async Task<ActionResult<V1DTO.Track>> PostTrack(V1DTO.Track track)
        {
            track.AppUserId = User.UserId();


            var bllEntity = _mapper.Map(track);
            _bll.Tracks.Add(bllEntity);
            await _bll.SaveChangesAsync();
            track.Id = bllEntity.Id;
            
            return CreatedAtAction("GetTrack",
                new {id = track.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                track);
        }
        
        /// <summary>
        /// Delete Track
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Track))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [HttpDelete("{id}")]
        public async Task<ActionResult<V1DTO.Track>> DeleteTrack(Guid id)
        {
            var track = await _bll.Tracks.FirstOrDefaultAsync(id, User.UserId());
            if (track == null)
            {
                return NotFound(new V1DTO.MessageDTO($"Track with id {id} not found"));
            }

            await _bll.Tracks.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(track);
        }

    }
}