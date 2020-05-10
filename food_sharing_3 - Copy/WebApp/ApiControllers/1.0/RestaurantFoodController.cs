using System;
using System.Collections.Generic;
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
    public class RestaurantFoodController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly RestaurantFoodMapper _mapper = new RestaurantFoodMapper();

        public RestaurantFoodController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/RestaurantFood
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<V1DTO.RestaurantFoodDTO>>> GetRestaurantFoods()
        {
            return Ok(await _bll.RestaurantFoods.GetAllAsyncBase());
        }

        // GET: api/RestaurantFood/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<V1DTO.RestaurantFoodDTO>> GetRestaurantFood(Guid id)
        {
            var restaurantFood = await _bll.RestaurantFoods.FirstOrDefaultAsync(id);
            
            if (restaurantFood == null)
            {
                return NotFound(new V1DTO.MessageDTO($"GetRestaurantFood with id {id} not found"));
            }

            return Ok(restaurantFood);
        }

        // PUT: api/RestaurantFood/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRestaurantFood(Guid id, RestaurantFoodDTO restaurantFood)
        {
            if (id != restaurantFood.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and restaurantFood.id do not match"));
            }

            await _bll.RestaurantFoods.UpdateAsync(_mapper.Map(restaurantFood));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/RestaurantFood
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<V1DTO.RestaurantFoodDTO>> PostRestaurantFood(RestaurantFoodDTO restaurantFood)
        {
            var bllEntity = _mapper.Map(restaurantFood);
            _bll.RestaurantFoods.Add(bllEntity);
            await _bll.SaveChangesAsync();
            restaurantFood.Id = bllEntity.Id;

            return CreatedAtAction("GetRestaurantFood", new { id = restaurantFood.Id }, restaurantFood);
        }

        // DELETE: api/RestaurantFood/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<V1DTO.RestaurantFoodDTO>> DeleteRestaurantFood(Guid id)
        {
            var restaurantFood = await _bll.RestaurantFoods.FirstOrDefaultAsync(id);
            if (restaurantFood == null)
            {
                return NotFound();
            }

            await _bll.RestaurantFoods.RemoveAsync(restaurantFood);
            await _bll.SaveChangesAsync();

            return Ok(restaurantFood);
        }
    }
}
