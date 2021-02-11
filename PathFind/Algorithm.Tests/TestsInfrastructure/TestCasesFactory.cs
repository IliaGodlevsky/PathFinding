using Algorithm.Algorithms;
using Algorithm.PathFindingAlgorithms;
using GraphLib.Factories;
using GraphLib.Interface;
using NUnit.Framework;
using System.Collections;

namespace Algorithm.Tests.TestsInfrastructure
{
    internal class TestCasesFactory
    {
        private const int ObstaclePercent = 0;
        private const int Width2D = 25;
        private const int Length2D = 15;
        private const int Width3D = 7;
        private const int Length3D = 8;
        private const int Height3D = 9;

        private static readonly IGraphAssembler graph2DAssembler;
        private static readonly IGraphAssembler graph3DAssembler;

        static TestCasesFactory()
        {
            var vertexFactory = new TestVertexFactory();
            var coordinate2DFactory = new Coordinate2DFactory();
            var coordinate3DFactory = new Coordinate3DFactory();

            var graph2DFactory = new Graph2DFactory();
            var graph3DFactory = new Graph3DFactory();

            graph2DAssembler = new GraphAssembler(vertexFactory, coordinate2DFactory, graph2DFactory);
            graph3DAssembler = new GraphAssembler(vertexFactory, coordinate3DFactory, graph3DFactory);
        }

        internal static IEnumerable AlgorithmWithGraphsTestCases
        {
            get
            {
                yield return new TestCaseData(GetGraph2D(), new LeeAlgorithm());
                yield return new TestCaseData(GetGraph2D(), new BestFirstLeeAlgorithm());
                yield return new TestCaseData(GetGraph2D(), new DijkstraAlgorithm());
                yield return new TestCaseData(GetGraph2D(), new AStarAlgorithm());
                yield return new TestCaseData(GetGraph2D(), new AStarModified());
                yield return new TestCaseData(GetGraph2D(), new DistanceFirstAlgorithm());

                yield return new TestCaseData(GetGraph3D(), new LeeAlgorithm());
                yield return new TestCaseData(GetGraph3D(), new BestFirstLeeAlgorithm());
                yield return new TestCaseData(GetGraph3D(), new DijkstraAlgorithm());
                yield return new TestCaseData(GetGraph3D(), new AStarAlgorithm());
                yield return new TestCaseData(GetGraph3D(), new AStarModified());
                yield return new TestCaseData(GetGraph3D(), new DistanceFirstAlgorithm());
            }
        }

        internal static IEnumerable AlgorithmsTestCases
        {
            get
            {
                yield return new TestCaseData(new LeeAlgorithm());
                yield return new TestCaseData(new BestFirstLeeAlgorithm());
                yield return new TestCaseData(new DijkstraAlgorithm());
                yield return new TestCaseData(new AStarAlgorithm());
                yield return new TestCaseData(new AStarModified());
                yield return new TestCaseData(new DistanceFirstAlgorithm());
            }
        }

        private static IGraph GetGraph2D()
        {
            var graph = graph2DAssembler
                .AssembleGraph(ObstaclePercent, Width2D, Length2D);

            return graph;
        }

        private static IGraph GetGraph3D()
        {
            var graph = graph3DAssembler
                .AssembleGraph(ObstaclePercent, Width3D, Length3D, Height3D);

            return graph;
        }
    }
}
