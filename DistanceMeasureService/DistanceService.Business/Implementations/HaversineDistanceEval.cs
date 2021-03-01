﻿using DistanceService.Domain;

namespace DistanceService.Business.Implementations
{
    //TODO Can be made internal, with InternalsVisibleToAttribute used for UT

    public class HaversineDistanceEval: IDistanceEvaluationComponent
    {
        public double EvalDistance(ref LatLongCoordinates src, ref LatLongCoordinates dst)
        {
            var distanceInMeters =  Eval.Haversine(ref src, ref dst);
            return distanceInMeters / 1000d * 0.62137119;
        }
    }
}