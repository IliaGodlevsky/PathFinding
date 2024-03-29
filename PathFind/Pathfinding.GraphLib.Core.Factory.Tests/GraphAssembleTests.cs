using NUnit.Framework;
using Pathfinding.GraphLib.Core.Realizations;
using Pathfinding.GraphLib.Factory.Extensions;
using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.GraphLib.Factory.Realizations.GraphAssembles;
using Pathfinding.GraphLib.Factory.Realizations.GraphFactories;
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
    using Assemble = GraphAssemble<TestVertex>;

    [TestFixture]
    public class GraphAssembleTests
    {
        private readonly string dimensionsWrongMessage = "A graph with wrong dimensionSizes was created";
        private readonly string graphHasWrongSizeMessage = "Graph size is not equal to multiplication of its dimension sizes";
        private readonly string wrongVerticesCreated = "A wrong vertices were created";
        private readonly string wrongNeighborsQuantity = "Some of vertices don't have neighbors";
        private readonly string wrongCostValue = "Wrong cost values detected";

        [TestCaseSource(typeof(AssembleTestCaseData), nameof(AssembleTestCaseData.Data))]
        public void AssembleGraphMethod_TestRealizations_ReturnsValidGraph(int[] dimensions)
        {
            var assemble = GetAssemble();
            var graph = assemble.AssembleGraph(dimensions);

            Assert.Multiple(() =>
            {
                Assert.IsTrue(graph.DimensionsSizes.SequenceEqual(dimensions), dimensionsWrongMessage);
                Assert.IsTrue(graph.Count(v => v != null) == graph.Count, wrongVerticesCreated);
                Assert.AreEqual(graph.Count, dimensions.Aggregate((x, y) => x * y), graphHasWrongSizeMessage);
            });
        }

        [TestCaseSource(typeof(AssembleTestCaseData), nameof(AssembleTestCaseData.Data))]
        public void AssembleGraphExtensionMethod_TestRealizations_ReturnsValidGraph(int[] dimensions)
        {
            var assemble = GetAssemble();
            var layers = new Layers(GetLayers());
            var graph = assemble.AssembleGraph(layers, dimensions);

            Assert.Multiple(() =>
            {
                Assert.IsTrue(graph.DimensionsSizes.SequenceEqual(dimensions), dimensionsWrongMessage);
                Assert.IsTrue(graph.Count(v => v != null) == graph.Count, wrongVerticesCreated);
                Assert.AreEqual(graph.Count, dimensions.Aggregate((x, y) => x * y), graphHasWrongSizeMessage);
                Assert.IsTrue(graph.All(v => v.Neighbours.Any()), wrongNeighborsQuantity);
                Assert.IsTrue(graph.All(v => v.Cost.CurrentCost != 0), wrongCostValue);
            });
        }

        private static Assemble GetAssemble()
        {
            var graphFactory = new GraphFactory<TestVertex>();
            var vertexFactory = new TestVertexFactory();
            return new Assemble(vertexFactory, graphFactory);
        }

        private static ILayer[] GetLayers()
        {
            int obstaclePercent = 14;
            var random = new CongruentialRandom();
            var range = new InclusiveValueRange<int>(9, 1);
            return new ILayer[]
            {
                new NeighborhoodLayer(new MooreNeighborhoodFactory()),
                new VertexCostLayer(range, random),
                new ObstacleLayer(random, obstaclePercent)
            };
        }
    }
}