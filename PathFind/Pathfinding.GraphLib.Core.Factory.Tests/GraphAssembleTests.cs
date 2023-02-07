using NUnit.Framework;
using Pathfinding.GraphLib.Factory.Extensions;
using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.GraphLib.Factory.Realizations.GraphAssembles;
using Pathfinding.GraphLib.UnitTest.Realizations.TestObjects;
using System;
using System.Linq;

namespace Pathfinding.GraphLib.Core.Factory.Tests
{
    using Assemble = GraphAssemble<TestGraph, TestVertex>;
    using Layer = ILayer<TestGraph, TestVertex>;

    [TestFixture]
    public class GraphAssembleTests
    {
        private readonly string dimensionsWrongMessage = "A graph with wrong dimensionSizes was created";
        private readonly string graphHasWrongSizeMessage = "Graph size is not equal to multiplication of its dimension sizes";
        private readonly string wrongVerticesCreated = "A wrong vertices were created";
        private readonly string wrongNeighborsQuantity = "Some of vertices don't have neighbors";
        private readonly string wrongCostValue = "Wrong cost values detected";

        [TestCaseSource(typeof(AssembleTestCaseData), nameof(AssembleTestCaseData.Data))]
        public void AssembleGraphMethod_TestRealizations_ReturnsValidGraph(Assemble assemble, int[] dimensions)
        {
            var graph = assemble.AssembleGraph(dimensions);

            Assert.Multiple(() =>
            {
                Assert.IsTrue(graph.DimensionsSizes.SequenceEqual(dimensions), dimensionsWrongMessage);
                Assert.IsTrue(graph.Count(v => v != null) == graph.Count, wrongVerticesCreated);
                Assert.AreEqual(graph.Count, dimensions.Aggregate((x, y) => x * y), graphHasWrongSizeMessage);
            });
        }

        [TestCaseSource(typeof(AssembleTestCaseData), nameof(AssembleTestCaseData.LayersData))]
        public void AssembleGraphExtensionMethod_TestRealizations_ReturnsValidGraph(Assemble assemble,
            Layer[] layers, int[] dimensions)
        {
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
    }
}