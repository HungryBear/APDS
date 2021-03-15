using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DistanceService.Business;
using Microsoft.Extensions.Logging;

namespace DistanceService.Rest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DistanceController : ControllerBase
    {
        private readonly ILogger<DistanceController> _logger;
        private readonly DistanceMeasureService _distanceMeasureService;

        public DistanceController(ILogger<DistanceController> logger, DistanceMeasureService distanceMeasureService)
        {
            _logger = logger;
            _distanceMeasureService = distanceMeasureService;
        }

        /// <summary>
        /// Evaluates distance in miles between two airports, airports are described by IATA codes
        /// </summary>
        /// <param name="src">Source airport IATA code</param>
        /// <param name="dst">Destination airport IATA code</param>
        /// <returns>Distance in miles between source and destination</returns>
        [HttpGet("/eval")]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 600, VaryByQueryKeys = new[] { "src", "dst" })]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(double), 200)]
        public double GetDistance([FromQuery] string src, [FromQuery] string dst)
        {

            var result = _distanceMeasureService.EvaluateDistance(src, dst);
            _logger.LogInformation($"Request evaluation : Distance {src}-{dst} = {result:F7} miles");
            return result;

        }

        /// <summary>
        /// Returns all stored IATA codes
        /// </summary>
        /// <returns>An array of 3-Letter IATA codes for all airports stored in the cache.</returns>
        [HttpGet("/iata")]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 600)]
        [ProducesResponseType(typeof(IEnumerable<string>), 200)]
        public async Task<IEnumerable<string>> GetIataCodes()
        {
            _logger.LogInformation($"All IATA Codes requested.");
            var res = new List<string>();
            await foreach (var code in _distanceMeasureService.GetAllCodes())
            {
                res.Add(code);
            }

            return res;
        }
    }
}
