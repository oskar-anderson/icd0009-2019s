using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Domain.Base.App.EF;
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
    
    public class PizzaUserController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PizzaUserMapper _mapper = new PizzaUserMapper();

        public PizzaUserController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/PizzaUser
        [HttpGet]
        public async Task<ActionResult<IEnumerable<V1DTO.PizzaUserDTO>>> GetPizzaUsers()
        {
            return Ok(await _bll.PizzaUsers.GetAllForApiAsync(User.UserId()));
        }

        // GET: api/PizzaUser/5
        [HttpGet("{id}")]
        public async Task<ActionResult<V1DTO.PizzaUserDTO>> GetPizzaUser(Guid id)
        {
            var pizzaUser = await _bll.PizzaUsers.FirstOrDefaultApiAsync(id);
            
            if (pizzaUser == null)
            {
                return NotFound(new V1DTO.MessageDTO($"GetPizzaUser with id {id} not found"));
            }

            return Ok(pizzaUser);
        }

        // PUT: api/PizzaUser/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPizzaUser(Guid id, PizzaUserDTO pizzaUser)
        {
            if (id != pizzaUser.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and pizzaUser.id do not match"));
            }

            await _bll.PizzaUsers.UpdateAsync(_mapper.Map(pizzaUser));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/PizzaUser
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<V1DTO.PizzaUserDTO>> PostPizzaUser(PizzaUserDTO pizzaUser)
        {
            var bllEntity = _mapper.Map(pizzaUser);
            _bll.PizzaUsers.Add(bllEntity);
            await _bll.SaveChangesAsync();
            pizzaUser.Id = bllEntity.Id;

            return CreatedAtAction("GetPizzaUser", new { id = pizzaUser.Id }, pizzaUser);
        }

        // DELETE: api/PizzaUser/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<V1DTO.PizzaUserDTO>> DeletePizzaUser(Guid id)
        {
            var pizzaUser = await _bll.PizzaUsers.FirstOrDefaultApiAsync(id);
            if (pizzaUser == null)
            {
                return NotFound();
            }

            await _bll.PizzaUsers.RemoveAsync(pizzaUser);
            await _bll.SaveChangesAsync();

            return Ok(pizzaUser);
        }
    }
}
