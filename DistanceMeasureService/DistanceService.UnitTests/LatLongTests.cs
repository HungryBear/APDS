using DistanceService.Domain;
using Xunit;

namespace DistanceService.UnitTests
{
    public class LatLongTests
    {

        [Fact]
        public void TestZeroDistance()
        {
            var data = new[] { LatLongCoordinates.New(10f, 3f), LatLongCoordinates.New(4f, 5f) };

        }
    }
}