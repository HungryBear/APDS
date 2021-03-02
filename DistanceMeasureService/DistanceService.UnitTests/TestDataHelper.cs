using System;
using System.Collections.Generic;
using DistanceService.Domain;

namespace DistanceService.UnitTests
{
    public static class TestDataHelper
    {
        static readonly Random Rnd = new Random();

        public static string[] IATACodes = { "LAX", "JFK", "SFO", "SVO", "DME", "LED", "MRV", "AGP", "BCN", "MAD", "AMS", "EIN", "MST" };

        public static Dictionary<string, double> PrecomputedDistancesInMiles = new Dictionary<string, double>()
        {
            {"LAX-JFK", 2469.45 },
            {"BCN-LED", 1753.4 },
            {"AGP-AMS", 1170.0 },
        };

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