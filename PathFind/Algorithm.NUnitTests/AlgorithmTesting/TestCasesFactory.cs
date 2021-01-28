using Algorithm.Algorithms;
using Algorithm.PathFindingAlgorithms;
using GraphLib.Coordinates;
using GraphLib.Graphs;
using GraphLib.Graphs.Factories;
using GraphLib.Graphs.Factories.Interfaces;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm.NUnitTests.AlgorithmTesting
{
    internal class TestCasesFactory
    {
        private static readonly IGraphFactory graph2DFactory;
        private static readonly IGraphFactory graph3DFactory;

        static TestCasesFactory()
        {
            graph2DFactory = new GraphFactory<Graph2D>(0, 15, 15);
            graph3DFactory = new GraphFactory<Graph3D>(0, 7, 7, 7);
        }

        internal static IEnumerable AlgorithmTestCases
        {
            get
            {
                yield return new TestCaseData(graph2DFactory.CreateGraph(CreateVertex, CreateCoordinate2D), new LeeAlgorithm());
                yield return new TestCaseData(graph2DFactory.CreateGraph(CreateVertex, CreateCoordinate2D), new BestFirstLeeAlgorithm());
                yield return new TestCaseData(graph2DFactory.CreateGraph(CreateVertex, CreateCoordinate2D), new DijkstraAlgorithm());
                yield return new TestCaseData(graph2DFactory.CreateGraph(CreateVertex, CreateCoordinate2D), new AStarAlgorithm());
                yield return new TestCaseData(graph2DFactory.CreateGraph(CreateVertex, CreateCoordinate2D), new AStarModified());
                yield return new TestCaseData(graph2DFactory.CreateGraph(CreateVertex, CreateCoordinate2D), new DepthFirstAlgorithm());
                yield return new TestCaseData(graph2DFactory.CreateGraph(CreateVertex, CreateCoordinate2D), new CostGreedyAlgorithm());
                yield return new TestCaseData(graph2DFactory.CreateGraph(CreateVertex, CreateCoordinate2D), new DistanceGreedyAlgoritm());

                yield return new TestCaseData(graph3DFactory.CreateGraph(CreateVertex, CreateCoordinate3D), new LeeAlgorithm());
                yield return new TestCaseData(graph3DFactory.CreateGraph(CreateVertex, CreateCoordinate3D), new BestFirstLeeAlgorithm());
                yield return new TestCaseData(graph3DFactory.CreateGraph(CreateVertex, CreateCoordinate3D), new DijkstraAlgorithm());
                yield return new TestCaseData(graph3DFactory.CreateGraph(CreateVertex, CreateCoordinate3D), new AStarAlgorithm());
                yield return new TestCaseData(graph3DFactory.CreateGraph(CreateVertex, CreateCoordinate3D), new AStarModified());
                yield return new TestCaseData(graph3DFactory.CreateGraph(CreateVertex, CreateCoordinate3D), new DepthFirstAlgorithm());
                yield return new TestCaseData(graph3DFactory.CreateGraph(CreateVertex, CreateCoordinate3D), new CostGreedyAlgorithm());
                yield return new TestCaseData(graph3DFactory.CreateGraph(CreateVertex, CreateCoordinate3D), new DistanceGreedyAlgoritm());
            }
        }

        internal static IEnumerable Algorithms
        {
            get
            {
                yield return new TestCaseData(new LeeAlgorithm());
                yield return new TestCaseData(new BestFirstLeeAlgorithm());
                yield return new TestCaseData(new DijkstraAlgorithm());
                yield return new TestCaseData(new AStarAlgorithm());
                yield return new TestCaseData(new AStarModified());
                yield return new TestCaseData(new DepthFirstAlgorithm());
                yield return new TestCaseData(new CostGreedyAlgorithm());
                yield return new TestCaseData(new DistanceGreedyAlgoritm());
            }
        }

        private static TestVertex CreateVertex()
        {
            return new TestVertex();
        }

        private static Coordinate2D CreateCoordinate2D(IEnumerable<int>coordinates)
        {
            return new Coordinate2D(coordinates.ToArray());
        }

        private static Coordinate3D CreateCoordinate3D(IEnumerable<int> coordinate)
        {
            return new Coordinate3D(coordinate.ToArray());
        }
    }
}
