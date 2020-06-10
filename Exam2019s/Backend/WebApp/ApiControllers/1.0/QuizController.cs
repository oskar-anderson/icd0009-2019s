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
    /// Quiz Api Controller
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class QuizController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;
        private readonly QuizMapper _mapper = new QuizMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public QuizController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        /// <summary>
        /// Get the list of Quizzes
        /// </summary>
        /// <returns>List of Quizzes</returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<QuizDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<IEnumerable<V1DTO.QuizDTO>>> GetQuizzes()
        {
            return Ok((await _uow.Quizzes.GetAllForApiAsync()).Select(e => _mapper.MapQuizView(e)));;
        }

        /// <summary>
        /// Get a single Quiz
        /// </summary>
        /// <param name="id">id for Quiz</param>
        /// <returns>Quiz</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.QuizDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.QuizDTO>> GetQuiz(Guid id)
        {
            var quiz = await _uow.Quizzes.FirstOrDefaultApiAsync(id);
            
            if (quiz == null)
            {
                return NotFound(new V1DTO.MessageDTO($"GetQuiz with id {id} not found"));
            }

            return Ok(_mapper.MapQuizView(quiz));
        }

        /// <summary>
        /// Update Quiz
        /// </summary>
        /// <param name="id"></param>
        /// <param name="quiz"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutQuiz(Guid id, QuizDTO quiz)
        {
            quiz.AppUserId = User.UserId();

            if (id != quiz.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and quiz.id do not match"));
            }

            await _uow.Quizzes.UpdateAsync(_mapper.Map(quiz), User.UserId());
            await _uow.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create/add a new Quiz
        /// </summary>
        /// <param name="quiz">Quiz info</param>
        /// <returns>created Quiz object</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.QuizDTO))]
        [HttpPost]
        public async Task<ActionResult<V1DTO.QuizDTO>> PostQuiz(QuizDTO quiz)
        {
            quiz.AppUserId = User.UserId();
            
            var bllEntity = _mapper.Map(quiz);
            _uow.Quizzes.Add(bllEntity);
            await _uow.SaveChangesAsync();
            quiz.Id = bllEntity.Id;

            return CreatedAtAction("GetQuiz",
                new { id = quiz.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"}, 
                quiz);
        }

        /// <summary>
        /// Delete the Quiz
        /// </summary>
        /// <param name="id">Quiz Id</param>
        /// <returns>deleted Quiz object</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.QuizDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.QuizDTO>> DeleteQuiz(Guid id)
        {
            var quiz = await _uow.Quizzes.FirstOrDefaultApiAsync(id);
            if (quiz == null)
            {
                return NotFound(new {message = "Quiz not found"});
            }

            await _uow.Quizzes.RemoveAsync(id);
            await _uow.SaveChangesAsync();

            return Ok(quiz);
        }
    }
}
