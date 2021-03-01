namespace DistanceService.Domain
{
    public interface IDistanceEvaluationComponent
    {
        double EvalDistance(ref LatLongCoordinates src, ref LatLongCoordinates dst);
    }
}