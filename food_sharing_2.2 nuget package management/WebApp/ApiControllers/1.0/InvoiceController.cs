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
    public class InvoiceController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly InvoiceMapper _mapper = new InvoiceMapper();

        public InvoiceController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Invoice
        [HttpGet]
        public async Task<ActionResult<IEnumerable<V1DTO.InvoiceDTO>>> GetInvoices()
        {
            return Ok(await _bll.Invoices.GetAllAsyncBase());
        }

        // GET: api/Invoice/5
        [HttpGet("{id}")]
        public async Task<ActionResult<V1DTO.InvoiceDTO>> GetInvoice(Guid id)
        {
            var invoice = await _bll.Invoices.FirstOrDefaultAsync(id);
            
            if (invoice == null)
            {
                return NotFound(new V1DTO.MessageDTO($"GetInvoice with id {id} not found"));
            }

            return Ok(invoice);
        }

        // PUT: api/Invoice/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvoice(Guid id, InvoiceDTO invoice)
        {
            if (id != invoice.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and invoice.id do not match"));
            }

            await _bll.Invoices.UpdateAsync(_mapper.Map(invoice));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Invoice
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<V1DTO.InvoiceDTO>> PostInvoice(InvoiceDTO invoice)
        {
            var bllEntity = _mapper.Map(invoice);
            _bll.Invoices.Add(bllEntity);
            await _bll.SaveChangesAsync();
            invoice.Id = bllEntity.Id;

            return CreatedAtAction("GetInvoices", new { id = invoice.Id }, invoice);
        }

        // DELETE: api/Invoice/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<V1DTO.InvoiceDTO>> DeleteInvoice(Guid id)
        {
            var invoice = await _bll.Invoices.FirstOrDefaultAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }

            await _bll.Invoices.RemoveAsync(invoice);
            await _bll.SaveChangesAsync();

            return Ok(invoice);
        }
    }
}
