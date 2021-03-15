using System;
using System.Runtime.CompilerServices;

namespace DistanceService.Domain
{
    public static class Eval
    {
        private const double PiDiv180 = Math.PI / 180d;

        /// <summary>
        /// Evaluates angular distance (on the surface of a sphere) between two points, described with latitude-longitude coordinates
        /// </summary>
        /// <param name="src"> Source lat-long point</param>
        /// <param name="dst"> Destination lat-long point</param>
        /// <param name="earthRadius"> Earth radius. The value of this constant controls the measurement units of the output, i.e. if it is specified in meters the output value will be in meters. Default parameter value is in miles - 3960.</param>
        /// <returns> Distance in the units specified by the earthRadius param, miles by default</returns>
        public static double HaversineDistance(ref LatLongCoordinates src, ref LatLongCoordinates dst, double earthRadius = 3960d)
        {
            var R = earthRadius;
            var dLat = ToRadians(dst.Latitude - src.Latitude);
            var dLon = ToRadians(dst.Longitude - src.Longitude);

            var a = Math.Sin(dLat / 2d) * Math.Sin(dLat / 2d) +
                       Math.Cos(ToRadians(src.Latitude)) * Math.Cos(ToRadians(dst.Latitude)) *
                       Math.Sin(dLon / 2d) * Math.Sin(dLon / 2d);
            var c = 2d * Math.Asin(Math.Min(1d, Math.Sqrt(a)));
            var d = R * c;

            return d;
        }


        public static double VincentyDistance(ref LatLongCoordinates src, ref LatLongCoordinates dst,
            double earthRadius = 3960d)
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// Convert angle value from degrees to radians.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double ToRadians(double angle)
        {
            return angle * PiDiv180;
        }

    }
}
