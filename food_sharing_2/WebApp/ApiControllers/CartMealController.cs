using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartMealController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CartMealController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/CartMeal
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartMeal>>> GetCartMeals()
        {
            return await _context.CartMeals.ToListAsync();
        }

        // GET: api/CartMeal/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CartMeal>> GetCartMeal(Guid id)
        {
            var cartMeal = await _context.CartMeals.FindAsync(id);

            if (cartMeal == null)
            {
                return NotFound();
            }

            return cartMeal;
        }

        // PUT: api/CartMeal/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCartMeal(Guid id, CartMeal cartMeal)
        {
            if (id != cartMeal.Id)
            {
                return BadRequest();
            }

            _context.Entry(cartMeal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartMealExists(id))
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

        // POST: api/CartMeal
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<CartMeal>> PostCartMeal(CartMeal cartMeal)
        {
            _context.CartMeals.Add(cartMeal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCartMeal", new { id = cartMeal.Id }, cartMeal);
        }

        // DELETE: api/CartMeal/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CartMeal>> DeleteCartMeal(Guid id)
        {
            var cartMeal = await _context.CartMeals.FindAsync(id);
            if (cartMeal == null)
            {
                return NotFound();
            }

            _context.CartMeals.Remove(cartMeal);
            await _context.SaveChangesAsync();

            return cartMeal;
        }

        private bool CartMealExists(Guid id)
        {
            return _context.CartMeals.Any(e => e.Id == id);
        }
    }
}
