using System;
using DistanceService.Domain;

namespace DistanceService.Business
{
    public class DistanceMeasureService
    {
        private readonly IDistanceEvaluationComponent _distanceEval;
        private readonly IAirportDataStorage _airportDict;

        public DistanceMeasureService(IDistanceEvaluationComponent distanceEval, IAirportDataStorage airportDict)
        {
            _distanceEval = distanceEval;
            _airportDict = airportDict;
        }

        public double Eval(string srcIata, string dstIata)
        {
            if (!_airportDict.Query(srcIata, out var srcAirport))
            {
                throw new ArgumentException($"Invalid source IATA code = {srcIata}", nameof(srcIata));
            }

            if (!_airportDict.Query(dstIata, out var dstAirport))
            {
                throw new ArgumentException($"Invalid destination IATA code = {dstIata}", nameof(dstIata));
            }

            return _distanceEval.EvalDistance(ref srcAirport, ref dstAirport);
        }
    }
}