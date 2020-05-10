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
    
    public class PizzaFinalController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PizzaFinalMapper _mapper = new PizzaFinalMapper();

        public PizzaFinalController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/PizzaFinal
        [HttpGet]
        public async Task<ActionResult<IEnumerable<V1DTO.PizzaFinalDTO>>> GetPizzaFinals()
        {
            return Ok(await _bll.PizzaFinals.GetAllAsyncBase());
        }

        // GET: api/PizzaFinal/5
        [HttpGet("{id}")]
        public async Task<ActionResult<V1DTO.PizzaFinalDTO>> GetPizzaFinal(Guid id)
        {
            var pizzaFinal = await _bll.PizzaFinals.FirstOrDefaultAsync(id);
            
            if (pizzaFinal == null)
            {
                return NotFound(new V1DTO.MessageDTO($"GetPizzaFinal with id {id} not found"));
            }

            return Ok(pizzaFinal);
        }

        // PUT: api/PizzaFinal/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPizzaFinal(Guid id, PizzaFinalDTO pizzaFinal)
        {
            if (id != pizzaFinal.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and pizzaFinal.id do not match"));
            }

            await _bll.PizzaFinals.UpdateAsync(_mapper.Map(pizzaFinal));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/PizzaFinal
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<V1DTO.PizzaFinalDTO>> PostPizzaFinal(PizzaFinalDTO pizzaFinal)
        {
            var bllEntity = _mapper.Map(pizzaFinal);
            _bll.PizzaFinals.Add(bllEntity);
            await _bll.SaveChangesAsync();
            pizzaFinal.Id = bllEntity.Id;

            return CreatedAtAction("GetPizzaFinal", new { id = pizzaFinal.Id }, pizzaFinal);
        }

        // DELETE: api/PizzaFinal/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<V1DTO.PizzaFinalDTO>> DeletePizzaFinal(Guid id)
        {
            var pizzaFinal = await _bll.PizzaFinals.FirstOrDefaultAsync(id);
            if (pizzaFinal == null)
            {
                return NotFound();
            }

            await _bll.PizzaFinals.RemoveAsync(pizzaFinal);
            await _bll.SaveChangesAsync();

            return Ok(pizzaFinal);
        }
    }
}
