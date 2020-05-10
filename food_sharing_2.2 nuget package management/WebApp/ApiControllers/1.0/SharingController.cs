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
    public class SharingController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly SharingMapper _mapper = new SharingMapper();

        public SharingController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Sharing
        [HttpGet]
        public async Task<ActionResult<IEnumerable<V1DTO.SharingDTO>>> GetSharings()
        {
            return Ok(await _bll.Sharings.GetAllAsyncBase(User.UserId()));
        }

        // GET: api/Sharing/5
        [HttpGet("{id}")]
        public async Task<ActionResult<V1DTO.SharingDTO>> GetSharing(Guid id)
        {
            var sharing = await _bll.Sharings.FirstOrDefaultAsync(id, User.UserId());
            
            if (sharing == null)
            {
                return NotFound(new V1DTO.MessageDTO($"GetSharing with id {id} not found"));
            }

            return Ok(sharing);
        }

        // PUT: api/Sharing/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
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

        // POST: api/Sharing
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<V1DTO.SharingDTO>> PostSharing(SharingDTO sharing)
        {
            var bllEntity = _mapper.Map(sharing);
            _bll.Sharings.Add(bllEntity);
            await _bll.SaveChangesAsync();
            sharing.Id = bllEntity.Id;

            return CreatedAtAction("GetSharing", new { id = sharing.Id }, sharing);
        }

        // DELETE: api/Sharing/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<V1DTO.SharingDTO>> DeleteSharing(Guid id)
        {
            var sharing = await _bll.Sharings.FirstOrDefaultAsync(id, User.UserId());
            if (sharing == null)
            {
                return NotFound();
            }

            await _bll.Sharings.RemoveAsync(sharing);
            await _bll.SaveChangesAsync();

            return Ok(sharing);
        }
    }
}
