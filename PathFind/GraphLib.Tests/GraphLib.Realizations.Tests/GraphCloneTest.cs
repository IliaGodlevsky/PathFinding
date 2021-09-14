using GraphLib.Extensions;
using GraphLib.TestRealizations.TestFactories;
using NUnit.Framework;

namespace GraphLib.Realizations.Tests
{
    [TestFixture]
    public class GraphCloneTest
    {
        private const int ObstaclePercent = 25;
        private TestGraphAssemble graphAssemble;

        [TestCase(new[] { 14, 13} )]
        [TestCase(new[] { 14, 13, 12 })]
        public void GraphCloneTest_VariousDimensions_ReturnsAnotherEqualGraph(int[] dimensionSizes)
        {
            graphAssemble = new TestGraphAssemble();
            var graph = graphAssemble.AssembleGraph(ObstaclePercent, dimensionSizes);

            var clonedGraph = graph.Clone();

            Assert.AreNotSame(graph, clonedGraph);
            Assert.IsTrue(graph.IsEqual(clonedGraph));
        }
    }
}
