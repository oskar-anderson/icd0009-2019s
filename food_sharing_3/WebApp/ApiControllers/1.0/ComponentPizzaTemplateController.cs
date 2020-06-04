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
    /// ComponentPizzaTemplate Api Controller
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class ComponentPizzaTemplateController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ComponentPizzaTemplateMapper _mapper = new ComponentPizzaTemplateMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public ComponentPizzaTemplateController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get a list of all the ComponentPizzaTemplates
        /// </summary>
        /// <returns>List of ComponentPizzaTemplates</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.ComponentPizzaTemplateDTO>))]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<V1DTO.ComponentPizzaTemplateDTO>>> GetPizzaComponents()
        {
            return Ok((await _bll.ComponentPizzaTemplates.GetAllForApiAsync()).Select(e => _mapper.MapComponentPizzaTemplateView(e)));
        }

        /// <summary>
        /// Get single ComponentPizzaTemplate
        /// </summary>
        /// <param name="id">ComponentPizzaTemplate Id</param>
        /// <returns>request ComponentPizzaTemplate</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.ComponentDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.ComponentPizzaTemplateDTO>> GetPizzaComponent(Guid id)
        {
            var pizzaComponent = await _bll.ComponentPizzaTemplates.FirstOrDefaultApiAsync(id);
            
            if (pizzaComponent == null)
            {
                return NotFound(new V1DTO.MessageDTO($"GetPizzaComponent with id {id} not found"));
            }

            return Ok(_mapper.MapComponentPizzaTemplateView(pizzaComponent));
        }

        /// <summary>
        /// Update the PizzaComponent
        /// </summary>
        /// <param name="id">PizzaComponent id</param>
        /// <param name="pizzaComponent">PizzaComponent object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
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

        /// <summary>
        /// Create a new PizzaComponent
        /// </summary>
        /// <param name="pizzaComponent">PizzaComponent object</param>
        /// <returns>created PizzaComponent object</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.ComponentPizzaTemplateDTO))]
        public async Task<ActionResult<V1DTO.ComponentPizzaTemplateDTO>> PostPizzaComponent(ComponentPizzaTemplateDTO pizzaComponent)
        {
            var bllEntity = _mapper.Map(pizzaComponent);
            _bll.ComponentPizzaTemplates.Add(bllEntity);
            await _bll.SaveChangesAsync();
            pizzaComponent.Id = bllEntity.Id;

            return CreatedAtAction("GetPizzaComponent",
                new { id = pizzaComponent.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"}, 
                pizzaComponent);
        }

        /// <summary>
        /// Delete the ComponentPizzaTemplate
        /// </summary>
        /// <param name="id">ComponentPizzaTemplate Id</param>
        /// <returns>deleted ComponentPizzaTemplate object</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.ComponentPizzaTemplateDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.ComponentPizzaTemplateDTO>> DeletePizzaComponent(Guid id)
        {
            var pizzaComponent = await _bll.ComponentPizzaTemplates.FirstOrDefaultApiAsync(id);
            if (pizzaComponent == null)
            {
                return NotFound(new {message = "ComponentPizza not found"});
            }

            pizzaComponent.Component = null;
            pizzaComponent.PizzaTemplate = null;
            await _bll.ComponentPizzaTemplates.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(pizzaComponent);
        }
    }
}
