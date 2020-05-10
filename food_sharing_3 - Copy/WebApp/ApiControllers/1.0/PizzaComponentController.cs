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
    public class PizzaComponentController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PizzaComponentMapper _mapper = new PizzaComponentMapper();

        public PizzaComponentController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/PizzaComponent
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<V1DTO.PizzaComponentDTO>>> GetPizzaComponents()
        {
            return Ok(await _bll.PizzaComponents.GetAllAsyncBase());
        }

        // GET: api/PizzaComponent/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<V1DTO.PizzaComponentDTO>> GetPizzaComponent(Guid id)
        {
            var pizzaComponent = await _bll.PizzaComponents.FirstOrDefaultAsync(id);
            
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
        public async Task<IActionResult> PutPizzaComponent(Guid id, PizzaComponentDTO pizzaComponent)
        {
            if (id != pizzaComponent.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and pizzaComponent.id do not match"));
            }

            await _bll.PizzaComponents.UpdateAsync(_mapper.Map(pizzaComponent));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/PizzaComponent
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<V1DTO.PizzaComponentDTO>> PostPizzaComponent(PizzaComponentDTO pizzaComponent)
        {
            var bllEntity = _mapper.Map(pizzaComponent);
            _bll.PizzaComponents.Add(bllEntity);
            await _bll.SaveChangesAsync();
            pizzaComponent.Id = bllEntity.Id;

            return CreatedAtAction("GetPizzaComponent", new { id = pizzaComponent.Id }, pizzaComponent);
        }

        // DELETE: api/PizzaComponent/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<V1DTO.PizzaComponentDTO>> DeletePizzaComponent(Guid id)
        {
            var pizzaComponent = await _bll.PizzaComponents.FirstOrDefaultAsync(id);
            if (pizzaComponent == null)
            {
                return NotFound();
            }

            await _bll.PizzaComponents.RemoveAsync(pizzaComponent);
            await _bll.SaveChangesAsync();

            return Ok(pizzaComponent);
        }
    }
}
