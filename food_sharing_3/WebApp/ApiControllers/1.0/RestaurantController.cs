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
    /// Restaurant Api Controller
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(Roles = "admin")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class RestaurantController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly RestaurantMapper _mapper = new RestaurantMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public RestaurantController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get a list of all the Restaurants
        /// </summary>
        /// <returns>List of Restaurants</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.RestaurantDTO>))]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<V1DTO.RestaurantDTO>>> GetRestaurants()
        {
            return Ok((await _bll.Restaurants.GetAllAsyncBase()).Select(e => _mapper.Map(e)));
        }

        /// <summary>
        /// Get single Restaurant
        /// </summary>
        /// <param name="id">Restaurant Id</param>
        /// <returns>request Restaurant</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.RestaurantDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<RestaurantDTO>> GetRestaurant(Guid id)
        {
            var restaurantDTO = await _bll.Restaurants.FirstOrDefaultAsync(id);

            if (restaurantDTO == null)
            {
                return NotFound(new V1DTO.MessageDTO($"GetRestaurant with id {id} not found"));
            }

            return Ok(_mapper.Map(restaurantDTO));
        }

        /// <summary>
        /// Update the Restaurant
        /// </summary>
        /// <param name="id">Restaurant id</param>
        /// <param name="restaurant">Restaurant object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
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

        /// <summary>
        /// Create a new Restaurant
        /// </summary>
        /// <param name="restaurant">Restaurant object</param>
        /// <returns>created Restaurant object</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.RestaurantDTO))]
        public async Task<ActionResult<V1DTO.RestaurantDTO>> PostRestaurant(RestaurantDTO restaurant)
        {
            var bllEntity = _mapper.Map(restaurant);
            _bll.Restaurants.Add(bllEntity);
            await _bll.SaveChangesAsync();
            restaurant.Id = bllEntity.Id;

            return CreatedAtAction("GetRestaurant",
                new { id = restaurant.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"}, 
                restaurant);
        }

        /// <summary>
        /// Delete the Restaurant
        /// </summary>
        /// <param name="id">Restaurant Id</param>
        /// <returns>deleted Restaurant object</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.RestaurantDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.RestaurantDTO>> DeleteRestaurant(Guid id)
        {
            var restaurantFood = await _bll.Restaurants.FirstOrDefaultAsync(id);
            if (restaurantFood == null)
            {
                return NotFound(new {message = "Restaurant not found"});
            }

            await _bll.Restaurants.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(restaurantFood);
        }
    }
}
