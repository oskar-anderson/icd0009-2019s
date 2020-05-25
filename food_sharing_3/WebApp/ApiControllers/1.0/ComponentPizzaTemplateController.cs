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
    [Authorize(Roles = "admin")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ComponentPizzaTemplateController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ComponentPizzaTemplateMapper _mapper = new ComponentPizzaTemplateMapper();

        public ComponentPizzaTemplateController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/PizzaComponent
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<V1DTO.ComponentPizzaTemplateDTO>>> GetPizzaComponents()
        {
            return Ok(await _bll.ComponentPizzaTemplates.GetAllForApiAsync());
        }

        // GET: api/PizzaComponent/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<V1DTO.ComponentPizzaTemplateDTO>> GetPizzaComponent(Guid id)
        {
            var pizzaComponent = await _bll.ComponentPizzaTemplates.FirstOrDefaultApiAsync(id);
            
            if (pizzaComponent == null)
            {
                return NotFound(new V1DTO.MessageDTO($"GetPizzaComponent with id {id} not found"));
            }

            return Ok(pizzaComponent);
        }

        // PUT: api/PizzaComponent/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPizzaComponent(Guid id, ComponentPizzaTemplateDTO pizzaComponent)
        {
            if (id != pizzaComponent.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and pizzaComponent.id do not match"));
            }

            await _bll.ComponentPizzaTemplates.UpdateAsync(_mapper.Map(pizzaComponent));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/PizzaComponent
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<V1DTO.ComponentPizzaTemplateDTO>> PostPizzaComponent(ComponentPizzaTemplateDTO pizzaComponent)
        {
            var bllEntity = _mapper.Map(pizzaComponent);
            _bll.ComponentPizzaTemplates.Add(bllEntity);
            await _bll.SaveChangesAsync();
            pizzaComponent.Id = bllEntity.Id;

            return CreatedAtAction("GetPizzaComponent", new { id = pizzaComponent.Id }, pizzaComponent);
        }

        // DELETE: api/PizzaComponent/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<V1DTO.ComponentPizzaTemplateDTO>> DeletePizzaComponent(Guid id)
        {
            var pizzaComponent = await _bll.ComponentPizzaTemplates.FirstOrDefaultApiAsync(id);
            if (pizzaComponent == null)
            {
                return NotFound();
            }

            pizzaComponent.Component = null;
            pizzaComponent.PizzaTemplate = null;
            await _bll.ComponentPizzaTemplates.RemoveAsync(pizzaComponent);
            await _bll.SaveChangesAsync();

            return Ok(pizzaComponent);
        }
    }
}
