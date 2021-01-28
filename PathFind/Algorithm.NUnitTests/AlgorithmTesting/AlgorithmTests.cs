using Algorithm.Algorithms.Abstractions;
using Algorithm.NUnitTests.AlgorithmTesting;
using GraphLib.Extensions;
using GraphLib.Graphs.Abstractions;
using GraphLib.Graphs.Infrastructure;
using NUnit.Framework;
using System.Linq;

namespace Algorithm.NUnitTests
{
    [TestFixture]
    internal class AlgorithmTests
    {
        [TestCaseSource(typeof(TestCasesFactory), 
            nameof(TestCasesFactory.AlgorithmTestCases))]
        public void FindPath_NotNullGraph_Success(IGraph graph, 
            IAlgorithm algorithm)
        {
            graph.Refresh();
            graph.Start = graph.First();
            graph.End = graph.Last();
            algorithm.Graph = graph;

            algorithm.FindPath();
            algorithm.Reset();
            var path = new GraphPath(graph);

            Assert.IsTrue(path.IsExtracted);
        }
    }
}
