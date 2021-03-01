using DistanceService.Domain;
using Xunit;

namespace DistanceService.UnitTests
{
    public class LatLongTests
    {
        [Fact]
        public void TestCreate()
        {
            var lat = TestDataHelper.GetLat();
            var lon = TestDataHelper.GetLong();
            var item = new LatLongCoordinates(lat, lon);
            Assert.True(item.Latitude.NearEqual(lat));
            Assert.True(item.Longitude.NearEqual(lon));
            Assert.True(item.Equals(new LatLongCoordinates(lat, lon)));
        }
    }
}