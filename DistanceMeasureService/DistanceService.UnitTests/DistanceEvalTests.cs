using DistanceService.Domain;
using Xunit;

namespace DistanceService.UnitTests
{
    public class DistanceEvalTests
    {

        [Theory]
        [InlineData(41.385064, 2.173403, 59.802913, 30.267839, 1746, 0.5d)] // LED-BCN, error ~ 0.5
        public void TestRealDistanceEvaluations(double srcLat, double srcLong, double dstLat, double dstLong, double expectedDistance, double epsilon)
        {
            var src = LatLongCoordinates.New(srcLat, srcLong);
            var dst = LatLongCoordinates.New(dstLat, dstLong);
            var distance = Eval.HaversineDistance(ref src, ref dst);
            Assert.True(distance.NearEqual(expectedDistance, epsilon));
        }

        [Fact]
        public void TestZeroDistance()
        {
            var p1 = new LatLongCoordinates();
            var p2 = new LatLongCoordinates();

            Assert.True(Eval.HaversineDistance(ref p1, ref p2).NearEqual(0));
        }
    }
}
