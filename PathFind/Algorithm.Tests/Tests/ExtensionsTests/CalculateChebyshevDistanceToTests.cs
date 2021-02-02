using Algorithm.Extensions;
using GraphLib.Interface;
using Moq;
using NUnit.Framework;
using System;

namespace Algorithm.Tests.Tests.ExtensionsTests
{
    [TestFixture]
    public class CalculateChebyshevDistanceToTests
    {
        private Mock<IVertex> firstVertexMock;
        private Mock<IVertex> secondVertexMock;
        private Mock<ICoordinate> firstVertexCoordinateMock;
        private Mock<ICoordinate> secondVertexCoordinateMock;

        [SetUp]
        public void SetUp()
        {
            firstVertexMock = new Mock<IVertex>(MockBehavior.Loose);
            secondVertexMock = new Mock<IVertex>(MockBehavior.Loose);
            firstVertexCoordinateMock = new Mock<ICoordinate>(MockBehavior.Loose);
            secondVertexCoordinateMock = new Mock<ICoordinate>(MockBehavior.Loose);
        }

        [TestCase(new int[] { 9, 15 }, new int[] { 12, 8 }, ExpectedResult = 7)]
        [TestCase(new int[] { 4, 9, 25 }, new int[] { 11, 13, 24 }, ExpectedResult = 7)]
        [TestCase(new int[] { 15, 37, 25, 1 }, new int[] { 16, 34, 11, 9 }, ExpectedResult = 14)]
        public double CalculateChebyshevDistanceTo_VerticesWithSameDimensions_ReturnsValidDistance(
            int[] firstVertexCoordinates, int[] secondVertexCoordinates)
        {
            firstVertexCoordinateMock.Setup(coordinate => coordinate.CoordinatesValues).Returns(firstVertexCoordinates);
            secondVertexCoordinateMock.Setup(coordinate => coordinate.CoordinatesValues).Returns(secondVertexCoordinates);
            firstVertexMock.Setup(vertex => vertex.Position).Returns(firstVertexCoordinateMock.Object);
            secondVertexMock.Setup(vertex => vertex.Position).Returns(secondVertexCoordinateMock.Object);
            var first = firstVertexMock.Object;
            var second = secondVertexMock.Object;

            return first.CalculateChebyshevDistanceTo(second);
        }

        [Test]
        public void CalculateChebyshevDistanceTo_ArgumentIsNull_ThrowsArgumentNullException()
        {
            firstVertexMock.Setup(vertex => vertex.Position).Returns(firstVertexCoordinateMock.Object);
            var first = firstVertexMock.Object;

            Assert.Throws<ArgumentNullException>(() => first.CalculateChebyshevDistanceTo(null));
        }

        [Test]
        public void CalculateChebyshevDistanceTo_CallerIsNull_ThrowsArgumentNullException()
        {
            var second = secondVertexMock.Object;
            IVertex first = null;

            Assert.Throws<ArgumentNullException>(() => first.CalculateChebyshevDistanceTo(second));
        }

        [Test]
        public void CalculateChebyshevDistanceTo_CallerPositionIsNull_ThrowsArgumentException()
        {
            var first = firstVertexMock.Object;
            var second = secondVertexMock.Object;

            Assert.Throws<ArgumentException>(() => first.CalculateChebyshevDistanceTo(second));
        }

        [Test]
        public void CalculateChebyshevDistanceTo_ArgumentPositionIsNull_ThrowsArgumentException()
        {
            var first = firstVertexMock.Object;
            var second = secondVertexMock.Object;

            Assert.Throws<ArgumentException>(() => first.CalculateChebyshevDistanceTo(second));
        }

        [TestCase(new int[] { 9, 15 }, new int[] { 12, 8, 3 })]
        [TestCase(new int[] { 4 }, new int[] { 11, 13, 24 })]
        [TestCase(new int[] { 15, 37, 25, 1 }, new int[] { 16, 34, 11, 9, 15, 17 })]
        public void CalculateChebyshevDistanceTo_DifferentPositionCoorindatesCount_ThrowsArgumentException(
            int[] firstVertexCoordinates, int[] secondVertexCoordinates)
        {
            firstVertexCoordinateMock.Setup(coordinate => coordinate.CoordinatesValues).Returns(firstVertexCoordinates);
            secondVertexCoordinateMock.Setup(coordinate => coordinate.CoordinatesValues).Returns(secondVertexCoordinates);
            firstVertexMock.Setup(vertex => vertex.Position).Returns(firstVertexCoordinateMock.Object);
            secondVertexMock.Setup(vertex => vertex.Position).Returns(secondVertexCoordinateMock.Object);
            var first = firstVertexMock.Object;
            var second = secondVertexMock.Object;

            Assert.Throws<ArgumentException>(() => first.CalculateChebyshevDistanceTo(second));
        }
    }
}
