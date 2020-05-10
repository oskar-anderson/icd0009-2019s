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
    [Authorize(Roles = "admin")]
    public class RestaurantController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly RestaurantMapper _mapper = new RestaurantMapper();

        public RestaurantController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Restaurant
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<V1DTO.RestaurantDTO>>> GetRestaurants()
        {
            return Ok(await _bll.Restaurants.GetAllAsyncBase());
        }

        // GET: api/Restaurant/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<RestaurantDTO>> GetRestaurant(Guid id)
        {
            var restaurantDTO = await _bll.Restaurants.FirstOrDefaultAsync(id);

            if (restaurantDTO == null)
            {
                return NotFound(new V1DTO.MessageDTO($"GetRestaurant with id {id} not found"));
            }

            return Ok(restaurantDTO);
        }

        // PUT: api/Restaurant/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRestaurant(Guid id, RestaurantDTO restaurant)
        {
            if (id != restaurant.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and restaurant.id do not match"));
            }

            await _bll.Restaurants.UpdateAsync(_mapper.Map(restaurant));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Restaurant
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<V1DTO.RestaurantDTO>> PostRestaurant(RestaurantDTO restaurant)
        {
            var bllEntity = _mapper.Map(restaurant);
            _bll.Restaurants.Add(bllEntity);
            await _bll.SaveChangesAsync();
            restaurant.Id = bllEntity.Id;

            return CreatedAtAction("GetRestaurant", new { id = restaurant.Id }, restaurant);
        }

        // DELETE: api/Restaurant/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<V1DTO.RestaurantDTO>> DeleteRestaurant(Guid id)
        {
            var restaurantFood = await _bll.Restaurants.FirstOrDefaultAsync(id);
            if (restaurantFood == null)
            {
                return NotFound();
            }

            await _bll.Restaurants.RemoveAsync(restaurantFood);
            await _bll.SaveChangesAsync();

            return Ok(restaurantFood);
        }
    }
}
