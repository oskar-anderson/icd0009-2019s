using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.BLL.App;
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
    public class CartController : ControllerBase
    {
        // private readonly IAppUnitOfWork _uow;
        private readonly IAppBLL _bll;
        private readonly CartMapper _mapper = new CartMapper();

        public CartController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Cart
        [HttpGet]
        public async Task<ActionResult<IEnumerable<V1DTO.CartDTO>>> GetCarts()
        {
            return Ok(await _bll.Carts.GetAllAsyncBase(User.UserId()));
        }

        // GET: api/Cart/5
        [HttpGet("{id}")]
        public async Task<ActionResult<V1DTO.CartDTO>> GetCart(Guid id)
        {
            var cart = await _bll.Carts.FirstOrDefaultAsync(id, User.UserId());
            
            if (cart == null)
            {
                return NotFound(new V1DTO.MessageDTO($"GetCart with id {id} not found"));
            }

            return Ok(cart);
        }

        // PUT: api/Cart/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
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

        // POST: api/Cart
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<V1DTO.CartDTO>> PostCart(CartDTO cart)
        {
            var bllEntity = _mapper.Map(cart);
            _bll.Carts.Add(bllEntity);
            await _bll.SaveChangesAsync();
            cart.Id = bllEntity.Id;

            return CreatedAtAction("GetCart", new { id = cart.Id }, cart);
        }

        // DELETE: api/Cart/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<V1DTO.CartDTO>> DeleteCart(Guid id)
        {
            var cart = await _bll.Carts.FirstOrDefaultAsync(id, User.UserId());
            if (cart == null)
            {
                return NotFound();
            }

            await _bll.Carts.RemoveAsync(cart);
            await _bll.SaveChangesAsync();

            return Ok(cart);
        }
    }
}
