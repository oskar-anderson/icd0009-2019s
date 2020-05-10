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
    [Authorize(Roles = "Admin")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ComponentPriceController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ComponentPriceMapper _mapper = new ComponentPriceMapper();

        public ComponentPriceController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/ComponentPrice
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<V1DTO.ComponentPriceDTO>>> GetComponentPrices()
        {
            return Ok(await _bll.ComponentPrices.GetAllAsyncBase());
        }

        // GET: api/ComponentPrice/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<V1DTO.ComponentPriceDTO>> GetComponentPrice(Guid id)
        {
            var componentPrice = await _bll.ComponentPrices.FirstOrDefaultAsync(id);
            
            if (componentPrice == null)
            {
                return NotFound(new V1DTO.MessageDTO($"GetComponentPrice with id {id} not found"));
            }

            return Ok(componentPrice);
        }

        // PUT: api/ComponentPrice/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComponentPrice(Guid id, ComponentPriceDTO componentPrice)
        {
            if (id != componentPrice.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and componentPrice.id do not match"));
            }

            await _bll.ComponentPrices.UpdateAsync(_mapper.Map(componentPrice));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/ComponentPrice
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<V1DTO.ComponentPriceDTO>> PostComponentPrice(ComponentPriceDTO componentPrice)
        {
            var bllEntity = _mapper.Map(componentPrice);
            _bll.ComponentPrices.Add(bllEntity);
            await _bll.SaveChangesAsync();
            componentPrice.Id = bllEntity.Id;

            return CreatedAtAction("GetComponentPrice", new { id = componentPrice.Id }, componentPrice);
        }

        // DELETE: api/ComponentPrice/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<V1DTO.ComponentPriceDTO>> DeleteComponentPrice(Guid id)
        {
            var componentPrice = await _bll.ComponentPrices.FirstOrDefaultAsync(id);
            if (componentPrice == null)
            {
                return NotFound();
            }

            await _bll.ComponentPrices.RemoveAsync(componentPrice);
            await _bll.SaveChangesAsync();

            return Ok(componentPrice);
        }
    }
}
