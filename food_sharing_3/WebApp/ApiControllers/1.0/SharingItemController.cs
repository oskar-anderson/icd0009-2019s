using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Domain.Base.App.EF;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;
using V1DTO = PublicApi.DTO.v1;

namespace WebApp.ApiControllers._1._0
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SharingItemController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly SharingItemMapper _mapper = new SharingItemMapper();

        public SharingItemController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/SharingItem
        [HttpGet]
        public async Task<ActionResult<IEnumerable<V1DTO.SharingItemDTO>>> GetSharingItems()
        {
            return Ok(await _bll.Sharings.GetAllAsyncBase());
        }

        // GET: api/SharingItem/5
        [HttpGet("{id}")]
        public async Task<ActionResult<V1DTO.SharingItemDTO>> GetSharingItem(Guid id)
        {
            var sharingItem = await _bll.SharingItems.FirstOrDefaultAsync(id);
            
            if (sharingItem == null)
            {
                return NotFound(new V1DTO.MessageDTO($"GetSharingItem with id {id} not found"));
            }

            return Ok(sharingItem);
        }

        // PUT: api/SharingItem/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
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

        // POST: api/SharingItem
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<V1DTO.SharingItemDTO>> PostSharingItem(SharingItemDTO sharingItem)
        {
            var bllEntity = _mapper.Map(sharingItem);
            _bll.SharingItems.Add(bllEntity);
            await _bll.SaveChangesAsync();
            sharingItem.Id = bllEntity.Id;

            return CreatedAtAction("GetSharingItem", new { id = sharingItem.Id }, sharingItem);
        }

        // DELETE: api/SharingItem/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<V1DTO.SharingItemDTO>> DeleteSharingItem(Guid id)
        {
            var sharingItem = await _bll.SharingItems.FirstOrDefaultAsync(id);
            if (sharingItem == null)
            {
                return NotFound();
            }

            await _bll.SharingItems.RemoveAsync(sharingItem);
            await _bll.SaveChangesAsync();

            return Ok(sharingItem);
        }
    }
}
