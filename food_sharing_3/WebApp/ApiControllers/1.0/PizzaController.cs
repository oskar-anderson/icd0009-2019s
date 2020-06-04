using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Domain.Base.App.EF;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;
using V1DTO = PublicApi.DTO.v1;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Pizza Api Controller
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class PizzaController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PizzaMapper _mapper = new PizzaMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public PizzaController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get a list of all the Pizzas
        /// </summary>
        /// <returns>List of Pizzas</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.PizzaDTO>))]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<V1DTO.PizzaDTO>>> GetPizzas()
        {
            return Ok((await _bll.Pizzas.GetAllForApiAsync()).Select(e => _mapper.MapPizzaView(e)));
        }

        /// <summary>
        /// Get single Pizza
        /// </summary>
        /// <param name="id">Pizza Id</param>
        /// <returns>request Pizza</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.PizzaDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.PizzaDTO>> GetPizza(Guid id)
        {
            var pizza = await _bll.Pizzas.FirstOrDefaultApiAsync(id);
            
            if (pizza == null)
            {
                return NotFound(new V1DTO.MessageDTO($"GetPizza with id {id} not found"));
            }

            return Ok(_mapper.MapPizzaView(pizza));
        }

        /// <summary>
        /// Update the Pizza
        /// </summary>
        /// <param name="id">Pizza id</param>
        /// <param name="pizza">Pizza object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
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

        /// <summary>
        /// Create a new Pizza
        /// </summary>
        /// <param name="pizza">Pizza object</param>
        /// <returns>created Pizza object</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.PizzaDTO))]
        public async Task<ActionResult<V1DTO.PizzaDTO>> PostPizza(PizzaDTO pizza)
        {
            var bllEntity = _mapper.Map(pizza);
            _bll.Pizzas.Add(bllEntity);
            await _bll.SaveChangesAsync();
            pizza.Id = bllEntity.Id;

            return CreatedAtAction("GetPizza",
                new { id = pizza.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"}, 
                pizza);
        }

        /// <summary>
        /// Delete the Pizza
        /// </summary>
        /// <param name="id">Pizza Id</param>
        /// <returns>deleted Pizza object</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.PizzaDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.PizzaDTO>> DeletePizza(Guid id)
        {
            var pizza = await _bll.Pizzas.FirstOrDefaultApiAsync(id);
            if (pizza == null)
            {
                return NotFound(new {message = "Pizza not found"});
            }
            pizza.PizzaTemplate = null;

            await _bll.Pizzas.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(pizza);
        }
    }
}
