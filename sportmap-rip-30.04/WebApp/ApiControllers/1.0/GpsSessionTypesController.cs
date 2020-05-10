using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PublicApi.DTO.v1.Mappers;
using V1DTO=PublicApi.DTO.v1;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// GpsSessionTypes Api Controller
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class GpsSessionTypesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly GpsSessionTypeMapper _mapper = new GpsSessionTypeMapper();
        
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public GpsSessionTypesController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get the list of all the GpsSessionType-s
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.GpsSessionType>))]
        public async Task<ActionResult<IEnumerable<V1DTO.GpsSessionType>>> GetGpsSessionTypes()
        {
            return Ok((await _bll.GpsSessionTypes.GetAllAsyncBase()).Select(e => _mapper.Map(e)));
        }

        /// <summary>
        /// Get single GpsSessionType
        /// </summary>
        /// <param name="id">Guid id of item to get</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.GpsSessionType))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.GpsSessionType>> GetGpsSessionType(Guid id)
        {
            
            var gpsSessionType = await _bll.GpsSessionTypes.FirstOrDefaultAsync(id);

            if (gpsSessionType == null)
            {
                return NotFound(new V1DTO.MessageDTO("GpsSessionType not found"));
            }

            return Ok(_mapper.Map(gpsSessionType));
        }

        /// <summary>
        /// update GpsSessionType
        /// </summary>
        /// <param name="id">Item id</param>
        /// <param name="gpsSessionType">GpsSessionType</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutGpsSessionType(Guid id, V1DTO.GpsSessionType gpsSessionType)
        {
            if (id != gpsSessionType.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and gpsSessionType.id do not match"));
            }

            await _bll.GpsSessionTypes.UpdateAsync(_mapper.Map(gpsSessionType));
            await _bll.SaveChangesAsync();

            return NoContent();
       
        }
        
        /// <summary>
        /// Create new GpsSessionType 
        /// </summary>
        /// <param name="gpsSessionType">GpsSessionType to create</param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.GpsSessionType))]
        public async Task<ActionResult<V1DTO.GpsSessionType>> PostGpsSessionType(V1DTO.GpsSessionType gpsSessionType)
        {
            
            var bllEntity = _mapper.Map(gpsSessionType);
            _bll.GpsSessionTypes.Add(bllEntity);
            await _bll.SaveChangesAsync();
            gpsSessionType.Id = bllEntity.Id;

            return CreatedAtAction("GetGpsSessionType",
                new {id = gpsSessionType.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                gpsSessionType);
        }

        /// <summary>
        /// Delete GpsSessionType
        /// </summary>
        /// <param name="id">Guid id of item to delete</param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.GpsSessionType))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.GpsSessionType>> DeleteGpsSessionType(Guid id)
        {
            var gpsSessionType = await _bll.GpsSessionTypes.FirstOrDefaultAsync(id);
            if (gpsSessionType == null)
            {
                return NotFound(new V1DTO.MessageDTO("GpsSessionType not found"));
            }

            await _bll.GpsSessionTypes.RemoveAsync(gpsSessionType);
            await _bll.SaveChangesAsync();

            return Ok(gpsSessionType);
        }


    }
}
