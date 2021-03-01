namespace DistanceService.Domain
{
    /// <summary>
    /// Encapsulates evaluation of the distance between two lat-long points
    /// </summary>
    public interface IDistanceEvaluationComponent
    {
        /// <summary>
        /// Evaluates angular distance between two lat-long points
        /// </summary>
        /// <param name="src"> Source point</param>
        /// <param name="dst"> Destination point</param>
        /// <returns> Distance in miles </returns>
        double EvalDistance(ref LatLongCoordinates src, ref LatLongCoordinates dst);
    }
}