using Autofac.Extras.Moq;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.NullRealizations.NullObjects;
using GraphLib.Realizations.Factories.GraphAssembles;
using GraphLib.Realizations.Neighbourhoods;
using GraphLib.TestRealizations;
using GraphLib.TestRealizations.TestObjects;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Realizations.Tests
{
    [TestFixture]
    public class GraphAssemblerTests
    {
        [TestCase(15, new int[] { 15 })]
        [TestCase(10, new int[] { 10, 15 })]
        [TestCase(10, new int[] { 7, 10, 7 })]
        [TestCase(33, new int[] { 7, 4, 7, 4 })]
        public void AssembleGraph_ReturnsValidGraph(int obstaclePercent, int[] dimensionSizes)
        {
            using var mock = AutoMock.GetLoose();
            mock.Mock<ICoordinateFactory>()
                .Setup(x => x.CreateCoordinate(It.IsAny<int[]>()))
                .Returns<int[]>(x => new TestCoordinate(x));

            mock.Mock<INeighborhoodFactory>()
                .Setup(x => x.CreateNeighborhood(It.IsAny<ICoordinate>()))
                .Returns<ICoordinate>(x => new MooreNeighborhood(x));

            mock.Mock<IVertexFactory>()
                .Setup(x => x.CreateVertex(It.IsAny<INeighborhood>(), It.IsAny<ICoordinate>()))
                .Returns<INeighborhood, ICoordinate>((nc, c) => new TestVertex(nc, c));

            mock.Mock<IGraphFactory>()
                .Setup(x => x.CreateGraph(It.IsAny<IEnumerable<IVertex>>(), It.IsAny<int[]>()))
                .Returns<IEnumerable<IVertex>, int[]>((vertices, dimensions) => new TestGraph(vertices, dimensions));

            mock.Mock<IVertexCostFactory>()
                .Setup(x => x.CreateCost(It.IsAny<int>()))
                .Returns<int>(cost => new TestVertexCost(cost));

            var assemble = mock.Create<GraphAssemble>();

            var graph = assemble.AssembleGraph(obstaclePercent, dimensionSizes);

            Assert.IsTrue(graph.DimensionsSizes.SequenceEqual(dimensionSizes));
            Assert.IsTrue(graph.Vertices.All(IsTestVertexType));
            Assert.IsTrue(CoordinatesAreUnique(graph));
        }

        [Test]
        public void AssembleGraph_NullRealizations_ReturnsNullGraph()
        {
            using var mock = AutoMock.GetLoose();
            mock.Mock<ICoordinateFactory>()
                .Setup(x => x.CreateCoordinate(It.IsAny<int[]>()))
                .Returns<int[]>(x => NullCoordinate.Instance);

            mock.Mock<INeighborhoodFactory>()
                .Setup(x => x.CreateNeighborhood(It.IsAny<ICoordinate>()))
                .Returns<ICoordinate>(x => NullNeighboursCoordinates.Instance);

            mock.Mock<IVertexFactory>()
                .Setup(x => x.CreateVertex(It.IsAny<INeighborhood>(), It.IsAny<ICoordinate>()))
                .Returns<INeighborhood, ICoordinate>((nc, c) => NullVertex.Instance);

            mock.Mock<IGraphFactory>()
                .Setup(x => x.CreateGraph(It.IsAny<IEnumerable<IVertex>>(), It.IsAny<int[]>()))
                .Returns<IEnumerable<IVertex>, int[]>((vertices, dimensions) => NullGraph.Instance);

            mock.Mock<IVertexCostFactory>()
                .Setup(x => x.CreateCost(default))
                .Returns(() => NullCost.Instance);

            var assemble = mock.Create<GraphAssemble>();

            IGraph graph = null;

            Assert.DoesNotThrow(() => graph = assemble.AssembleGraph(0));
        }

        private bool CoordinatesAreUnique(IGraph graph)
        {
            var uniqueVertices = graph.Vertices
                .DistinctBy(vertex => vertex.Position)
                .ToArray();

            return uniqueVertices.Length == graph.Size;
        }

        private bool IsTestVertexType(IVertex vertex)
        {
            return vertex is TestVertex;
        }
    }
}
