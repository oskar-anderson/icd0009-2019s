using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Domain.Base.App.EF;
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
    [Authorize(Roles = "admin")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MealController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly MealMapper _mapper = new MealMapper();

        public MealController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Meal
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<V1DTO.MealDTO>>> GetMeals()
        {
            return Ok(await _bll.Meals.GetAllAsyncBase());
        }

        // GET: api/Meal/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<V1DTO.MealDTO>> GetMeal(Guid id)
        {
            var meal = await _bll.Meals.FirstOrDefaultAsync(id);
            
            if (meal == null)
            {
                return NotFound(new V1DTO.MessageDTO($"GetMeal with id {id} not found"));
            }

            return Ok(meal);
        }

        // PUT: api/Meal/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMeal(Guid id, MealDTO meal)
        {
            if (id != meal.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and meal.id do not match"));
            }

            await _bll.Meals.UpdateAsync(_mapper.Map(meal));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Meal
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<V1DTO.MealDTO>> PostMeal(MealDTO meal)
        {
            var bllEntity = _mapper.Map(meal);
            _bll.Meals.Add(bllEntity);
            await _bll.SaveChangesAsync();
            meal.Id = bllEntity.Id;

            return CreatedAtAction("GetMeal", new { id = meal.Id }, meal);
        }

        // DELETE: api/Meal/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<V1DTO.MealDTO>> DeleteMeal(Guid id)
        {
            var meal = await _bll.Meals.FirstOrDefaultAsync(id);
            if (meal == null)
            {
                return NotFound();
            }

            await _bll.Meals.RemoveAsync(meal);
            await _bll.SaveChangesAsync();

            return Ok(meal);
        }
    }
}
