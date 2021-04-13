using System;
using System.Collections.Generic;
using System.Linq;
using GraphLib.Interface;
using Moq;
using NUnit.Framework;

using DijkstrasAlgorithm = Plugins.DijkstraALgorithm.DijkstraAlgorithm;

namespace Plugins.DijkstraAlgorithm.Tests
{
    [TestFixture]
    public class DijkstraAlgorithmTest
    {
        private const int StartVertexIndex = 2;
        private const int EndVertexIndex = 6;

        private readonly Mock<IGraph> graphMock;
        private readonly Mock<IEndPoints> endPointsMock;
        private readonly List<IVertex> expectedPath;
        private readonly Dictionary<ICoordinate, IVertex> graphVerticesImitation;

        private List<IVertex> vertex1Neighbours;
        private List<IVertex> vertex2Neighbours;
        private List<IVertex> vertex3Neighbours;
        private List<IVertex> vertex4Neighbours;
        private List<IVertex> vertex5Neighbours;
        private List<IVertex> vertex6Neighbours;
        private List<IVertex> vertex7Neighbours;
        private List<IVertex> vertex8Neighbours;
        private List<IVertex> vertex9Neighbours;

        public DijkstraAlgorithmTest()
        {
            expectedPath = new List<IVertex>();
            endPointsMock = new Mock<IEndPoints>();
            graphVerticesImitation = new Dictionary<ICoordinate, IVertex>();
            graphMock = new Mock<IGraph>();
            endPointsMock = new Mock<IEndPoints>();

            InitilizeFakeNeighbours();
            InitFakeVertices();

            MockGraph();
            MockEndPoints();

            expectedPath.AddRange(new []
            {
                graphVerticesImitation.ElementAt(6).Value, 
                graphVerticesImitation.ElementAt(7).Value, 
                graphVerticesImitation.ElementAt(5).Value
            });
        }

        // Graph: 
        // 1   2   3
        // 4   5   1
        // 7   1   9
        // Start - 3 End - 7
        [Test]
        public void FindPath_EndpointsBelongToGraph_ReturnsShortestGraph()
        {
            var algorithm = new DijkstrasAlgorithm(graphMock.Object);
            int GetVertexCost(IVertex vertex) => vertex.Cost.CurrentCost;

            var graphPath = algorithm.FindPath(endPointsMock.Object);
            var path = graphPath.Path.ToArray();
            int pathCost = path.Sum(GetVertexCost);
            int expectedPathCost = expectedPath.Sum(GetVertexCost);
            int[] costs = path.Select(GetVertexCost).ToArray();
            int[] expectedPathVerticesCosts = expectedPath.Select(GetVertexCost).ToArray();
            var comparer = new VertexComparer();

            Assert.IsTrue(costs.SequenceEqual(expectedPathVerticesCosts));
            Assert.AreEqual(pathCost, expectedPathCost);
            Assert.IsTrue(path.SequenceEqual(expectedPath, comparer));
        }

        [Test]
        public void FindPath_EndPointsDoesntBelongToGraph_TrowsArgumentException()
        {
            var endPointsMock = new Mock<IEndPoints>();
            var endVertex = new Mock<IVertex>();
            var startVertex = new Mock<IVertex>();
            endPointsMock.Setup(e => e.Start).Returns(startVertex.Object);
            endPointsMock.Setup(e => e.End).Returns(endVertex.Object);

            var algorithm = new DijkstrasAlgorithm(graphMock.Object);

            Assert.Throws<ArgumentException>(() => algorithm.FindPath(endPointsMock.Object));
        }

        private void MockGraph()
        {
            graphMock
                .Setup(g => g[It.IsAny<ICoordinate>()])
                .Returns<ICoordinate>(index => graphVerticesImitation[index]);

            graphMock
                .Setup(g => g.Vertices)
                .Returns(graphVerticesImitation.Values);
        }

        private void MockEndPoints()
        {
            endPointsMock.Setup(e => e.Start).Returns(graphVerticesImitation.ElementAt(StartVertexIndex).Value);
            endPointsMock.Setup(e => e.End).Returns(graphVerticesImitation.ElementAt(EndVertexIndex).Value);
            endPointsMock.Setup(e => e.IsEndPoint(It.IsAny<IVertex>())).Returns<IVertex>(IsEndPoint);
        }

        private bool IsEndPoint(IVertex vertex)
        {
            var vertexCoordinates = vertex.Position.CoordinatesValues.ToArray();
            var startVertexCoordinates = endPointsMock.Object.Start.Position.CoordinatesValues.ToArray();
            var endVertexCoordinates = endPointsMock.Object.End.Position.CoordinatesValues.ToArray();
            bool isEnd = vertexCoordinates.SequenceEqual(endVertexCoordinates);
            bool isStart = vertexCoordinates.SequenceEqual(startVertexCoordinates);
            return isStart || isEnd;
        }

        private void InitilizeFakeNeighbours()
        {
            vertex1Neighbours = new List<IVertex>();
            vertex2Neighbours = new List<IVertex>();
            vertex3Neighbours = new List<IVertex>();
            vertex4Neighbours = new List<IVertex>();
            vertex5Neighbours = new List<IVertex>();
            vertex6Neighbours = new List<IVertex>();
            vertex7Neighbours = new List<IVertex>();
            vertex8Neighbours = new List<IVertex>();
            vertex9Neighbours = new List<IVertex>();
        }

        private void InitFakeVertices()
        {
            var vertexCost1 = new Mock<IVertexCost>();
            vertexCost1.Setup(cost => cost.CurrentCost).Returns(1);
            var vertexCost2 = new Mock<IVertexCost>();
            vertexCost2.Setup(cost => cost.CurrentCost).Returns(2);
            var vertexCost3 = new Mock<IVertexCost>();
            vertexCost3.Setup(cost => cost.CurrentCost).Returns(3);
            var vertexCost4 = new Mock<IVertexCost>();
            vertexCost4.Setup(cost => cost.CurrentCost).Returns(4);
            var vertexCost5 = new Mock<IVertexCost>();
            vertexCost5.Setup(cost => cost.CurrentCost).Returns(5);
            var vertexCost6 = new Mock<IVertexCost>();
            vertexCost6.Setup(cost => cost.CurrentCost).Returns(1);
            var vertexCost7 = new Mock<IVertexCost>();
            vertexCost7.Setup(cost => cost.CurrentCost).Returns(7);
            var vertexCost8 = new Mock<IVertexCost>();
            vertexCost8.Setup(cost => cost.CurrentCost).Returns(1);
            var vertexCost9 = new Mock<IVertexCost>();
            vertexCost9.Setup(cost => cost.CurrentCost).Returns(9);

            var coordinate1 = new Mock<ICoordinate>();
            coordinate1.Setup(c => c.CoordinatesValues).Returns(new [] {0, 0});
            var coordinate2 = new Mock<ICoordinate>();
            coordinate2.Setup(c => c.CoordinatesValues).Returns(new[] { 0, 1 });
            var coordinate3 = new Mock<ICoordinate>();
            coordinate3.Setup(c => c.CoordinatesValues).Returns(new[] { 0, 2 });
            var coordinate4 = new Mock<ICoordinate>();
            coordinate4.Setup(c => c.CoordinatesValues).Returns(new[] { 1, 0 });
            var coordinate5 = new Mock<ICoordinate>();
            coordinate5.Setup(c => c.CoordinatesValues).Returns(new[] { 1, 1 });
            var coordinate6 = new Mock<ICoordinate>();
            coordinate6.Setup(c => c.CoordinatesValues).Returns(new[] { 1, 2 });
            var coordinate7 = new Mock<ICoordinate>();
            coordinate7.Setup(c => c.CoordinatesValues).Returns(new[] { 2, 0 });
            var coordinate8 = new Mock<ICoordinate>();
            coordinate8.Setup(c => c.CoordinatesValues).Returns(new[] { 2, 1 });
            var coordinate9 = new Mock<ICoordinate>();
            coordinate9.Setup(c => c.CoordinatesValues).Returns(new[] { 2, 2 });

            var vertex1 = new Mock<IVertex>();
            vertex1.Setup(v => v.Position).Returns(coordinate1.Object);
            vertex1.Setup(v => v.Cost).Returns(vertexCost1.Object);
            vertex1.Setup(v => v.Neighbours).Returns(vertex1Neighbours);
            vertex1.Setup(v => v.IsObstacle).Returns(false);
            var vertex2 = new Mock<IVertex>();
            vertex2.Setup(v => v.Position).Returns(coordinate2.Object);
            vertex2.Setup(v => v.Cost).Returns(vertexCost2.Object);
            vertex2.Setup(v => v.Neighbours).Returns(vertex2Neighbours);
            vertex2.Setup(v => v.IsObstacle).Returns(false);
            var vertex3 = new Mock<IVertex>();
            vertex3.Setup(v => v.Position).Returns(coordinate3.Object);
            vertex3.Setup(v => v.Cost).Returns(vertexCost3.Object);
            vertex3.Setup(v => v.Neighbours).Returns(vertex3Neighbours);
            vertex3.Setup(v => v.IsObstacle).Returns(false);
            var vertex4 = new Mock<IVertex>();
            vertex4.Setup(v => v.Position).Returns(coordinate4.Object);
            vertex4.Setup(v => v.Cost).Returns(vertexCost4.Object);
            vertex4.Setup(v => v.Neighbours).Returns(vertex4Neighbours);
            vertex4.Setup(v => v.IsObstacle).Returns(false);
            var vertex5 = new Mock<IVertex>();
            vertex5.Setup(v => v.Position).Returns(coordinate5.Object);
            vertex5.Setup(v => v.Cost).Returns(vertexCost5.Object);
            vertex5.Setup(v => v.Neighbours).Returns(vertex5Neighbours);
            vertex5.Setup(v => v.IsObstacle).Returns(false);
            var vertex6 = new Mock<IVertex>();
            vertex6.Setup(v => v.Position).Returns(coordinate6.Object);
            vertex6.Setup(v => v.Cost).Returns(vertexCost6.Object);
            vertex6.Setup(v => v.Neighbours).Returns(vertex6Neighbours);
            vertex6.Setup(v => v.IsObstacle).Returns(false);
            var vertex7 = new Mock<IVertex>();
            vertex7.Setup(v => v.Position).Returns(coordinate7.Object);
            vertex7.Setup(v => v.Cost).Returns(vertexCost7.Object);
            vertex7.Setup(v => v.Neighbours).Returns(vertex7Neighbours);
            vertex7.Setup(v => v.IsObstacle).Returns(false);
            var vertex8 = new Mock<IVertex>();
            vertex8.Setup(v => v.Position).Returns(coordinate8.Object);
            vertex8.Setup(v => v.Cost).Returns(vertexCost8.Object);
            vertex8.Setup(v => v.Neighbours).Returns(vertex8Neighbours);
            vertex8.Setup(v => v.IsObstacle).Returns(false);
            var vertex9 = new Mock<IVertex>();
            vertex9.Setup(v => v.Position).Returns(coordinate9.Object);
            vertex9.Setup(v => v.Cost).Returns(vertexCost9.Object);
            vertex9.Setup(v => v.Neighbours).Returns(vertex9Neighbours);
            vertex9.Setup(v => v.IsObstacle).Returns(false);

            vertex1Neighbours.Add(vertex2.Object);
            vertex1Neighbours.Add(vertex4.Object);
            vertex1Neighbours.Add(vertex5.Object);

            vertex2Neighbours.Add(vertex1.Object);
            vertex2Neighbours.Add(vertex3.Object);
            vertex2Neighbours.Add(vertex4.Object);
            vertex2Neighbours.Add(vertex5.Object);
            vertex2Neighbours.Add(vertex6.Object);

            vertex3Neighbours.Add(vertex2.Object);
            vertex3Neighbours.Add(vertex5.Object);
            vertex3Neighbours.Add(vertex6.Object);

            vertex4Neighbours.Add(vertex1.Object);
            vertex4Neighbours.Add(vertex5.Object);
            vertex4Neighbours.Add(vertex2.Object);
            vertex4Neighbours.Add(vertex7.Object);
            vertex4Neighbours.Add(vertex8.Object);

            vertex5Neighbours.Add(vertex1.Object);
            vertex5Neighbours.Add(vertex2.Object);
            vertex5Neighbours.Add(vertex3.Object);
            vertex5Neighbours.Add(vertex4.Object);
            vertex5Neighbours.Add(vertex6.Object);
            vertex5Neighbours.Add(vertex7.Object);
            vertex5Neighbours.Add(vertex8.Object);
            vertex5Neighbours.Add(vertex9.Object);

            vertex6Neighbours.Add(vertex2.Object);
            vertex6Neighbours.Add(vertex3.Object);
            vertex6Neighbours.Add(vertex5.Object);
            vertex6Neighbours.Add(vertex8.Object);
            vertex6Neighbours.Add(vertex9.Object);

            vertex7Neighbours.Add(vertex4.Object);
            vertex7Neighbours.Add(vertex5.Object);
            vertex7Neighbours.Add(vertex8.Object);
            
            vertex8Neighbours.Add(vertex4.Object);
            vertex8Neighbours.Add(vertex7.Object);
            vertex8Neighbours.Add(vertex5.Object);
            vertex8Neighbours.Add(vertex6.Object);
            vertex8Neighbours.Add(vertex9.Object);

            vertex9Neighbours.Add(vertex6.Object);
            vertex9Neighbours.Add(vertex8.Object);
            vertex9Neighbours.Add(vertex5.Object);

            graphVerticesImitation.Add(coordinate1.Object, vertex1.Object);
            graphVerticesImitation.Add(coordinate2.Object, vertex2.Object);
            graphVerticesImitation.Add(coordinate3.Object, vertex3.Object);
            graphVerticesImitation.Add(coordinate4.Object, vertex4.Object);
            graphVerticesImitation.Add(coordinate5.Object, vertex5.Object);
            graphVerticesImitation.Add(coordinate6.Object, vertex6.Object);
            graphVerticesImitation.Add(coordinate7.Object, vertex7.Object);
            graphVerticesImitation.Add(coordinate8.Object, vertex8.Object);
            graphVerticesImitation.Add(coordinate9.Object, vertex9.Object);
        }

        private class VertexComparer : IEqualityComparer<IVertex>
        {
            public bool Equals(IVertex x, IVertex y)
            {
                if (x == null || y == null)
                {
                    return false;
                }

                return x.Position.CoordinatesValues.SequenceEqual(y.Position.CoordinatesValues)
                       && x.Cost.CurrentCost == y.Cost.CurrentCost;
            }

            public int GetHashCode(IVertex obj)
            {
                return obj.Position.CoordinatesValues
                           .Select(i => i.GetHashCode())
                           .Aggregate((x, y) => x.GetHashCode() ^ y.GetHashCode()) ^
                       obj.Cost.CurrentCost.GetHashCode();
            }
        }
    }
}