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
using PublicApi.DTO.v1.Mappers;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;
using V1DTO = PublicApi.DTO.v1;

namespace WebApp.ApiControllers._1._0
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PaymentMethodController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PaymentMethodMapper _mapper = new PaymentMethodMapper();

        public PaymentMethodController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/PaymentMethod
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<V1DTO.PaymentMethodDTO>>> GetPaymentMethods()
        {
            return Ok(await _bll.PaymentMethods.GetAllAsyncBase());
        }

        // GET: api/PaymentMethod/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<V1DTO.PaymentMethodDTO>> GetPaymentMethod(Guid id)
        {
            var paymentMethod = await _bll.PaymentMethods.FirstOrDefaultAsync(id);

            if (paymentMethod == null)
            {
                return NotFound();
            }

            return Ok(paymentMethod);
        }

        // PUT: api/PaymentMethod/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaymentMethod(Guid id, PaymentMethodDTO paymentMethod)
        {
            if (id != paymentMethod.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and paymentMethod.id do not match"));
            }

            await _bll.PaymentMethods.UpdateAsync(_mapper.Map(paymentMethod));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/PaymentMethod
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<V1DTO.PaymentMethodDTO>> PostPaymentMethod(PaymentMethodDTO paymentMethod)
        {
            var bllEntity = _mapper.Map(paymentMethod);
            _bll.PaymentMethods.Add(bllEntity);
            await _bll.SaveChangesAsync();
            paymentMethod.Id = bllEntity.Id;

            return CreatedAtAction("GetPaymentMethod", new { id = paymentMethod.Id }, paymentMethod);
        }

        // DELETE: api/PaymentMethod/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<V1DTO.PaymentMethodDTO>> DeletePaymentMethod(Guid id)
        {
            var paymentMethod = await _bll.PaymentMethods.FirstOrDefaultAsync(id);
            if (paymentMethod == null)
            {
                return NotFound();
            }

            await _bll.PaymentMethods.RemoveAsync(paymentMethod);
            await _bll.SaveChangesAsync();

            return Ok(paymentMethod);
        }
    }
}
