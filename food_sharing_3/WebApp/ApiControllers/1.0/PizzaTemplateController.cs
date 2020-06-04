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
    /// PizzaTemplate Api Controller
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(Roles = "admin")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class PizzaTemplateController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PizzaTemplateMapper _mapper = new PizzaTemplateMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public PizzaTemplateController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get a list of all the PizzaTemplates
        /// </summary>
        /// <returns>List of PizzaTemplates</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.PizzaTemplateDTO>))]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<V1DTO.PizzaTemplateDTO>>> GetPizzaTemplates()
        {
            //var result = await _bll.PizzaTemplates.GetAllForViewAsync();
            return Ok((await _bll.PizzaTemplates.GetAllForApiAsync()).Select(e => _mapper.MapPizzaTemplateView(e)));
        }

        /// <summary>
        /// Get single PizzaTemplate
        /// </summary>
        /// <param name="id">PizzaTemplate Id</param>
        /// <returns>request PizzaTemplate</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.PizzaTemplateDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.PizzaTemplateDTO>> GetPizzaTemplate(Guid id)
        {
            var pizzaTemplate = await _bll.PizzaTemplates.FirstOrDefaultApiAsync(id);
            
            if (pizzaTemplate == null)
            {
                return NotFound(new V1DTO.MessageDTO($"GetPizzaTemplate with id {id} not found"));
            }

            return Ok(_mapper.MapPizzaTemplateView(pizzaTemplate));
        }

        /// <summary>
        /// Update the PizzaTemplate
        /// </summary>
        /// <param name="id">PizzaTemplate id</param>
        /// <param name="pizzaTemplate">PizzaTemplate object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutPizzaTemplate(Guid id, PizzaTemplateDTO pizzaTemplate)
        {
            pizzaTemplate.Category = null;
            if (id != pizzaTemplate.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and pizzaTemplate.id do not match"));
            }

            await _bll.PizzaTemplates.UpdateAsync(_mapper.Map(pizzaTemplate));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create a new PizzaTemplate
        /// </summary>
        /// <param name="pizzaTemplate">PizzaTemplate object</param>
        /// <returns>created PizzaTemplate object</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.PizzaTemplateDTO))]
        public async Task<ActionResult<V1DTO.PizzaTemplateDTO>> PostPizzaTemplate(PizzaTemplateDTO pizzaTemplate)
        {
            var bllEntity = _mapper.Map(pizzaTemplate);
            _bll.PizzaTemplates.Add(bllEntity);
            await _bll.SaveChangesAsync();
            pizzaTemplate.Id = bllEntity.Id;

            return CreatedAtAction("GetPizzaTemplate",
                new { id = pizzaTemplate.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"}, 
                pizzaTemplate);
        }

        /// <summary>
        /// Delete the PizzaTemplate
        /// </summary>
        /// <param name="id">PizzaTemplate Id</param>
        /// <returns>deleted PizzaTemplate object</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.PizzaTemplateDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.PizzaTemplateDTO>> DeletePizzaTemplate(Guid id)
        {
            var pizzaTemplate = await _bll.PizzaTemplates.FirstOrDefaultAsync(id);
            if (pizzaTemplate == null)
            {
                return NotFound(new {message = "PizzaTemplate not found"});
            }

            await _bll.PizzaTemplates.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(pizzaTemplate);
        }
    }
}
