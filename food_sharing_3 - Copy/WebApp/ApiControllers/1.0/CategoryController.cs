using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Contracts.DAL.App;
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
    [Authorize(Roles = "Admin")]
    public class CategoryController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly CategoryMapper _mapper = new CategoryMapper();

        public CategoryController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Category
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<V1DTO.CategoryDTO>>> GetCategories()
        {
            return Ok(await _bll.Categorys.GetAllAsyncBase());
        }

        // GET: api/Category/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<V1DTO.CategoryDTO>> GetCategory(Guid id)
        {
            var category = await _bll.Categorys.FirstOrDefaultAsync(id);
            
            if (category == null)
            {
                return NotFound(new V1DTO.MessageDTO($"GetCategory with id {id} not found"));
            }

            return Ok(category);
        }

        // PUT: api/Category/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
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

        // POST: api/Category
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<V1DTO.CategoryDTO>> PostCategory(CategoryDTO category)
        {
            var bllEntity = _mapper.Map(category);
            _bll.Categorys.Add(bllEntity);
            await _bll.SaveChangesAsync();
            category.Id = bllEntity.Id;

            return CreatedAtAction("GetCategory", new { id = category.Id }, category);
        }

        // DELETE: api/Category/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<V1DTO.CategoryDTO>> DeleteCategory(Guid id)
        {
            var category = await _bll.Categorys.FirstOrDefaultAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            await _bll.Categorys.RemoveAsync(category);
            await _bll.SaveChangesAsync();

            return Ok(category);
        }
    }
}
