using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Domain;
using Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;

namespace WebApp.ApiControllers._1._0
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}[controller]")]
    [ApiController]
    public class RestaurantFoodController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public RestaurantFoodController(IAppUnitOfWork uow)
        {
            _uow = uow;;
        }

        // GET: api/RestaurantFood
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestaurantFood>>> GetRestaurantFoods()
        {
            return Ok(await _uow.RestaurantFoods.DTOAllAsync(User.UserGuidId()));
        }

        // GET: api/RestaurantFood/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RestaurantFood>> GetRestaurantFood(Guid id)
        {
            var restaurantFood = await _uow.RestaurantFoods.DTOFirstOrDefaultAsync(id, User.UserGuidId());
            
            if (restaurantFood == null)
            {
                return NotFound();
            }

            return Ok(restaurantFood);
        }

        // PUT: api/RestaurantFood/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRestaurantFood(Guid id, RestaurantFoodDTO restaurantFoodDTO)
        {
            if (id != restaurantFoodDTO.Id)
            {
                return BadRequest();
            }
            
            // _context.Entry(restaurantFood).State = EntityState.Modified;
            RestaurantFood restaurantFood = await _uow.RestaurantFoods.FirstOrDefaultAsync(restaurantFoodDTO.Id);
            
            if (restaurantFood == null)
            {
                return BadRequest();
            }

            _uow.RestaurantFoods.Update(restaurantFood);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _uow.RestaurantFoods.ExistsAsync(id, User.UserGuidId()))
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

        // POST: api/RestaurantFood
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<RestaurantFood>> PostRestaurantFood(RestaurantFood restaurantFood)
        {
            _uow.RestaurantFoods.Add(restaurantFood);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetRestaurantFood", new { id = restaurantFood.Id }, restaurantFood);
        }

        // DELETE: api/RestaurantFood/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RestaurantFood>> DeleteRestaurantFood(Guid id)
        {
            var restaurantFood = await _uow.RestaurantFoods.FirstOrDefaultAsync(id, User.UserGuidId());
            if (restaurantFood == null)
            {
                return NotFound();
            }

            _uow.RestaurantFoods.Remove(restaurantFood);
            await _uow.SaveChangesAsync();

            return Ok(restaurantFood);
        }
    }
}
