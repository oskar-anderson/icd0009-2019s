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
    public class UserLocationController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserLocationController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/UserLocation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserLocation>>> GetUserLocations()
        {
            return await _context.UserLocations.ToListAsync();
        }

        // GET: api/UserLocation/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserLocation>> GetUserLocation(Guid id)
        {
            var userLocation = await _context.UserLocations.FindAsync(id);

            if (userLocation == null)
            {
                return NotFound();
            }

            return userLocation;
        }

        // PUT: api/UserLocation/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserLocation(Guid id, UserLocation userLocation)
        {
            if (id != userLocation.Id)
            {
                return BadRequest();
            }

            _context.Entry(userLocation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserLocationExists(id))
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

        // POST: api/UserLocation
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<UserLocation>> PostUserLocation(UserLocation userLocation)
        {
            _context.UserLocations.Add(userLocation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserLocation", new { id = userLocation.Id }, userLocation);
        }

        // DELETE: api/UserLocation/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserLocation>> DeleteUserLocation(Guid id)
        {
            var userLocation = await _context.UserLocations.FindAsync(id);
            if (userLocation == null)
            {
                return NotFound();
            }

            _context.UserLocations.Remove(userLocation);
            await _context.SaveChangesAsync();

            return userLocation;
        }

        private bool UserLocationExists(Guid id)
        {
            return _context.UserLocations.Any(e => e.Id == id);
        }
    }
}
