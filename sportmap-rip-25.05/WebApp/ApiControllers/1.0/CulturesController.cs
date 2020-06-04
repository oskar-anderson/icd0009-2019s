using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using PublicApi.DTO.v1;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Culture info
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class CulturesController : ControllerBase
    {
        private readonly IOptions<RequestLocalizationOptions> _localizationOptions;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="localizationOptions"></param>
        public CulturesController(IOptions<RequestLocalizationOptions> localizationOptions)
        {
            _localizationOptions = localizationOptions;
        }

        /// <summary>
        /// Get the list of supported cultures
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CultureDTO>))]
        public ActionResult<IEnumerable<CultureDTO>> GetCultures()
        {
            var result = _localizationOptions.Value.SupportedUICultures
                .Select(c => new CultureDTO()
                {
                    Code = c.Name,
                    Name = c.NativeName,
                }).ToList();

            return Ok(result);
        }

        /// <summary>
        /// Get the resource strings and keys
        /// </summary>
        /// <returns></returns>
        [HttpGet("resources")]
        public ActionResult<IEnumerable<string>> GetResources()
        {
            
            var res = new List<CultureDTO>();
            var resourceSet =
                Resources.Views.Shared._Layout.ResourceManager
                    .GetResourceSet(Thread.CurrentThread.CurrentUICulture,
                        true, true);
                        
            if (resourceSet == null)
            {
                return Ok(res);
            }

            foreach (DictionaryEntry? entry in resourceSet)
            {
                if (entry==null) continue;
                res.Add(new CultureDTO() {Name = entry.Value.Value!.ToString()!, Code = entry.Value.Key.ToString()!});
            }
            return Ok(res);
        }
    }
}