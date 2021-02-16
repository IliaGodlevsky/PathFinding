using GraphLib.Interface;
using Moq;
using NUnit.Framework;

namespace Algorithm.Extensions.Tests
{
    [TestFixture]
    public class IVertexExtensionsTests
    {
        private readonly Mock<IVertex> vertexFromMock;
        private readonly Mock<IVertex> vertexToMock;
        private readonly Mock<ICoordinate> coordinateFromMock;
        private readonly Mock<ICoordinate> coordinateToMock;

        public IVertexExtensionsTests()
        {
            vertexFromMock = new Mock<IVertex>();
            vertexToMock = new Mock<IVertex>();
            coordinateFromMock = new Mock<ICoordinate>();
            coordinateToMock = new Mock<ICoordinate>();
        }

        [TestCase(new int[] {3, 15 }, new int[] { 4, 6 }, ExpectedResult = 9)]
        [TestCase(new int[] { 7, 1, 20 }, new int[] { 5, 9, 1 }, ExpectedResult = 19)]
        [TestCase(new int[] { 5 }, new int[] { 33 }, ExpectedResult = 28)]
        [TestCase(new int[] { 4, 7, 3, 20 }, new int[] { 1, 5, 16, 9 }, ExpectedResult = 13)]
        public double CalculateChebyshevDistanceTo_EqualNumberOfCoordinateValues_ReturnsValidValue(
            int[] fromVertexCoordinateValues, int[] toVertexCoordinateValues)
        {
            coordinateFromMock.Setup(coordinate => coordinate.CoordinatesValues).Returns(fromVertexCoordinateValues);
            vertexFromMock.Setup(vertex => vertex.Position).Returns(coordinateFromMock.Object);
            coordinateToMock.Setup(coordinate => coordinate.CoordinatesValues).Returns(toVertexCoordinateValues);
            vertexToMock.Setup(vertex => vertex.Position).Returns(coordinateToMock.Object);

            return vertexFromMock.Object.CalculateChebyshevDistanceTo(vertexToMock.Object);
        }
    }
}