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
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class ItemController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ItemMapper _mapper = new ItemMapper();

        public ItemController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Item
        [HttpGet]
        public async Task<ActionResult<IEnumerable<V1DTO.ItemDTO>>> GetItems()
        {
            return Ok(await _bll.Items.GetAllForApiAsync());
        }

        // GET: api/Item/5
        [HttpGet("{id}")]
        public async Task<ActionResult<V1DTO.ItemDTO>> GetItem(Guid id)
        {
            var item = await _bll.Items.FirstOrDefaultApiAsync(id);
            
            if (item == null)
            {
                return NotFound(new V1DTO.MessageDTO($"GetItem with id {id} not found"));
            }

            return Ok(item);
        }

        // PUT: api/Item/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
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

        // POST: api/Item
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<V1DTO.ItemDTO>> PostItem(ItemDTO item)
        {
            var bllEntity = _mapper.Map(item);
            _bll.Items.Add(bllEntity);
            await _bll.SaveChangesAsync();
            item.Id = bllEntity.Id;

            return CreatedAtAction("GetItem", new { id = item.Id }, item);
        }

        // DELETE: api/Item/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<V1DTO.ItemDTO>> DeleteItem(Guid id)
        {
            var item = await _bll.Items.FirstOrDefaultApiAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            await _bll.Items.RemoveAsync(item);
            await _bll.SaveChangesAsync();

            return Ok(item);
        }

    }
}
