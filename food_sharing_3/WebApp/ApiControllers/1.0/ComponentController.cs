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
    /// Component Api Controller
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class ComponentController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ComponentMapper _mapper = new ComponentMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public ComponentController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get a list of all the Components
        /// </summary>
        /// <returns>List of Components</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.ComponentDTO>))]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<V1DTO.ComponentDTO>>> GetComponents()
        {
            return Ok((await _bll.Components.GetAllAsyncBase()).Select(e => _mapper.Map(e)));
        }

        /// <summary>
        /// Get single Component
        /// </summary>
        /// <param name="id">Component Id</param>
        /// <returns>request Component</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.ComponentDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.ComponentDTO>> GetComponent(Guid id)
        {
            var component = await _bll.Components.FirstOrDefaultAsync(id);
            
            if (component == null)
            {
                return NotFound(new V1DTO.MessageDTO($"GetComponent with id {id} not found"));
            }

            return Ok(_mapper.Map(component));
        }

        /// <summary>
        /// Update the Component
        /// </summary>
        /// <param name="id">Component id</param>
        /// <param name="component">Component object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
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

        /// <summary>
        /// Create a new Component
        /// </summary>
        /// <param name="component">Component object</param>
        /// <returns>created Component object</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.ComponentDTO))]
        public async Task<ActionResult<V1DTO.ComponentDTO>> PostComponent(ComponentDTO component)
        {
            var bllEntity = _mapper.Map(component);
            _bll.Components.Add(bllEntity);
            await _bll.SaveChangesAsync();
            component.Id = bllEntity.Id;

            return CreatedAtAction("GetComponent", 
                new { id = component.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"}, 
                component);
        }

        /// <summary>
        /// Delete the Component
        /// </summary>
        /// <param name="id">Component Id</param>
        /// <returns>deleted Component object</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.ComponentDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.ComponentDTO>> DeleteComponent(Guid id)
        {
            var component = await _bll.Components.FirstOrDefaultAsync(id);
            if (component == null)
            {
                return NotFound(new {message = "Component not found"});
            }

            await _bll.Components.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(component);
        }
    }
}
