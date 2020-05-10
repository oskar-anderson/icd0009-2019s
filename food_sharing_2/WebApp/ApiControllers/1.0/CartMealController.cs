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
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CartMealController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly CartMealMapper _mapper = new CartMealMapper();

        public CartMealController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/CartMeal
        [HttpGet]
        public async Task<ActionResult<IEnumerable<V1DTO.CartMealDTO>>> GetCartMeals()
        {
            return Ok(await _bll.CartMeals.GetAllAsyncBase());
        }

        // GET: api/CartMeal/5
        [HttpGet("{id}")]
        public async Task<ActionResult<V1DTO.CartMealDTO>> GetCartMeal(Guid id)
        {
            var cartMeal = await _bll.CartMeals.FirstOrDefaultAsync(id);
            
            if (cartMeal == null)
            {
                return NotFound(new V1DTO.MessageDTO($"GetCartMeal with id {id} not found"));
            }

            return Ok(cartMeal);
        }

        // PUT: api/CartMeal/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
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

        // POST: api/CartMeal
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<V1DTO.CartMealDTO>> PostCartMeal(CartMealDTO cartMeal)
        {
            var bllEntity = _mapper.Map(cartMeal);
            _bll.CartMeals.Add(bllEntity);
            await _bll.SaveChangesAsync();
            cartMeal.Id = bllEntity.Id;

            return CreatedAtAction("GetCartMeal", new { id = cartMeal.Id }, cartMeal);
        }

        // DELETE: api/CartMeal/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<V1DTO.CartMealDTO>> DeleteCartMeal(Guid id)
        {
            var cartMeal = await _bll.CartMeals.FirstOrDefaultAsync(id);
            if (cartMeal == null)
            {
                return NotFound();
            }

            await _bll.CartMeals.RemoveAsync(cartMeal);
            await _bll.SaveChangesAsync();

            return Ok(cartMeal);
        }
    }
}
