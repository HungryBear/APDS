using DistanceService.Domain;
using Xunit;

namespace DistanceService.UnitTests
{
    public class DistanceEvalTests
    {

        [Fact]
        public void TestRealDistanceEvaluations()
        {
            //WPK - DTW , 
            // double lat1 = 144.0019989013672, lon1 = -16.658300399780273, lat2 = -83.35340118408203, lon2 = 42.212398529052734;
            var data = new[]{LatLongCoordinates.New(10f, 3f), LatLongCoordinates.New(4f, 5f)};

        }

        [Fact]
        public void TestZeroDistance()
        {
            var p1 = new LatLongCoordinates();
            var p2 = new LatLongCoordinates();

            Assert.True(Eval.Haversine(ref p1, ref p2).NearEqual(0)));
        }
    }
}
