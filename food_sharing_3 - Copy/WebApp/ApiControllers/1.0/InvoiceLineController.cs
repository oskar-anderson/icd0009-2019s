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
    public class InvoiceLineController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly InvoiceLineMapper _mapper = new InvoiceLineMapper();

        public InvoiceLineController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/InvoiceLine
        [HttpGet]
        public async Task<ActionResult<IEnumerable<V1DTO.InvoiceLineDTO>>> GetInvoiceLines()
        {
            return Ok(await _bll.InvoiceLines.GetAllAsyncBase());
        }

        // GET: api/InvoiceLine/5
        [HttpGet("{id}")]
        public async Task<ActionResult<V1DTO.InvoiceLineDTO>> GetInvoiceLine(Guid id)
        {
            var invoiceLine = await _bll.InvoiceLines.FirstOrDefaultAsync(id);
            
            if (invoiceLine == null)
            {
                return NotFound(new V1DTO.MessageDTO($"GetInvoiceLine with id {id} not found"));
            }

            return Ok(invoiceLine);
        }

        // PUT: api/InvoiceLine/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvoiceLine(Guid id, InvoiceLineDTO invoiceLine)
        {
            if (id != invoiceLine.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and invoiceLine.id do not match"));
            }

            await _bll.InvoiceLines.UpdateAsync(_mapper.Map(invoiceLine));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/InvoiceLine
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<V1DTO.InvoiceLineDTO>> PostInvoiceLine(InvoiceLineDTO invoiceLine)
        {
            var bllEntity = _mapper.Map(invoiceLine);
            _bll.InvoiceLines.Add(bllEntity);
            await _bll.SaveChangesAsync();
            invoiceLine.Id = bllEntity.Id;

            return CreatedAtAction("GetInvoiceLine", new { id = invoiceLine.Id }, invoiceLine);
        }

        // DELETE: api/InvoiceLine/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<V1DTO.InvoiceLineDTO>> DeleteInvoiceLine(Guid id)
        {
            var invoiceLine = await _bll.InvoiceLines.FirstOrDefaultAsync(id);
            if (invoiceLine == null)
            {
                return NotFound();
            }

            await _bll.InvoiceLines.RemoveAsync(invoiceLine);
            await _bll.SaveChangesAsync();

            return Ok(invoiceLine);
        }

    }
}
