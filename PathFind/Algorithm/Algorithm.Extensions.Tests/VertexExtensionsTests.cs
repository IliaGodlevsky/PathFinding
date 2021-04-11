using GraphLib.Exceptions;
using GraphLib.Interface;
using Moq;
using NUnit.Framework;
using System;

namespace Algorithm.Extensions.Tests
{
    [TestFixture]
    public class VertexExtensionsTests
    {
        private readonly Mock<IVertex> vertexFromMock;
        private readonly Mock<IVertex> vertexToMock;
        private readonly Mock<ICoordinate> coordinateFromMock;
        private readonly Mock<ICoordinate> coordinateToMock;

        public VertexExtensionsTests()
        {
            vertexFromMock = new Mock<IVertex>();
            vertexToMock = new Mock<IVertex>();
            coordinateFromMock = new Mock<ICoordinate>();
            coordinateToMock = new Mock<ICoordinate>();
        }

        [TestCase(new[] { 3, 15 }, new[] { 4, 6 }, ExpectedResult = 9)]
        [TestCase(new[] { 7, 1, 20 }, new[] { 5, 9, 1 }, ExpectedResult = 19)]
        [TestCase(new[] { 5 }, new[] { 33 }, ExpectedResult = 28)]
        [TestCase(new[] { 4, 7, 3, 20 }, new[] { 1, 5, 16, 9 }, ExpectedResult = 13)]
        public double CalculateChebyshevDistanceTo_EqualNumberOfCoordinateValues_ReturnsValidValue(
            int[] fromVertexCoordinateValues, int[] toVertexCoordinateValues)
        {
            coordinateFromMock.Setup(coordinate => coordinate.CoordinatesValues).Returns(fromVertexCoordinateValues);
            vertexFromMock.Setup(vertex => vertex.Position).Returns(coordinateFromMock.Object);
            coordinateToMock.Setup(coordinate => coordinate.CoordinatesValues).Returns(toVertexCoordinateValues);
            vertexToMock.Setup(vertex => vertex.Position).Returns(coordinateToMock.Object);

            return vertexFromMock.Object.CalculateChebyshevDistanceTo(vertexToMock.Object);
        }

        [TestCase(new[] { 3, 15 }, new[] { 4 })]
        [TestCase(new[] { 7, 1, 20 }, new[] { 9, 1 })]
        [TestCase(new[] { 5, 1 }, new[] { 33 })]
        [TestCase(new[] { 4, 7, 3, 20 }, new[] { 1, 16, 9 })]
        public void CalculateChebyshevDistanceTo_NotEqualNumberOfCoordinateValues_ThrowsWrongNumberOfDimensionsException(
            int[] fromVertexCoordinateValues, int[] toVertexCoordinateValues)
        {
            coordinateFromMock.Setup(coordinate => coordinate.CoordinatesValues).Returns(fromVertexCoordinateValues);
            vertexFromMock.Setup(vertex => vertex.Position).Returns(coordinateFromMock.Object);
            coordinateToMock.Setup(coordinate => coordinate.CoordinatesValues).Returns(toVertexCoordinateValues);
            vertexToMock.Setup(vertex => vertex.Position).Returns(coordinateToMock.Object);
            void CalculateChebyshevDistance() => vertexFromMock.Object.CalculateChebyshevDistanceTo(vertexToMock.Object);

            Assert.Throws<WrongNumberOfDimensionsException>(CalculateChebyshevDistance);
        }

        [Test]
        public void CalculateChebyshevDistanceTo_CoordinatesAreNull_ThrowsArgumentException()
        {
            void CalculateChebyshevDistance() => vertexFromMock.Object.CalculateChebyshevDistanceTo(vertexToMock.Object);

            Assert.Throws<ArgumentException>(CalculateChebyshevDistance);
        }
    }
}