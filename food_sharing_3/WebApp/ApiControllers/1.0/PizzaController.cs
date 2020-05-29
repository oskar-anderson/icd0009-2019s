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
    public class PizzaController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PizzaMapper _mapper = new PizzaMapper();

        public PizzaController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Pizza
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<V1DTO.PizzaDTO>>> GetPizzas()
        {
            return Ok(await _bll.Pizzas.GetAllForApiAsync());
        }

        // GET: api/Pizza/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<V1DTO.PizzaDTO>> GetPizza(Guid id)
        {
            var pizza = await _bll.Pizzas.FirstOrDefaultApiAsync(id);
            
            if (pizza == null)
            {
                return NotFound(new V1DTO.MessageDTO($"GetPizza with id {id} not found"));
            }

            return Ok(pizza);
        }

        // PUT: api/Pizza/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPizza(Guid id, PizzaDTO pizza)
        {
            if (id != pizza.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and pizza.id do not match"));
            }

            await _bll.Pizzas.UpdateAsync(_mapper.Map(pizza));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Pizza
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<V1DTO.PizzaDTO>> PostPizza(PizzaDTO pizza)
        {
            var bllEntity = _mapper.Map(pizza);
            _bll.Pizzas.Add(bllEntity);
            await _bll.SaveChangesAsync();
            pizza.Id = bllEntity.Id;

            return CreatedAtAction("GetPizza", new { id = pizza.Id }, pizza);
        }

        // DELETE: api/Pizza/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<V1DTO.PizzaDTO>> DeletePizza(Guid id)
        {
            var pizza = await _bll.Pizzas.FirstOrDefaultApiAsync(id);
            if (pizza == null)
            {
                return NotFound();
            }
            pizza.PizzaTemplate = null;

            await _bll.Pizzas.RemoveAsync(pizza);
            await _bll.SaveChangesAsync();

            return Ok(pizza);
        }
    }
}
