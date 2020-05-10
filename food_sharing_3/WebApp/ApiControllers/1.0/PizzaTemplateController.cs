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
    [Authorize(Roles = "admin")]
    public class PizzaTemplateController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PizzaTemplateMapper _mapper = new PizzaTemplateMapper();

        public PizzaTemplateController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/PizzaTemplate
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<V1DTO.PizzaTemplateDTO>>> GetPizzaTemplates()
        {
            //var result = await _bll.PizzaTemplates.GetAllForViewAsync();
            return Ok(await _bll.PizzaTemplates.GetAllAsyncBase());
        }

        // GET: api/PizzaTemplate/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<V1DTO.PizzaTemplateDTO>> GetPizzaTemplate(Guid id)
        {
            var pizzaTemplate = await _bll.PizzaTemplates.FirstOrDefaultAsync(id);
            
            if (pizzaTemplate == null)
            {
                return NotFound(new V1DTO.MessageDTO($"GetPizzaTemplate with id {id} not found"));
            }

            return Ok(pizzaTemplate);
        }

        // PUT: api/PizzaTemplate/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPizzaTemplate(Guid id, PizzaTemplateDTO pizzaTemplate)
        {
            if (id != pizzaTemplate.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and pizzaTemplate.id do not match"));
            }

            await _bll.PizzaTemplates.UpdateAsync(_mapper.Map(pizzaTemplate));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/PizzaTemplate
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<V1DTO.PizzaTemplateDTO>> PostPizzaTemplate(PizzaTemplateDTO pizzaTemplate)
        {
            var bllEntity = _mapper.Map(pizzaTemplate);
            _bll.PizzaTemplates.Add(bllEntity);
            await _bll.SaveChangesAsync();
            pizzaTemplate.Id = bllEntity.Id;

            return CreatedAtAction("GetPizzaTemplate", new { id = pizzaTemplate.Id }, pizzaTemplate);
        }

        // DELETE: api/PizzaTemplate/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<V1DTO.PizzaTemplateDTO>> DeletePizzaTemplate(Guid id)
        {
            var pizzaTemplate = await _bll.PizzaTemplates.FirstOrDefaultAsync(id);
            if (pizzaTemplate == null)
            {
                return NotFound();
            }

            await _bll.PizzaTemplates.RemoveAsync(pizzaTemplate);
            await _bll.SaveChangesAsync();

            return Ok(pizzaTemplate);
        }
    }
}
