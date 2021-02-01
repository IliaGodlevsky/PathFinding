using Algorithm.Algorithms.Abstractions;
using Algorithm.Tests.TestsInfrastructure;
using GraphLib.Graphs;
using GraphLib.Graphs.Abstractions;
using GraphLib.Graphs.Infrastructure;
using NUnit.Framework;

namespace Algorithm.Tests.Tests.AlgorithmTesting
{
    [TestFixture]
    internal class AlgorithmTests
    {
        [TestCaseSource(
            typeof(TestCasesFactory),
            nameof(TestCasesFactory.AlgorithmWithGraphsTestCases))]
        public void FindPath_NotNullGraph_Success(IGraph graph,
            IAlgorithm algorithm)
        {
            algorithm.Graph = graph;

            algorithm.FindPath();
            var path = new GraphPath(graph);

            Assert.IsTrue(path.IsExtracted);
        }

        [TestCaseSource(
            typeof(TestCasesFactory),
            nameof(TestCasesFactory.AlgorithmsTestCases))]
        public void FindPath_NullGraph_Failed(IAlgorithm algorithm)
        {
            IGraph graph = new NullGraph();
            algorithm.Graph = graph;

            algorithm.FindPath();
            var path = new GraphPath(graph);

            Assert.IsFalse(path.IsExtracted);
        }
    }
}
