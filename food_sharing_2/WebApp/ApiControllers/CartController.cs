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
    public class CartController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public CartController(AppDbContext context, IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/Cart
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartDTO>>> GetCarts()
        {
            return Ok(await _uow.Carts.DTOAllAsync(User.UserGuidId()));
        }

        // GET: api/Cart/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CartDTO>> GetCart(Guid id)
        {
            var cart = await _uow.Carts.DTOFirstOrDefaultAsync(id, User.UserGuidId());
            
            if (cart == null)
            {
                return NotFound();
            }

            return cart;
        }

        // PUT: api/Cart/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCart(Guid id, CartDTO cartDTO)
        {
            if (id != cartDTO.Id)
            {
                return BadRequest();
            }

            //_context.Entry(cart).State = EntityState.Modified;
            
            Cart cart = await _uow.Carts.FirstOrDefaultAsync(cartDTO.Id);

            if (cart == null)
            {
                return BadRequest();
            }
            
            
            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _uow.Carts.ExistsAsync(id, User.UserGuidId()))
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

        // POST: api/Cart
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Cart>> PostCart(Cart cart)
        {
            _uow.Carts.Add(cart);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetCart", new { id = cart.Id }, cart);
        }

        // DELETE: api/Cart/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Cart>> DeleteCart(Guid id)
        {
            var cart = await _uow.Carts.FirstOrDefaultAsync(id, User.UserGuidId());
            if (cart == null)
            {
                return NotFound();
            }

            _uow.Carts.Remove(cart);
            await _uow.SaveChangesAsync();

            return cart;
        }
    }
}
