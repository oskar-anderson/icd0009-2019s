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
    /// RestaurantFood Api Controller
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(Roles = "admin")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class RestaurantFoodController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly RestaurantFoodMapper _mapper = new RestaurantFoodMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public RestaurantFoodController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get a list of all the RestaurantFoods
        /// </summary>
        /// <returns>List of RestaurantFoods</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.RestaurantFoodDTO>))]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<V1DTO.RestaurantFoodDTO>>> GetRestaurantFoods()
        {
            return Ok((await _bll.RestaurantFoods.GetAllForApiAsync()).Select(e => _mapper.MapRestaurantFoodView(e)));
        }

        /// <summary>
        /// Get single RestaurantFood
        /// </summary>
        /// <param name="id">RestaurantFood Id</param>
        /// <returns>request RestaurantFood</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.RestaurantFoodDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.RestaurantFoodDTO>> GetRestaurantFood(Guid id)
        {
            var restaurantFood = await _bll.RestaurantFoods.FirstOrDefaultApiAsync(id);
            
            if (restaurantFood == null)
            {
                return NotFound(new V1DTO.MessageDTO($"GetRestaurantFood with id {id} not found"));
            }

            return Ok(_mapper.MapRestaurantFoodView(restaurantFood));
        }

        /// <summary>
        /// Update the RestaurantFood
        /// </summary>
        /// <param name="id">RestaurantFood id</param>
        /// <param name="restaurantFood">RestaurantFood object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
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

        /// <summary>
        /// Create a new RestaurantFood
        /// </summary>
        /// <param name="restaurantFood">RestaurantFood object</param>
        /// <returns>created RestaurantFood object</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.RestaurantFoodDTO))]
        public async Task<ActionResult<V1DTO.RestaurantFoodDTO>> PostRestaurantFood(RestaurantFoodDTO restaurantFood)
        {
            var bllEntity = _mapper.Map(restaurantFood);
            _bll.RestaurantFoods.Add(bllEntity);
            await _bll.SaveChangesAsync();
            restaurantFood.Id = bllEntity.Id;

            return CreatedAtAction("GetRestaurantFood",
                new { id = restaurantFood.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"}, 
                restaurantFood);
        }

        /// <summary>
        /// Delete the RestaurantFood
        /// </summary>
        /// <param name="id">RestaurantFood Id</param>
        /// <returns>deleted RestaurantFood object</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.RestaurantFoodDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.RestaurantFoodDTO>> DeleteRestaurantFood(Guid id)
        {
            var restaurantFood = await _bll.RestaurantFoods.FirstOrDefaultApiAsync(id);
            if (restaurantFood == null)
            {
                return NotFound(new {message = "RestaurantFood not found"});
            }

            await _bll.RestaurantFoods.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(restaurantFood);
        }
    }
}
