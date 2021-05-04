using GraphLib.Interfaces.Factories;
using GraphLib.Realizations.Factories;
using GraphLib.Realizations.Tests.Factories;
using GraphLib.Realizations.Tests.Objects;
using Common.Extensions;
using NUnit.Framework;
using System.Linq;
using GraphLib.Interfaces;

namespace GraphLib.Realizations.Tests
{
    [TestFixture]
    public class GraphAssemblerTests
    {
        private readonly ICoordinateFactory coordinateFactory;
        private readonly IVertexFactory vertexFactory;
        private readonly IVertexCostFactory costFactory;
        private readonly IGraphFactory graphFactory;
        private readonly ICoordinateRadarFactory radarFactory;
        private readonly IGraphAssembler graphAssembler;

        public GraphAssemblerTests()
        {
            coordinateFactory = new TestCoordinateFactory();
            vertexFactory = new TestVertexFactory();
            costFactory = new TestCostFactory();
            graphFactory = new TestGraphFactory();
            radarFactory = new CoordinateAroundRadarFactory();
            graphAssembler = new GraphAssembler(vertexFactory,
                coordinateFactory, graphFactory, costFactory, radarFactory);
        }

        [TestCase(15, new int[] { 15 })]
        [TestCase(10, new int[] { 10, 15 })]
        [TestCase(10, new int[] { 7, 10, 7 })]
        [TestCase(33, new int[] { 7, 4, 7, 4 })]
        public void AssembleGraph_ReturnsNotNullGraph(int obstaclePercent, int[] dimensionSizes)
        {
            var graph = graphAssembler.AssembleGraph(obstaclePercent, dimensionSizes);

            bool CoordinatesAreUnique()
            {
                var uniqueVertices = graph.Vertices
                    .DistinctBy(vertex => vertex.Position)
                    .ToArray();

                return uniqueVertices.Length == graph.Size;
            }

            bool IsTestVertexType(IVertex vertex)
            {
                return vertex?.GetType() == typeof(TestVertex);
            }

            Assert.IsTrue(graph.DimensionsSizes.SequenceEqual(dimensionSizes));
            Assert.IsTrue(graph.Vertices.All(IsTestVertexType));
            Assert.IsTrue(CoordinatesAreUnique());
        }
    }
}
