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

        [HttpGet("{src}/{dst}")]
        public double GetDistance(string src,  string dst)
        {
            var result = _distanceMeasureService.Eval(src, dst);
            _logger.LogInformation($"Request evaluation : Distance {src}-{dst} = {result:F7} miles");
            return result;
        }
    }
}
