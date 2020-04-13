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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using PublicApi.DTO.v1;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RestaurantController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public RestaurantController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/Restaurant
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestaurantDTO>>> GetRestaurants()
        {
            return Ok(await _uow.Restaurants.DTOAllAsync());
        }

        // GET: api/Restaurant/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RestaurantDTO>> GetRestaurant(Guid id)
        {
            var restaurantDTO = await _uow.Restaurants.DTOFirstOrDefaultAsync(id, User.UserGuidId());

            if (restaurantDTO == null)
            {
                return NotFound();
            }

            return Ok(restaurantDTO);
        }

        // PUT: api/Restaurant/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRestaurant(Guid id, Restaurant restaurantEditDTO)
        {
            if (id != restaurantEditDTO.Id)
            {
                return BadRequest();
            }

            var restaurant = await _uow.Restaurants.FirstOrDefaultAsync(restaurantEditDTO.Id, User.UserGuidId());
            if (restaurant == null)
            {
                return BadRequest();
            }

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _uow.Restaurants.ExistsAsync(id, User.UserGuidId()))
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

        // POST: api/Restaurant
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Restaurant>> PostRestaurant(Restaurant restaurant)
        {
            _uow.Restaurants.Add(restaurant);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetRestaurant", new { id = restaurant.Id }, restaurant);
        }

        // DELETE: api/Restaurant/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Restaurant>> DeleteRestaurant(Guid id)
        {
            var restaurant = await _uow.Restaurants.FirstOrDefaultAsync(id, User.UserGuidId());
            if (restaurant == null)
            {
                return NotFound();
            }

            _uow.Restaurants.Remove(restaurant);
            await _uow.SaveChangesAsync();

            return Ok(restaurant);
        }
    }
}
