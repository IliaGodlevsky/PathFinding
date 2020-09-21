using GraphLibrary.AlgoSelector;
using GraphLibrary.Enums;
using GraphLibrary.Graphs;
using GraphLibrary.PathFindingAlgorithm;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GraphLibrary.UnitTests.Tests.AlgoSelectorTest
{
    [TestClass]
    public class AlgoSelectorTest
    {
        [TestMethod]
        public void AlgoSelectorTest_NotValidValue_ReturnsNullAlgorithm()
        {
            Algorithms lee = Algorithms.LeeAlgorithm;
            Algorithms dijkstra = Algorithms.DijkstraAlgorithm;
            Algorithms aStar = Algorithms.AStarAlgorithm;
            Algorithms aStarModified = Algorithms.AStarModified;
            Algorithms greedy = Algorithms.ValueGreedyAlgorithm;

            var leeAlgo = AlgorithmSelector.GetPathFindAlgorithm(lee, NullGraph.Instance);
            var dikstraAlgo = AlgorithmSelector.GetPathFindAlgorithm(dijkstra, NullGraph.Instance);
            var aStarAlgo = AlgorithmSelector.GetPathFindAlgorithm(aStar, NullGraph.Instance);
            var aStarModifiedAlgo = AlgorithmSelector.GetPathFindAlgorithm(aStarModified, NullGraph.Instance);
            var greedyAlgo = AlgorithmSelector.GetPathFindAlgorithm(greedy, NullGraph.Instance);

            Assert.IsInstanceOfType(leeAlgo, typeof(LeeAlgorithm));
            Assert.IsInstanceOfType(dikstraAlgo, typeof(DijkstraAlgorithm));
            Assert.IsInstanceOfType(aStarAlgo, typeof(AStarAlgorithm));
            Assert.IsInstanceOfType(aStarModifiedAlgo, typeof(AStarModified));
            Assert.IsInstanceOfType(greedyAlgo, typeof(GreedyAlgorithm));
        }
    }
}
