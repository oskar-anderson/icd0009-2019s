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
    /// Sharing Api Controller
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class SharingController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly SharingMapper _mapper = new SharingMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public SharingController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get a list of all the Sharings
        /// </summary>
        /// <returns>List of Sharings</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.SharingDTO>))]
        public async Task<ActionResult<IEnumerable<V1DTO.SharingDTO>>> GetSharings()
        {
            return Ok((await _bll.Sharings.GetAllForApiAsync(User.UserId())).Select(e => _mapper.MapSharingView(e)));
        }

        /// <summary>
        /// Get single Sharing
        /// </summary>
        /// <param name="id">Sharing Id</param>
        /// <returns>request Sharing</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.SharingDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.SharingDTO>> GetSharing(Guid id)
        {
            var sharing = await _bll.Sharings.FirstOrDefaultApiAsync(id, User.UserId());
            
            if (sharing == null)
            {
                return NotFound(new V1DTO.MessageDTO($"GetSharing with id {id} not found"));
            }

            return Ok(_mapper.MapSharingView(sharing));
        }

        /// <summary>
        /// Update the Sharing
        /// </summary>
        /// <param name="id">Sharing id</param>
        /// <param name="sharing">Sharing object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutSharing(Guid id, SharingDTO sharing)
        {
            sharing.AppUserId = User.UserId();

            if (id != sharing.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and sharing.id do not match"));
            }

            await _bll.Sharings.UpdateAsync(_mapper.Map(sharing), User.UserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create a new Sharing
        /// </summary>
        /// <param name="sharing">Sharing object</param>
        /// <returns>created Sharing object</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.SharingDTO))]
        public async Task<ActionResult<V1DTO.SharingDTO>> PostSharing(SharingDTO sharing)
        {
            sharing.AppUserId = User.UserId();
            
            var bllEntity = _mapper.Map(sharing);
            _bll.Sharings.Add(bllEntity);
            await _bll.SaveChangesAsync();
            sharing.Id = bllEntity.Id;

            return CreatedAtAction("GetSharing",
                new { id = sharing.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"}, 
                sharing);
        }

        /// <summary>
        /// Delete the Sharing
        /// </summary>
        /// <param name="id">Sharing Id</param>
        /// <returns>deleted Sharing object</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.SharingDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.SharingDTO>> DeleteSharing(Guid id)
        {
            var sharing = await _bll.Sharings.FirstOrDefaultApiAsync(id, User.UserId());
            if (sharing == null)
            {
                return NotFound(new {message = "Sharing not found"});
            }

            await _bll.Sharings.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(sharing);
        }
    }
}
