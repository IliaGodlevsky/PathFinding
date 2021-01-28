using Algorithm.Algorithms;
using Algorithm.Algorithms.Abstractions;
using Algorithm.PathFindingAlgorithms;
using GraphLib.Coordinates;
using GraphLib.Graphs;
using GraphLib.Graphs.Abstractions;
using GraphLib.Graphs.Factories;
using NUnit.Framework;
using System.Collections;
using System.Linq;

namespace Algorithm.NUnitTests.AlgorithmTesting
{
    internal class TestCasesFactory
    {
        private static readonly IGraph graph2D;
        private static readonly IGraph graph3D;

        private static readonly IAlgorithm leeAlgorithm;
        private static readonly IAlgorithm bestFirstLeeAlgorithm;
        private static readonly IAlgorithm dijkstraAlgorithm;
        private static readonly IAlgorithm aStarAlgorithm;
        private static readonly IAlgorithm aStarModified;
        private static readonly IAlgorithm depthFirstAlgorithm;
        private static readonly IAlgorithm costGreedyAlgorithm;
        private static readonly IAlgorithm distanceGreedyAlgoritm;

        static TestCasesFactory()
        {
            var graph2DFactory = new GraphFactory<Graph2D>(0, 15, 15);
            var graph3DFactory = new GraphFactory<Graph3D>(0, 7, 7, 7);

            graph2D = graph2DFactory.CreateGraph(() => new TestVertex(), coordinates => new Coordinate2D(coordinates.ToArray()));
            graph3D = graph3DFactory.CreateGraph(() => new TestVertex(), coordinates => new Coordinate3D(coordinates.ToArray()));

            leeAlgorithm = new LeeAlgorithm();
            bestFirstLeeAlgorithm = new BestFirstLeeAlgorithm();
            dijkstraAlgorithm = new DijkstraAlgorithm();
            aStarAlgorithm = new AStarAlgorithm();
            aStarModified = new AStarModified();
            depthFirstAlgorithm = new DepthFirstAlgorithm();
            costGreedyAlgorithm = new CostGreedyAlgorithm();
            distanceGreedyAlgoritm = new DistanceGreedyAlgoritm();
        }

        internal static IEnumerable AlgorithmTestCases
        {
            get
            {
                yield return new TestCaseData(graph2D, leeAlgorithm);
                yield return new TestCaseData(graph2D, bestFirstLeeAlgorithm);
                yield return new TestCaseData(graph2D, dijkstraAlgorithm);
                yield return new TestCaseData(graph2D, aStarAlgorithm);
                yield return new TestCaseData(graph2D, aStarModified);
                yield return new TestCaseData(graph2D, depthFirstAlgorithm);
                yield return new TestCaseData(graph2D, costGreedyAlgorithm);
                yield return new TestCaseData(graph2D, distanceGreedyAlgoritm);

                yield return new TestCaseData(graph3D, leeAlgorithm);
                yield return new TestCaseData(graph3D, bestFirstLeeAlgorithm);
                yield return new TestCaseData(graph3D, dijkstraAlgorithm);
                yield return new TestCaseData(graph3D, aStarAlgorithm);
                yield return new TestCaseData(graph3D, aStarModified);
                yield return new TestCaseData(graph3D, depthFirstAlgorithm);
                yield return new TestCaseData(graph3D, costGreedyAlgorithm);
                yield return new TestCaseData(graph3D, distanceGreedyAlgoritm);
            }
        }
    }
}
