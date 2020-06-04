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
    /// SharingItem Api Controller
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class SharingItemController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly SharingItemMapper _mapper = new SharingItemMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public SharingItemController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get a list of all the SharingItem
        /// </summary>
        /// <returns>List of SharingItem</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.SharingItemDTO>))]
        public async Task<ActionResult<IEnumerable<V1DTO.SharingItemDTO>>> GetSharingItems()
        {
            return Ok((await _bll.SharingItems.GetAllForApiAsync()).Select(e => _mapper.MapSharingItemView(e)));
        }

        /// <summary>
        /// Get single SharingItem
        /// </summary>
        /// <param name="id">SharingItem Id</param>
        /// <returns>request SharingItem</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.SharingItemDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.SharingItemDTO>> GetSharingItem(Guid id)
        {
            var sharingItem = await _bll.SharingItems.FirstOrDefaultApiAsync(id);
            
            if (sharingItem == null)
            {
                return NotFound(new V1DTO.MessageDTO($"GetSharingItem with id {id} not found"));
            }

            return Ok(_mapper.MapSharingItemView(sharingItem));
        }

        /// <summary>
        /// Update the SharingItem
        /// </summary>
        /// <param name="id">SharingItem id</param>
        /// <param name="sharingItem">SharingItem object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutSharingItem(Guid id, SharingItemDTO sharingItem)
        {
            if (id != sharingItem.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and sharingItem.id do not match"));
            }

            await _bll.SharingItems.UpdateAsync(_mapper.Map(sharingItem));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create a new SharingItem
        /// </summary>
        /// <param name="sharingItem">SharingItem object</param>
        /// <returns>created SharingItem object</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.SharingItemDTO))]
        public async Task<ActionResult<V1DTO.SharingItemDTO>> PostSharingItem(SharingItemDTO sharingItem)
        {
            var bllEntity = _mapper.Map(sharingItem);
            _bll.SharingItems.Add(bllEntity);
            await _bll.SaveChangesAsync();
            sharingItem.Id = bllEntity.Id;

            return CreatedAtAction("GetSharingItem",
                new { id = sharingItem.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"}, 
                sharingItem);
        }

        /// <summary>
        /// Delete the SharingItem
        /// </summary>
        /// <param name="id">SharingItem Id</param>
        /// <returns>deleted SharingItem object</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.SharingItemDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.SharingItemDTO>> DeleteSharingItem(Guid id)
        {
            var sharingItem = await _bll.SharingItems.FirstOrDefaultApiAsync(id);
            if (sharingItem == null)
            {
                return NotFound(new {message = "SharingItem not found"});
            }

            await _bll.SharingItems.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return Ok(sharingItem);
        }
    }
}
