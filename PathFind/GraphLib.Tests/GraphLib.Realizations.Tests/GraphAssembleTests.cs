using Autofac.Extras.Moq;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.NullRealizations.NullObjects;
using GraphLib.Realizations.Factories.GraphAssembles;
using GraphLib.Realizations.Neighbourhoods;
using GraphLib.Realizations.Tests.Extenions;
using GraphLib.TestRealizations;
using GraphLib.TestRealizations.TestObjects;
using NUnit.Framework;
using System;
using System.Linq;

namespace GraphLib.Realizations.Tests
{
    [TestFixture]
    public class GraphAssemblerTests
    {
        [TestCase(15, new int[] { 100 })]
        [TestCase(10, new int[] { 10, 15 })]
        [TestCase(10, new int[] { 7, 10, 7 })]
        [TestCase(33, new int[] { 7, 4, 7, 4 })]
        public void AssembleGraph_ReturnsValidGraph(int obstaclePercent, int[] dimensionSizes)
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.MockCoordinateFactory(x => new TestCoordinate(x));
                mock.MockNeighbourhoodFactory(x => new MooreNeighborhood(x));
                mock.MockVertexFactory((nc, c) => new TestVertex(nc, c));
                mock.MockGraphFactory((vertices, dimensions) => new TestGraph(vertices, dimensions));
                mock.MockVertexCostFactory(cost => new TestVertexCost(cost));

                var assemble = mock.Create<GraphAssemble>();
                var graph = assemble.AssembleGraph(obstaclePercent, dimensionSizes);

                Assert.IsTrue(graph.DimensionsSizes.SequenceEqual(dimensionSizes));
                Assert.IsTrue(graph.Vertices.All(IsTestVertexType));
                Assert.AreEqual(obstaclePercent, graph.GetObstaclePercent());
                Assert.IsTrue(CoordinatesAreUnique(graph));
            }
        }

        [Test]
        public void AssembleGraph_NullRealizations_ReturnsNullGraph()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.MockCoordinateFactory(_ => NullCoordinate.Instance);
                mock.MockNeighbourhoodFactory(_ => NullNeighborhood.Instance);
                mock.MockVertexFactory((_, __) => NullVertex.Instance);
                mock.MockGraphFactory((_, __) => NullGraph.Instance);
                mock.MockVertexCostFactory(_ => NullCost.Instance);

                var assemble = mock.Create<GraphAssemble>();
                var graph = assemble.AssembleGraph(0);

                Assert.AreSame(NullGraph.Instance, graph);
            }
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
