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
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;
using V1DTO = PublicApi.DTO.v1;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Result Api Controller
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(Roles = "admin,user")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class ResultController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;
        private readonly ResultMapper _mapper = new ResultMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public ResultController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        /// <summary>
        /// Get the list of Results
        /// </summary>
        /// <returns>List of Results</returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ResultDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<IEnumerable<V1DTO.ResultDTO>>> GetResults()
        {
            return Ok((await _uow.Results.GetAllForApiAsync()).Select(e => _mapper.MapResultView(e)));
        }

        /// <summary>
        /// Get a single Result
        /// </summary>
        /// <param name="id">id for Result</param>
        /// <returns>Result</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.ResultDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.ResultDTO>> GetResult(Guid id)
        {
            var result = await _uow.Results.FirstOrDefaultApiAsync(id, User.UserId());
            
            if (result == null)
            {
                return NotFound(new V1DTO.MessageDTO($"GetResult with id {id} not found"));
            }

            return Ok(_mapper.MapResultView(result));
        }

        /// <summary>
        /// Update Result
        /// </summary>
        /// <param name="id"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutResult(Guid id, ResultDTO result)
        {
            if (id != result.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and result.id do not match"));
            }

            await _uow.Results.UpdateAsync(_mapper.Map(result), User.UserId());
            await _uow.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create/add a new Result
        /// </summary>
        /// <param name="result">Result info</param>
        /// <returns>created Result object</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.ResultDTO))]
        [HttpPost]
        public async Task<ActionResult<V1DTO.ResultDTO>> PostResult(ResultDTO result)
        {
            var bllEntity = _mapper.Map(result);
            _uow.Results.Add(bllEntity);
            await _uow.SaveChangesAsync();
            result.Id = bllEntity.Id;

            return CreatedAtAction("GetResult",
                new { id = result.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"}, 
                result);
        }

        /// <summary>
        /// Delete the Result
        /// </summary>
        /// <param name="id">Result Id</param>
        /// <returns>deleted Result object</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.ResultDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.ResultDTO>> DeleteResult(Guid id)
        {
            var result = await _uow.Results.FirstOrDefaultApiAsync(id, User.UserId());
            if (result == null)
            {
                return NotFound(new {message = "Result not found"});
            }

            await _uow.Results.RemoveAsync(id);
            await _uow.SaveChangesAsync();

            return Ok(result);
        }
    }
}
