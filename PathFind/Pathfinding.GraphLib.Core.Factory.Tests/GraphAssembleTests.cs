using NUnit.Framework;
using Pathfinding.GraphLib.Factory.Extensions;
using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.GraphLib.Factory.Realizations.GraphAssembles;
using Pathfinding.GraphLib.Factory.Realizations.Layers;
using Pathfinding.GraphLib.Factory.Realizations.NeighborhoodFactories;
using Pathfinding.GraphLib.UnitTest.Realizations.TestFactories;
using Pathfinding.GraphLib.UnitTest.Realizations.TestObjects;
using Shared.Primitives.ValueRange;
using Shared.Random.Realizations;
using System;
using System.Linq;

namespace Pathfinding.GraphLib.Core.Factory.Tests
{
    [TestFixture]
    public class GraphAssembleTests
    {
        [TestCase(new[] { 20, 25 }, 5, 1, Description = "Test of assembling logic of GraphAssemble class")]
        public void AssembleGraphMethod_TestRealizations_ReturnsValidGraph(int[] dimensions,
            int upperValueOfCostRange, int lowerValueOfCostRange)
        {
            var range = new InclusiveValueRange<int>(upperValueOfCostRange, lowerValueOfCostRange);
            var graphFactory = new TestGraphFactory();
            var coordinateFactory = new TestCoordinateFactory();
            var vertexFactory = new TestVertexFactory();
            var assemble = new GraphAssemble<TestGraph, TestVertex>(vertexFactory,
                coordinateFactory, graphFactory);
            var graph = assemble.AssembleGraph(dimensions);

            Assert.Multiple(() =>
            {
                Assert.IsTrue(graph.DimensionsSizes.SequenceEqual(dimensions),
                    "A graph with wrong dimensionSizes was created");
                Assert.IsTrue(graph.Where(vertex => vertex is TestVertex).Count() == graph.Count,
                    "Graph contains unitialized vertices");
                Assert.AreEqual(graph.Count, dimensions.Aggregate((x, y) => x * y),
                    "Graph size is not equal to multiplication of its dimension sizes");
            });
        }

        [TestCase(new[] { 20, 25 }, 5, 1, 25, Description = "Test of assembling logic of GraphAssemble class")]
        public void AssembleGraphExtensionsMethod_TestRealizations_ReturnValidGraph(int[] dimensions,
            int upperValueOfCostRange, int lowerValueOfCostRange, int obstaclePercent)
        {
            var range = new InclusiveValueRange<int>(upperValueOfCostRange, lowerValueOfCostRange);
            var graphFactory = new TestGraphFactory();
            var coordinateFactory = new TestCoordinateFactory();
            var vertexFactory = new TestVertexFactory();
            var costFactory = new TestCostFactory();
            var neighbourhoodFactory = new VonNeumannNeighborhoodFactory();
            var random = new PseudoRandom();
            var layers = new ILayer<TestGraph, TestVertex>[]
            {
                new ObstacleLayer<TestGraph, TestVertex>(random, obstaclePercent),
                new NeighborhoodLayer<TestGraph, TestVertex>(neighbourhoodFactory),
                new VertexCostLayer<TestGraph, TestVertex>(costFactory, range, random)
            };

            var assemble = new GraphAssemble<TestGraph, TestVertex>(vertexFactory,
                coordinateFactory, graphFactory);
            var graph = assemble.AssembleGraph(layers, dimensions);

            Assert.Multiple(() =>
            {
                Assert.IsTrue(graph.DimensionsSizes.SequenceEqual(dimensions),
                    "A graph with wrong dimensionSizes was created");
                Assert.IsTrue(graph.Where(vertex => vertex is TestVertex).Count() == graph.Count,
                    "Graph contains unitialized vertices");
                Assert.AreEqual(graph.Count, dimensions.Aggregate((x, y) => x * y),
                    "Graph size is not equal to multiplication of its dimension sizes");
            });
        }
    }
}