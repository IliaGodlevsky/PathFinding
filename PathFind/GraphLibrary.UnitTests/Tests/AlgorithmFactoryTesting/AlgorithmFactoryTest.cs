using GraphLibrary.AlgorithmCreating;
using GraphLibrary.Enums;
using GraphLibrary.Graphs;
using GraphLibrary.PathFindingAlgorithm;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GraphLibrary.UnitTests.Tests.AlgorithmFactoryTesting
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

            var leeAlgo = AlgorithmFactory.CreateAlgorithm(lee, NullGraph.Instance);
            var dikstraAlgo = AlgorithmFactory.CreateAlgorithm(dijkstra, NullGraph.Instance);
            var aStarAlgo = AlgorithmFactory.CreateAlgorithm(aStar, NullGraph.Instance);
            var aStarModifiedAlgo = AlgorithmFactory.CreateAlgorithm(aStarModified, NullGraph.Instance);
            var greedyAlgo = AlgorithmFactory.CreateAlgorithm(greedy, NullGraph.Instance);

            Assert.IsInstanceOfType(leeAlgo, typeof(LeeAlgorithm));
            Assert.IsInstanceOfType(dikstraAlgo, typeof(DijkstraAlgorithm));
            Assert.IsInstanceOfType(aStarAlgo, typeof(AStarAlgorithm));
            Assert.IsInstanceOfType(aStarModifiedAlgo, typeof(AStarModified));
            Assert.IsInstanceOfType(greedyAlgo, typeof(GreedyAlgorithm));
        }
    }
}
