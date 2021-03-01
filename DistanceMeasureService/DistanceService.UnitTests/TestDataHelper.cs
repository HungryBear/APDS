using System;
using DistanceService.Domain;

namespace DistanceService.UnitTests
{
    public static class TestDataHelper
    {
        static readonly Random Rnd = new Random();

        public static double GetLat()
        {
            return GetValue(LatLongCoordinates.MinLat, LatLongCoordinates.MaxLat);
        }

        public static double GetLong()
        {
            return GetValue(LatLongCoordinates.MinLat, LatLongCoordinates.MaxLat);
        }

        static double GetValue(double min, double max)
        {
            return min + Rnd.NextDouble() * max;
        }
    }
}