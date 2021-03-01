namespace DistanceService.Domain
{
    public interface IAirportDataStorage
    {
        bool Query(string iataCode, out LatLongCoordinates coords);
        //System.Threading.ValueTask<(bool, LatLongCoordinates)> QueryAsync(string iataCode)
    }
}