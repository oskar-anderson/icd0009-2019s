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
    /// Question Api Controller
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(Roles = "admin,user")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class QuestionController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;
        private readonly QuestionMapper _mapper = new QuestionMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public QuestionController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        /// <summary>
        /// Get the list of Questions
        /// </summary>
        /// <returns>List of Question</returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<QuestionDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<IEnumerable<V1DTO.QuestionDTO>>> GetQuestions()
        {
            return Ok((await _uow.Questions.GetAllForApiAsync()).Select(e => _mapper.MapQuestionView(e)));
        }

        /// <summary>
        /// Get a single Quiz Question
        /// </summary>
        /// <param name="id">id for Question</param>
        /// <returns>Question</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.QuestionDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.QuestionDTO>> GetQuestion(Guid id)
        {
            var question = await _uow.Questions.FirstOrDefaultApiAsync(id);
            
            if (question == null)
            {
                return NotFound(new V1DTO.MessageDTO($"GetQuestion with id {id} not found"));
            }

            return Ok(_mapper.MapQuestionView(question));
        }

        /// <summary>
        /// Update Question
        /// </summary>
        /// <param name="id"></param>
        /// <param name="question"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutQuestion(Guid id, QuestionDTO question)
        {
            if (id != question.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and question.id do not match"));
            }

            await _uow.Questions.UpdateAsync(_mapper.Map(question));
            await _uow.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create/add a new Question
        /// </summary>
        /// <param name="question">Question info</param>
        /// <returns>created Question object</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.QuestionDTO))]
        [HttpPost]
        public async Task<ActionResult<V1DTO.QuestionDTO>> PostQuestion(QuestionDTO question)
        {
            var bllEntity = _mapper.Map(question);
            _uow.Questions.Add(bllEntity);
            await _uow.SaveChangesAsync();
            question.Id = bllEntity.Id;

            return CreatedAtAction("GetQuestion",
                new { id = question.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"}, 
                question);
        }

        /// <summary>
        /// Delete the Question
        /// </summary>
        /// <param name="id">Question Id</param>
        /// <returns>deleted Question object</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.QuestionDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.QuestionDTO>> DeleteQuestion(Guid id)
        {
            var question = await _uow.Questions.FirstOrDefaultApiAsync(id);
            if (question == null)
            {
                return NotFound(new {message = "Question not found"});
            }

            await _uow.Questions.RemoveAsync(id);
            await _uow.SaveChangesAsync();

            return Ok(question);
        }
    }
}
