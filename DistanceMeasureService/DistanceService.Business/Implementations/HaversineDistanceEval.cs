using DistanceService.Domain;

namespace DistanceService.Business.Implementations
{
    internal class HaversineDistanceEval: IDistanceEvaluationComponent
    {
        public double EvalDistance(ref LatLongCoordinates src, ref LatLongCoordinates dst)
        {
            return Eval.HaversineDistance(ref src, ref dst);
        }
    }
}
