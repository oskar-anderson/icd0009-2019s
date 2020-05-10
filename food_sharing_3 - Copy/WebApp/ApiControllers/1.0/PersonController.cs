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
    public class PersonController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PersonMapper _mapper = new PersonMapper();

        public PersonController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Person
        [HttpGet]
        public async Task<ActionResult<IEnumerable<V1DTO.PersonDTO>>> GetPersons()
        {
            return Ok(await _bll.Persons.GetAllAsyncBase(User.UserId()));
        }

        // GET: api/Person/5
        [HttpGet("{id}")]
        public async Task<ActionResult<V1DTO.PersonDTO>> GetPerson(Guid id)
        {
            var person = await _bll.Persons.FirstOrDefaultAsync(id, User.UserId());
            
            if (person == null)
            {
                return NotFound(new V1DTO.MessageDTO($"GetPerson with id {id} not found"));
            }

            return Ok(person);
        }

        // PUT: api/Person/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(Guid id, PersonDTO person)
        {
            person.AppUserId = User.UserId();

            if (id != person.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and person.id do not match"));
            }

            await _bll.Persons.UpdateAsync(_mapper.Map(person), User.UserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Person
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<V1DTO.PersonDTO>> PostPerson(PersonDTO person)
        {
            var bllEntity = _mapper.Map(person);
            _bll.Persons.Add(bllEntity);
            await _bll.SaveChangesAsync();
            person.Id = bllEntity.Id;

            return CreatedAtAction("GetPerson", new { id = person.Id }, person);
        }

        // DELETE: api/Person/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<V1DTO.PersonDTO>> DeletePerson(Guid id)
        {
            var person = await _bll.Persons.FirstOrDefaultAsync(id, User.UserId());
            if (person == null)
            {
                return NotFound();
            }

            await _bll.Persons.RemoveAsync(person);
            await _bll.SaveChangesAsync();

            return Ok(person);
        }
    }
}
