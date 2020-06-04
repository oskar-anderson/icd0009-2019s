using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Domain.Base.App.EF;
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
    /// CartMeal Api Controller
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class CartMealController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly CartMealMapper _mapper = new CartMealMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public CartMealController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get a list of all the CartMeals
        /// </summary>
        /// <returns>List of CartMeals</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CartMealDTO>))]
        public async Task<ActionResult<IEnumerable<V1DTO.CartMealDTO>>> GetCartMeals()
        {
            return Ok((await _bll.CartMeals.GetAllForApiAsync()).Select(e => _mapper.MapCartMealView(e)));
        }

        /// <summary>
        /// Get single CartMeal
        /// </summary>
        /// <param name="id">CartMeal Id</param>
        /// <returns>request CartMeal</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.CartMealDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.CartMealDTO>> GetCartMeal(Guid id)
        {
            var cartMeal = await _bll.CartMeals.FirstOrDefaultApiAsync(id);
            
            if (cartMeal == null)
            {
                return NotFound(new V1DTO.MessageDTO($"GetCartMeal with id {id} not found"));
            }

            return Ok(_mapper.MapCartMealView(cartMeal));
        }

        /// <summary>
        /// Update the CartMeal
        /// </summary>
        /// <param name="id">CartMeal id</param>
        /// <param name="cartMeal">CartMeal object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutCartMeal(Guid id, CartMealDTO cartMeal)
        {
            if (id != cartMeal.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and cartMeal.id do not match"));
            }

            await _bll.CartMeals.UpdateAsync(_mapper.Map(cartMeal));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create a new CartMeal
        /// </summary>
        /// <param name="cartMeal">CartMeal object</param>
        /// <returns>created CartMeal object</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.CartMealDTO))]
        public async Task<ActionResult<V1DTO.CartMealDTO>> PostCartMeal(CartMealDTO cartMeal)
        {
            var bllEntity = _mapper.Map(cartMeal);
            _bll.CartMeals.Add(bllEntity);
            await _bll.SaveChangesAsync();
            cartMeal.Id = bllEntity.Id;

            return CreatedAtAction("GetCartMeal",
                new { id = cartMeal.Id , version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"}, 
                cartMeal);
        }

        /// <summary>
        /// Delete the CartMeal
        /// </summary>
        /// <param name="id">CartMeal Id</param>
        /// <returns>deleted CartMeal object</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.CartMealDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.CartMealDTO>> DeleteCartMeal(Guid id)
        {
            var cartMeal = await _bll.CartMeals.FirstOrDefaultApiAsync(id);
            if (cartMeal == null)
            {
                return NotFound(new {message = "CartMeal not found"});
            }

            await _bll.CartMeals.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(cartMeal);
        }
    }
}
