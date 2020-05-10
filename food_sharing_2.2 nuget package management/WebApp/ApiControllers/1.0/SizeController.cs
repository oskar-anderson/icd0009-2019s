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
    [Authorize(Roles = "Admin")]
    public class SizeController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly SizeMapper _mapper = new SizeMapper();

        public SizeController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Size
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<V1DTO.SizeDTO>>> GetSizes()
        {
            return Ok(await _bll.Sizes.GetAllAsyncBase());
        }

        // GET: api/Size/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<V1DTO.SizeDTO>> GetSize(Guid id)
        {
            var size = await _bll.Sizes.FirstOrDefaultAsync(id);
            
            if (size == null)
            {
                return NotFound(new V1DTO.MessageDTO($"GetSize with id {id} not found"));
            }

            return Ok(size);
        }

        // PUT: api/Size/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSize(Guid id, SizeDTO size)
        {
            if (id != size.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and size.id do not match"));
            }

            await _bll.Sizes.UpdateAsync(_mapper.Map(size));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Size
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<V1DTO.SizeDTO>> PostSize(SizeDTO size)
        {
            var bllEntity = _mapper.Map(size);
            _bll.Sizes.Add(bllEntity);
            await _bll.SaveChangesAsync();
            size.Id = bllEntity.Id;

            return CreatedAtAction("GetSize", new { id = size.Id }, size);
        }

        // DELETE: api/Size/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<V1DTO.SizeDTO>> DeleteSize(Guid id)
        {
            var size = await _bll.Sizes.FirstOrDefaultAsync(id);
            if (size == null)
            {
                return NotFound();
            }

            await _bll.Sizes.RemoveAsync(size);
            await _bll.SaveChangesAsync();

            return Ok(size);
        }
    }
}
