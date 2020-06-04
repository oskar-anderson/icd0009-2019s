using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
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
    /// Cart Api Controller
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class CartController : ControllerBase
    {
        // private readonly IAppUnitOfWork _uow;
        private readonly IAppBLL _bll;
        private readonly CartMapper _mapper = new CartMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public CartController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get the list of User Carts
        /// </summary>
        /// <returns>List of user carts</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CartDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<IEnumerable<V1DTO.CartDTO>>> GetCarts()
        {
            return Ok((await _bll.Carts.GetAllForApiAsync(User.UserId())).Select(e => _mapper.MapCartView(e)));
        }

        /// <summary>
        /// Get a single Cart
        /// </summary>
        /// <param name="id">id for Cart</param>
        /// <returns>Cart</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.CartDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.CartDTO>> GetCart(Guid id)
        {
            var cart = await _bll.Carts.FirstOrDefaultApiAsync(id, User.UserId());
            
            if (cart == null)
            {
                return NotFound(new V1DTO.MessageDTO($"GetCart with id {id} not found"));
            }

            return Ok(_mapper.MapCartView(cart));
        }

        /// <summary>
        /// Update Cart
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cart"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutCart(Guid id, CartDTO cart)
        {
            cart.AppUserId = User.UserId();

            if (id != cart.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and cart.id do not match"));
            }

            await _bll.Carts.UpdateAsync(_mapper.Map(cart), User.UserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create/add a new Cart
        /// </summary>
        /// <param name="cart">Cart info</param>
        /// <returns>created Cart object</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.CartDTO))]
        [HttpPost]
        public async Task<ActionResult<V1DTO.CartDTO>> PostCart(CartDTO cart)
        {
            cart.AppUserId = User.UserId();
            
            var bllEntity = _mapper.Map(cart);
            _bll.Carts.Add(bllEntity);
            await _bll.SaveChangesAsync();
            cart.Id = bllEntity.Id;

            return CreatedAtAction("GetCart",
                new { id = cart.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"}, 
                cart);
        }

        /// <summary>
        /// Delete the Cart
        /// </summary>
        /// <param name="id">Cart Id</param>
        /// <returns>deleted Cart object</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.CartDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.CartDTO>> DeleteCart(Guid id)
        {
            var cart = await _bll.Carts.FirstOrDefaultApiAsync(id, User.UserId());
            if (cart == null)
            {
                return NotFound(new {message = "Cart not found"});
            }

            await _bll.Carts.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(cart);
        }
    }
}
