using System;
using System.Collections.Generic;
using System.Text;
using DistanceService.Business;
using DistanceService.Domain;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Moq;
using Xunit;

namespace DistanceService.UnitTests
{
    public class DistanceMeasureServiceTests
    {
        [Fact]
        public void TestNormalFlow()
        {
            var svc = CreateDistanceEvalComponent(out var dataComponent, out var distanceEvalComponent);
            dataComponent.Setup(m => m.Query(It.IsAny<string>(), out It.Ref<LatLongCoordinates>.IsAny)).Returns(() => true);

            svc.Eval("ABC", "CBA");
            dataComponent.Verify(m => m.Query(It.IsAny<string>(), out It.Ref<LatLongCoordinates>.IsAny), Times.Exactly(2));
            distanceEvalComponent.Verify(m => m.EvalDistance(ref It.Ref<LatLongCoordinates>.IsAny, ref It.Ref<LatLongCoordinates>.IsAny), Times.Once);
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
