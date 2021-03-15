using System;
using DistanceService.Business;
using DistanceService.Domain;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace DistanceService.UnitTests
{
    public class DistanceMeasureServiceTests
    {
        [Fact]
        public void TestNormalFlow_Mocks()
        {
            var svc = CreateDistanceEvalComponent(out var dataComponent, out var distanceEvalComponent);
            dataComponent.Setup(m => m.Query(It.IsAny<string>(), out It.Ref<LatLongCoordinates>.IsAny)).Returns(() => true);

            svc.EvaluateDistance("ABC", "CBA");
            dataComponent.Verify(m => m.Query(It.IsAny<string>(), out It.Ref<LatLongCoordinates>.IsAny), Times.Exactly(2));
            distanceEvalComponent.Verify(m => m.EvalDistance(ref It.Ref<LatLongCoordinates>.IsAny, ref It.Ref<LatLongCoordinates>.IsAny), Times.Once);
        }


        [Fact]
        public void TestArgumentNullExceptions_Mocks()
        {
            var svc = CreateDistanceEvalComponent(out var dataComponent, out var distanceEvalComponent);
            dataComponent.Setup(m => m.Query(It.IsAny<string>(), out It.Ref<LatLongCoordinates>.IsAny)).Returns(() => true);
            Assert.Throws<ArgumentNullException>(() => svc.EvaluateDistance(null, "CBA"));
            Assert.Throws<ArgumentNullException>(() => svc.EvaluateDistance("ABC", null));
            Assert.Throws<ArgumentNullException>(() => svc.EvaluateDistance(string.Empty, "CBA"));
            Assert.Throws<ArgumentNullException>(() => svc.EvaluateDistance("ABC", string.Empty));
        }

        [Fact]
        public void TestEmptyIATAQueryExceptions_Mocks()
        {
            var svc = CreateDistanceEvalComponent(out var dataComponent, out var distanceEvalComponent);
            dataComponent.Setup(m => m.Query(It.IsAny<string>(), out It.Ref<LatLongCoordinates>.IsAny)).Returns(() => false);
            Assert.Throws<ArgumentException>(() => svc.EvaluateDistance(null, "CBA"));
            Assert.Throws<ArgumentNullException>(() => svc.EvaluateDistance("ABC", null));
            Assert.Throws<ArgumentNullException>(() => svc.EvaluateDistance(string.Empty, "CBA"));
            Assert.Throws<ArgumentNullException>(() => svc.EvaluateDistance("ABC", string.Empty));
        }

        private static DistanceMeasureService CreateDistanceEvalComponent(out Mock<IAirportDataStorage> dataComponent, out Mock<IDistanceEvaluationComponent> svc)
        {
            svc = new Mock<IDistanceEvaluationComponent>();
            dataComponent = new Mock<IAirportDataStorage>();
            var logger = new Mock<ILogger<DistanceMeasureService>>();
            return  new DistanceMeasureService(svc.Object, dataComponent.Object, logger.Object);
        }

        [Fact]
        public void TestExceptions()
        {

        }
    }
}
