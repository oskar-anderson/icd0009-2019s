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
    public class ComponentPizzaUserController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ComponentPizzaUserMapper _mapper = new ComponentPizzaUserMapper();

        public ComponentPizzaUserController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/PizzaComponent
        [HttpGet]
        public async Task<ActionResult<IEnumerable<V1DTO.ComponentPizzaUserDTO>>> GetPizzaComponents()
        {
            return Ok(await _bll.ComponentPizzaUsers.GetAllForApiAsync());
        }

        // GET: api/PizzaComponent/5
        [HttpGet("{id}")]
        public async Task<ActionResult<V1DTO.ComponentPizzaUserDTO>> GetPizzaComponent(Guid id)
        {
            var pizzaComponent = await _bll.ComponentPizzaUsers.FirstOrDefaultApiAsync(id);
            
            if (pizzaComponent == null)
            {
                return NotFound(new V1DTO.MessageDTO($"GetPizzaComponent with id {id} not found"));
            }

            return Ok(pizzaComponent);
        }

        // PUT: api/PizzaComponent/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPizzaComponent(Guid id, ComponentPizzaUserDTO pizzaComponent)
        {
            if (id != pizzaComponent.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and pizzaComponent.id do not match"));
            }

            await _bll.ComponentPizzaUsers.UpdateAsync(_mapper.Map(pizzaComponent));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/PizzaComponent
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<V1DTO.ComponentPizzaUserDTO>> PostPizzaComponent(ComponentPizzaUserDTO pizzaComponent)
        {
            var bllEntity = _mapper.Map(pizzaComponent);
            _bll.ComponentPizzaUsers.Add(bllEntity);
            await _bll.SaveChangesAsync();
            pizzaComponent.Id = bllEntity.Id;

            return CreatedAtAction("GetPizzaComponent", new { id = pizzaComponent.Id }, pizzaComponent);
        }

        // DELETE: api/PizzaComponent/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<V1DTO.ComponentPizzaUserDTO>> DeletePizzaComponent(Guid id)
        {
            var pizzaComponent = await _bll.ComponentPizzaUsers.FirstOrDefaultApiAsync(id);
            if (pizzaComponent == null)
            {
                return NotFound();
            }

            await _bll.ComponentPizzaUsers.RemoveAsync(pizzaComponent);
            await _bll.SaveChangesAsync();

            return Ok(pizzaComponent);
        }
    }
}
