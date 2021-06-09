using Autofac.Extras.Moq;
using Common.Extensions;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.NullRealizations.NullObjects;
using GraphLib.Realizations.Factories.GraphAssembles;
using GraphLib.Realizations.NeighboursCoordinates;
using GraphLib.TestRealizations;
using GraphLib.TestRealizations.TestObjects;
using Moq;
using NUnit.Framework;
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
            mock.Mock<INeighboursCoordinatesFactory>()
                .Setup(x => x.CreateNeighboursCoordinates(It.IsAny<ICoordinate>()))
                .Returns<ICoordinate>(x => new AroundNeighboursCoordinates(x));
            mock.Mock<IVertexFactory>()
                .Setup(x => x.CreateVertex(It.IsAny<INeighboursCoordinates>(), It.IsAny<ICoordinate>()))
                .Returns<INeighboursCoordinates, ICoordinate>((nc, c) => new TestVertex(nc, c));
            mock.Mock<IGraphFactory>()
                .Setup(x => x.CreateGraph(It.IsAny<int[]>()))
                .Returns<int[]>(d => new TestGraph(d));
            mock.Mock<IVertexCostFactory>()
                .Setup(x => x.CreateCost())
                .Returns(() => new TestVertexCost());

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
                .Returns<int[]>(x => new NullCoordinate());
            mock.Mock<INeighboursCoordinatesFactory>()
                .Setup(x => x.CreateNeighboursCoordinates(It.IsAny<ICoordinate>()))
                .Returns<ICoordinate>(x => new NullNeighboursCoordinates());
            mock.Mock<IVertexFactory>()
                .Setup(x => x.CreateVertex(It.IsAny<INeighboursCoordinates>(), It.IsAny<ICoordinate>()))
                .Returns<INeighboursCoordinates, ICoordinate>((nc, c) => new NullVertex());
            mock.Mock<IGraphFactory>()
                .Setup(x => x.CreateGraph(It.IsAny<int[]>()))
                .Returns<int[]>(d => new NullGraph());
            mock.Mock<IVertexCostFactory>()
                .Setup(x => x.CreateCost())
                .Returns(() => new NullCost());

            var assemble = mock.Create<GraphAssemble>();

            IGraph graph = null;

            Assert.DoesNotThrow(() => graph = assemble.AssembleGraph(0));
            Assert.IsTrue(graph.Vertices.All(IsNullVertex));
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
            return vertex.GetType() == typeof(TestVertex);
        }

        private bool IsNullVertex(IVertex vertex)
        {
            return vertex.GetType() == typeof(NullVertex);
        }
    }
}
