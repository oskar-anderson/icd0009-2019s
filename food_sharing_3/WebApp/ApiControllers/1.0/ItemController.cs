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
    /// Item Api Controller
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class ItemController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ItemMapper _mapper = new ItemMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public ItemController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get a list of all the Items
        /// </summary>
        /// <returns>List of Item</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.ItemDTO>))]
        public async Task<ActionResult<IEnumerable<V1DTO.ItemDTO>>> GetItems()
        {
            return Ok((await _bll.Items.GetAllForApiAsync()).Select(e => _mapper.MapItemView(e)));
        }

        /// <summary>
        /// Get single Item
        /// </summary>
        /// <param name="id">Item Id</param>
        /// <returns>request Item</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.ItemDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.ItemDTO>> GetItem(Guid id)
        {
            var item = await _bll.Items.FirstOrDefaultApiAsync(id);
            
            if (item == null)
            {
                return NotFound(new V1DTO.MessageDTO($"GetItem with id {id} not found"));
            }

            return Ok(_mapper.MapItemView(item));
        }

        /// <summary>
        /// Update the Item
        /// </summary>
        /// <param name="id">Item id</param>
        /// <param name="item">Item object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutItem(Guid id, ItemDTO item)
        {
            if (id != item.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and item.id do not match"));
            }

            await _bll.Items.UpdateAsync(_mapper.Map(item));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create a new Item
        /// </summary>
        /// <param name="item">Item object</param>
        /// <returns>created Item object</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.ItemDTO))]
        public async Task<ActionResult<V1DTO.ItemDTO>> PostItem(ItemDTO item)
        {
            var bllEntity = _mapper.Map(item);
            _bll.Items.Add(bllEntity);
            await _bll.SaveChangesAsync();
            item.Id = bllEntity.Id;

            return CreatedAtAction("GetItem",
                new { id = item.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"}, 
                item);
        }

        /// <summary>
        /// Delete the Item
        /// </summary>
        /// <param name="id">Item Id</param>
        /// <returns>deleted Item object</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.ItemDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.ItemDTO>> DeleteItem(Guid id)
        {
            var item = await _bll.Items.FirstOrDefaultApiAsync(id);
            if (item == null)
            {
                return NotFound(new {message = "Item not found"});
            }
            item.Sharing = null;

            await _bll.Items.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(item);
        }

    }
}
