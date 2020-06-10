using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;
using V1DTO = PublicApi.DTO.v1;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Choice Api Controller
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin,user")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class ChoiceController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;
        private readonly ChoiceMapper _mapper = new ChoiceMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public ChoiceController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        /// <summary>
        /// Get the list of Question Choices
        /// </summary>
        /// <returns>List of Question Choices</returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ChoiceDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<IEnumerable<V1DTO.ChoiceDTO>>> GetChoices()
        {
            return Ok((await _uow.Choices.GetAllForApiAsync()).Select(e => _mapper.MapChoiceView(e)));
        }

        /// <summary>
        /// Get a single Choice
        /// </summary>
        /// <param name="id">id for Choice</param>
        /// <returns>Choice</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.ChoiceDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.ChoiceDTO>> GetChoice(Guid id)
        {
            var choice = await _uow.Choices.FirstOrDefaultApiAsync(id);
            
            if (choice == null)
            {
                return NotFound(new V1DTO.MessageDTO($"GetChoice with id {id} not found"));
            }

            return Ok(_mapper.MapChoiceView(choice));;
        }

        /// <summary>
        /// Update Choice
        /// </summary>
        /// <param name="id"></param>
        /// <param name="choice"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutChoice(Guid id, ChoiceDTO choice)
        {
            if (id != choice.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and choice.id do not match"));
            }

            await _uow.Choices.UpdateAsync(_mapper.Map(choice));
            await _uow.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create/add a new Choice
        /// </summary>
        /// <param name="choice">Choice info</param>
        /// <returns>created Choice object</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.ChoiceDTO))]
        [HttpPost]
        public async Task<ActionResult<V1DTO.ChoiceDTO>> PostChoice(ChoiceDTO choice)
        {
            var bllEntity = _mapper.Map(choice);
            _uow.Choices.Add(bllEntity);
            await _uow.SaveChangesAsync();
            choice.Id = bllEntity.Id;

            return CreatedAtAction("GetChoice",
                new { id = choice.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"}, 
                choice);
        }

        /// <summary>
        /// Delete the Choice
        /// </summary>
        /// <param name="id">Choice Id</param>
        /// <returns>deleted Choice object</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.ChoiceDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.ChoiceDTO>> DeleteChoice(Guid id)
        {
            var choice = await _uow.Choices.FirstOrDefaultApiAsync(id);
            if (choice == null)
            {
                return NotFound(new {message = "Choice not found"});
            }

            await _uow.Choices.RemoveAsync(id);
            await _uow.SaveChangesAsync();

            return Ok(choice);
        }
    }
}