using System;

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
        /// <returns> Distance in meters </returns>
        public static double Haversine(ref LatLongCoordinates src, ref LatLongCoordinates dst)
        {
            const double R = 6371e3; // earth radius in m
            var f1 = src.Latitude * PiDiv180;
            var f2 = dst.Latitude * PiDiv180;
            var df = (dst.Latitude - src.Latitude) * PiDiv180;
            var dl = (dst.Longitude - src.Longitude) * PiDiv180;

            var a = Math.Sin(df / 2d) * Math.Sin(df / 2d) +
                    Math.Cos(f1) * Math.Cos(f2) *
                    Math.Sin(dl / 2d) * Math.Sin(dl / 2d);
            var c = 2d * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1d - a));

            var d = R * c; // in m
            return d;
        }
    }
}
