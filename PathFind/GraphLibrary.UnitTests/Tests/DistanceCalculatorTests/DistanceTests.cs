using Algorithm.Сalculations;
using GraphLib.Coordinates;
using GraphLib.UnitTests.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GraphLib.UnitTests.Tests.DistanceCalculatorTests
{
    [TestClass]
    public class DistanceTests
    {
        [TestMethod]
        public void GetChebyshevDistance_NotNullVertex_ReturnsChebyshevDistance()
        {
            var vertex1 = new TestVertex();
            var vertex2 = new TestVertex();
            vertex1.Position = new Coordinate2D(1, 1);
            vertex2.Position = new Coordinate2D(2, 2);
            var expectedDistance = 1.0;

            var realDistance =
                DistanceCalculator.GetChebyshevDistance(vertex1, vertex2);

            Assert.AreEqual(expectedDistance, realDistance);
        }
    }
}
