using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;
using Extensions;
using PublicApi.DTO.v1;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public CategoryController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/Category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories()
        {
            return Ok((await _uow.Categorys.AllAsync()).Select(c => new CategoryDTO()
            {
                Id = c.Id,
                Name = c.Name
            }));
        }

        // GET: api/Category/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetCategory(Guid id)
        {
            var category = await _uow.Categorys.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(new CategoryDTO()
            {
                Id = category.Id,
                Name = category.Name
            });
        }

        // PUT: api/Category/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(Guid id, CategoryDTO model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            var category = new Category()
            {
                Id = model.Id,
                Name = model.Name
            };

            _uow.Categorys.Update(category);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _uow.Categorys.ExistsAsync(id, User.UserGuidId()))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Category
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> PostCategory(CategoryDTO model)
        {
            var category = new Category()
            {
                Name = model.Name
            };
            _uow.Categorys.Add(category);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { id = category.Id }, category);
        }

        // DELETE: api/Category/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Category>> DeleteCategory(Guid id)
        {
            var category = await _uow.Categorys.FindAsync(id, User.UserGuidId());
            if (category == null)
            {
                return NotFound();
            }

            _uow.Categorys.Remove(category);
            await _uow.SaveChangesAsync();

            return Ok(category);
        }
    }
}
