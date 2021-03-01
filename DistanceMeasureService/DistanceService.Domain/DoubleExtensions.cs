using System;

namespace DistanceService.Domain
{
    public static class DoubleExtensions
    {
        public static bool NearEqual(this double f, double arg, double epsilon = 1e-8)
        {
            return
                Math.Abs(f - arg) <= epsilon;
        }
    }
}