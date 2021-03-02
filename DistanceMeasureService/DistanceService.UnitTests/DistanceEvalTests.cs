using DistanceService.Domain;
using Xunit;

namespace DistanceService.UnitTests
{
    public class DistanceEvalTests
    {

        [Theory]
        [InlineData(1f, 1f, 1f, 1f, 0f, 1e-10d)]
        public void TestRealDistanceEvaluations(double srcLat, double srcLong, double dstLat, double dstLong, double expectedDistance, double epsilon)
        {
            //WPK - DTW , 
            // double lat1 = 144.0019989013672, lon1 = -16.658300399780273, lat2 = -83.35340118408203, lon2 = 42.212398529052734;

            var src = LatLongCoordinates.New(srcLat, srcLong);
            var dst = LatLongCoordinates.New(dstLat, dstLong);
            Assert.True(Eval.Haversine(ref src, ref dst).NearEqual(expectedDistance, epsilon));
        }

        [Fact]
        public void TestZeroDistance()
        {
            var p1 = new LatLongCoordinates();
            var p2 = new LatLongCoordinates();

            Assert.True(Eval.Haversine(ref p1, ref p2).NearEqual(0));
        }
    }
}
