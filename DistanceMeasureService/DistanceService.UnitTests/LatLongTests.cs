using System;
using DistanceService.Domain;
using Xunit;

namespace DistanceService.UnitTests
{
    public class LatLongTests
    {
        [Fact]
        public void TestCreateAndEquals()
        {
            var lat = TestDataHelper.GetLat();
            var lon = TestDataHelper.GetLong();
            var item = new LatLongCoordinates(lat, lon);
            Assert.True(item.Latitude.NearEqual(lat));
            Assert.True(item.Longitude.NearEqual(lon));
            Assert.True(item.Equals(new LatLongCoordinates(lat, lon)));
        }

        [Fact]
        public void TestOutOfRangeValues()
        {
            var lat = LatLongCoordinates.MaxLat + 1d;
            var lon = TestDataHelper.GetLong();
            Assert.Throws<ArgumentException>(() => LatLongCoordinates.New(lat, lon));

            lat = LatLongCoordinates.MinLat-1d;
            lon = TestDataHelper.GetLong();
            Assert.Throws<ArgumentException>(() => LatLongCoordinates.New(lat, lon));

            lat = TestDataHelper.GetLat();
            lon = LatLongCoordinates.MinLon - 1d;
            Assert.Throws<ArgumentException>(() => LatLongCoordinates.New(lat, lon));

            lon = LatLongCoordinates.MaxLon + 1d;
            Assert.Throws<ArgumentException>(() => LatLongCoordinates.New(lat, lon));

            lon = double.NaN;
            Assert.Throws<ArgumentException>(() => LatLongCoordinates.New(lat, lon));
        }
    }
}