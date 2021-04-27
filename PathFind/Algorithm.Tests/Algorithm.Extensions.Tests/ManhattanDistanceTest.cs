using System;
using Algorithm.Realizations.HeuristicFunctions;
using GraphLib.Exceptions;
using GraphLib.Interfaces;
using Moq;
using NUnit.Framework;

namespace Algorithm.Extensions.Tests
{
    [TestFixture]
    public class ManhattanDistanceTest
    {
        private readonly Mock<IVertex> vertexFromMock;
        private readonly Mock<IVertex> vertexToMock;
        private readonly Mock<ICoordinate> coordinateFromMock;
        private readonly Mock<ICoordinate> coordinateToMock;
        private readonly ManhattanDistance manhattanDistance;

        private IVertex FirstVertex => vertexFromMock.Object;
        private IVertex SecondVertex => vertexToMock.Object;

        public ManhattanDistanceTest()
        {
            vertexFromMock = new Mock<IVertex>();
            vertexToMock = new Mock<IVertex>();
            coordinateFromMock = new Mock<ICoordinate>();
            coordinateToMock = new Mock<ICoordinate>();
            manhattanDistance = new ManhattanDistance();
        }

        [TestCase(new[] { 3, 15 }, new[] { 4, 6 }, ExpectedResult = 10)]
        [TestCase(new[] { 7, 1, 20 }, new[] { 5, 9, 1 }, ExpectedResult = 29)]
        [TestCase(new[] { 5 }, new[] { 33 }, ExpectedResult = 28)]
        [TestCase(new[] { 4, 7, 3, 20 }, new[] { 1, 5, 16, 9 }, ExpectedResult = 29)]
        public double CalculateChebyshevDistanceTo_EqualNumberOfCoordinateValues_ReturnsValidValue(
            int[] fromVertexCoordinateValues, int[] toVertexCoordinateValues)
        {
            coordinateFromMock.Setup(coordinate => coordinate.CoordinatesValues).Returns(fromVertexCoordinateValues);
            vertexFromMock.Setup(vertex => vertex.Position).Returns(coordinateFromMock.Object);
            coordinateToMock.Setup(coordinate => coordinate.CoordinatesValues).Returns(toVertexCoordinateValues);
            vertexToMock.Setup(vertex => vertex.Position).Returns(coordinateToMock.Object);

            return manhattanDistance.Calculate(FirstVertex, SecondVertex);
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
            void CalculateChebyshevDistance() => manhattanDistance.Calculate(FirstVertex, SecondVertex);

            Assert.Throws<WrongNumberOfDimensionsException>(CalculateChebyshevDistance);
        }

        [Test]
        public void CalculateChebyshevDistanceTo_CoordinatesAreNull_ThrowsArgumentException()
        {
            void CalculateChebyshevDistance() => manhattanDistance.Calculate(FirstVertex, SecondVertex);

            Assert.Throws<ArgumentException>(CalculateChebyshevDistance);
        }
    }
}