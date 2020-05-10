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
    public class ComponentController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ComponentMapper _mapper = new ComponentMapper();

        public ComponentController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Component
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<V1DTO.ComponentDTO>>> GetComponents()
        {
            return Ok(await _bll.Components.GetAllAsyncBase());
        }

        // GET: api/Component/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<V1DTO.ComponentDTO>> GetComponent(Guid id)
        {
            var component = await _bll.Components.FirstOrDefaultAsync(id);
            
            if (component == null)
            {
                return NotFound(new V1DTO.MessageDTO($"GetComponent with id {id} not found"));
            }

            return Ok(component);
        }

        // PUT: api/Component/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComponent(Guid id, ComponentDTO component)
        {
            if (id != component.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and component.id do not match"));
            }

            await _bll.Components.UpdateAsync(_mapper.Map(component));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Component
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<V1DTO.ComponentDTO>> PostComponent(ComponentDTO component)
        {
            var bllEntity = _mapper.Map(component);
            _bll.Components.Add(bllEntity);
            await _bll.SaveChangesAsync();
            component.Id = bllEntity.Id;

            return CreatedAtAction("GetComponent", new { id = component.Id }, component);
        }

        // DELETE: api/Component/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<V1DTO.ComponentDTO>> DeleteComponent(Guid id)
        {
            var component = await _bll.Components.FirstOrDefaultAsync(id);
            if (component == null)
            {
                return NotFound();
            }

            await _bll.Components.RemoveAsync(component);
            await _bll.SaveChangesAsync();

            return Ok(component);
        }
    }
}
