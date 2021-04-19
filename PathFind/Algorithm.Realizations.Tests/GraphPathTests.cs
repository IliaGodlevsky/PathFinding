using System.Collections.Generic;
using GraphLib.Interfaces;
using Moq;
using NUnit.Framework;

namespace Algorithm.Realizations.Tests
{
    [TestFixture]
    public class GraphPathTests
    {
        private Dictionary<ICoordinate, IVertex> parentVertices;
        private Mock<IEndPoints> endPointsMock;

        private List<IVertex> expectedPath;

        private List<IVertex> vertex1Neighbours;
        private List<IVertex> vertex2Neighbours;
        private List<IVertex> vertex3Neighbours;
        private List<IVertex> vertex4Neighbours;
        private List<IVertex> vertex5Neighbours;

        private IEndPoints EndPoints => endPointsMock.Object;

        public GraphPathTests()
        {
            parentVertices = new Dictionary<ICoordinate, IVertex>();
            endPointsMock = new Mock<IEndPoints>();

            vertex1Neighbours = new List<IVertex>();
            vertex2Neighbours = new List<IVertex>();
            vertex3Neighbours = new List<IVertex>();
            vertex4Neighbours = new List<IVertex>();
            vertex5Neighbours = new List<IVertex>();
            expectedPath = new List<IVertex>();
        }

        [SetUp]
        public void SetUp()
        {
            
        }

        [Test]
        public void Path_ValidParentVertices_ReturnsUnwindedPath()
        {
            Assert.Fail();
        }

        private void InitializeParentVertices()
        {
            var vertex1Coordinate = new Mock<ICoordinate>();
            vertex1Coordinate.Setup(c => c.CoordinatesValues).Returns(new[] {0, 0});
            var vertex2Coordinate = new Mock<ICoordinate>();
            vertex2Coordinate.Setup(c => c.CoordinatesValues).Returns(new[] {0, 1});
        }
    }
}