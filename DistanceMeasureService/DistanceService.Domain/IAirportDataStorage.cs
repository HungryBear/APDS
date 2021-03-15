using System.Collections.Generic;

namespace DistanceService.Domain
{
    /// <summary>
    /// Describes airport data dictionary, with IATA airport code as an unique key and lat-long coordinates as the value
    /// </summary>
    public interface IAirportDataStorage
    {
        /// <summary>
        /// Queries airport dictionary and returns coordinates of the airport described by IATA code argument
        /// </summary>
        /// <param name="iataCode"> Airport IATA code </param>
        /// <param name="coords"> Resulting lat-long coordinates </param>
        /// <returns>True if query is successful - in that case coords are initialized, false otherwise</returns>
        bool Query(string iataCode, out LatLongCoordinates coords);

        IAsyncEnumerable<string> GetAllCodes();
        //System.Threading.ValueTask<(bool, LatLongCoordinates)> QueryAsync(string iataCode)
    }
}