using System;

namespace DistanceService.Domain
{
    /// <summary>
    /// Describes latitude-longitude coordinates of the point on sphere
    /// </summary>
    public readonly struct LatLongCoordinates : IEquatable<LatLongCoordinates>
    {
        /*
Google maps values
Latitude: --85.05115 to +85
Longitude: -180 to +180
*/
        public static readonly double MinLat = -85.05115d;
        public static readonly double MaxLat = 85.0d;
        public static readonly double MinLon = -180.0d;
        public static readonly double MaxLon = 180d;

        internal static readonly double LatLongEpsilon = 1e8;

        /// <summary>
        /// Latitude value
        /// </summary>
        public double Latitude { get; }
        /// <summary>
        /// Longitude value
        /// </summary>
        public double Longitude { get; }

        internal LatLongCoordinates(double lat, double lon)
        {
            this.Latitude = lat;
            this.Longitude = lon;
        }

        /// <summary>
        /// Constructs new instance of the LatLongCoordinates 
        /// </summary>
        /// <param name="lat"> Latitude value, should be between -85.0 and 85.0</param>
        /// <param name="lon"> Longitude value, should be between -180.0 and 180.0</param>
        /// <returns></returns>
        public static LatLongCoordinates New(double lat, double lon)
        {
            if (lat > MaxLat || lat < MinLat || double.IsNaN(lat))
            {
                throw new ArgumentException("Invalid lat value", nameof(lat));
            }
            if (lon > MaxLon || lon < MinLon || double.IsNaN(lon))
            {
                throw new ArgumentException("Invalid lon value", nameof(lon));
            }

            return new LatLongCoordinates(lat, lon);
        }

        public override string ToString()
        {
            return $"[ Lat : {Latitude:F8} | Lon : {Longitude:F8} ]";
        }

        public bool Equals(LatLongCoordinates other)
        {
            return Latitude.NearEqual(other.Latitude, LatLongEpsilon) && Longitude.NearEqual(other.Longitude, LatLongEpsilon);
        }

        public override bool Equals(object obj)
        {
            return obj is LatLongCoordinates other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Latitude, Longitude);
        }
    }
}
