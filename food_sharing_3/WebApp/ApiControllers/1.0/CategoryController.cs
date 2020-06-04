using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
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
    /// Category Api Controller
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(Roles = "admin")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class CategoryController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly CategoryMapper _mapper = new CategoryMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public CategoryController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get a list of all the Categories
        /// </summary>
        /// <returns>List of Categories</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CategoryDTO>))]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<V1DTO.CategoryDTO>>> GetCategories()
        {
            return Ok((await _bll.Categorys.GetAllAsyncBase()).Select(e => _mapper.Map(e)));
        }

        /// <summary>
        /// Get single Category
        /// </summary>
        /// <param name="id">Category Id</param>
        /// <returns>request Category</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.CategoryDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.CategoryDTO>> GetCategory(Guid id)
        {
            var category = await _bll.Categorys.FirstOrDefaultAsync(id);
            
            if (category == null)
            {
                return NotFound(new V1DTO.MessageDTO($"GetCategory with id {id} not found"));
            }

            return Ok(_mapper.Map(category));
        }

        /// <summary>
        /// Update the Category
        /// </summary>
        /// <param name="id">Category id</param>
        /// <param name="categoryDTO">Category object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutCategory(Guid id, CategoryDTO categoryDTO)
        {
            if (id != categoryDTO.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and CategoryDTO.id do not match"));
            }

            await _bll.Categorys.UpdateAsync(_mapper.Map(categoryDTO));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create a new Category
        /// </summary>
        /// <param name="category">CCategory object</param>
        /// <returns>created Category object</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.CategoryDTO))]
        public async Task<ActionResult<V1DTO.CategoryDTO>> PostCategory(CategoryDTO category)
        {
            var bllEntity = _mapper.Map(category);
            _bll.Categorys.Add(bllEntity);
            await _bll.SaveChangesAsync();
            category.Id = bllEntity.Id;

            return CreatedAtAction("GetCategory",
                new { id = category.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0" },
                category);
        }

        /// <summary>
        /// Delete the Category
        /// </summary>
        /// <param name="id">Category Id</param>
        /// <returns>deleted Category object</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.CategoryDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.CategoryDTO>> DeleteCategory(Guid id)
        {
            var category = await _bll.Categorys.FirstOrDefaultAsync(id);
            if (category == null)
            {
                return NotFound(new {message = "Category not found"});
            }

            await _bll.Categorys.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(category);
        }
    }
}
