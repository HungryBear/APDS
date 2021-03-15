using System;
using System.Collections.Generic;
using DistanceService.Domain;
using Microsoft.Extensions.Logging;

namespace DistanceService.Business
{
    public class DistanceMeasureService
    {
        private readonly IDistanceEvaluationComponent _distanceEval;
        private readonly IAirportDataStorage _airportDict;
        private readonly ILogger<DistanceMeasureService> _logger;

        public DistanceMeasureService(IDistanceEvaluationComponent distanceEval, IAirportDataStorage airportDict, ILogger<DistanceMeasureService> logger)
        {
            _distanceEval = distanceEval;
            _airportDict = airportDict;
            _logger = logger;
        }

        public double EvaluateDistance(string srcIata, string dstIata)
        {
            if (string.IsNullOrWhiteSpace(srcIata))
                throw new ArgumentNullException(nameof(srcIata));
            if (string.IsNullOrWhiteSpace(dstIata))
                throw new ArgumentNullException(nameof(dstIata));
            var normalizedSource = srcIata.ToUpperInvariant();

            if (!_airportDict.Query(normalizedSource, out var srcAirport))
            {
                throw new ArgumentException($"Invalid source IATA code = {normalizedSource}",
                    nameof(srcIata));
            }

            var normalizedDest = dstIata.ToUpperInvariant();

            if (!_airportDict.Query(normalizedDest, out var dstAirport))
            {
                throw new ArgumentException($"Invalid destination IATA code = {normalizedDest}",
                    nameof(dstIata));
            }

            try
            {
                return _distanceEval.EvalDistance(ref srcAirport, ref dstAirport);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error evaluating distance {srcIata}-{dstIata}.");
                throw;
            }
        }

        public IAsyncEnumerable<string> GetAllCodes()
        {
            return _airportDict.GetAllCodes();
        }
    }
}