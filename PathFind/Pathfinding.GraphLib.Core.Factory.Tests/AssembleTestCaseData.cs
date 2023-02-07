using NUnit.Framework;
using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.GraphLib.Factory.Realizations;
using Pathfinding.GraphLib.Factory.Realizations.GraphAssembles;
using Pathfinding.GraphLib.Factory.Realizations.Layers;
using Pathfinding.GraphLib.Factory.Realizations.NeighborhoodFactories;
using Pathfinding.GraphLib.UnitTest.Realizations.TestFactories;
using Pathfinding.GraphLib.UnitTest.Realizations.TestFactories.Layers;
using Pathfinding.GraphLib.UnitTest.Realizations.TestObjects;
using Shared.Primitives.ValueRange;
using Shared.Random;
using Shared.Random.Realizations;
using System.Collections;

namespace Pathfinding.GraphLib.Core.Factory.Tests
{
    internal static class AssembleTestCaseData
    {
        public static IEnumerable Data
        {
            get
            {
                yield return GenerateTestCase(10, 20);
                yield return GenerateTestCase(8, 12, 16);
                yield return GenerateTestCase(5, 6, 7, 8);
            }
        }

        public static IEnumerable LayersData
        {
            get
            {
                yield return Generate(15, 9, 1, 25, 25);
                yield return Generate(5, 9, 1, 7, 10, 13);
                yield return Generate(25, 9, 1, 4, 5, 6, 7);
            }
        }

        private static TestCaseData Generate(int obstaclePercent, int maxCost, int minCost, params int[] dimensions)
        {
            var random = new PseudoRandom();
            var range = new InclusiveValueRange<int>(maxCost, minCost);
            var layers = new ILayer<TestGraph, TestVertex>[]
            {
                new NeighborhoodLayer<TestGraph, TestVertex>(new MooreNeighborhoodFactory()),
                new VertexCostLayer<TestGraph, TestVertex>(new CostFactory(), range, random),
                new ObstacleLayer<TestGraph, TestVertex>(random, obstaclePercent)
            };
            var assemble = GetAssemble();
            return new TestCaseData(assemble, layers, dimensions);
        }

        private static TestCaseData GenerateTestCase(params int[] dimensionSizes)
        {
            var assemble = GetAssemble();
            return new TestCaseData(assemble, dimensionSizes)
                .SetDescription("Test of assembling logic of the GraphAssemble class");
        }

        private static GraphAssemble<TestGraph, TestVertex> GetAssemble()
        {
            var graphFactory = new TestGraphFactory();
            var coordinateFactory = new TestCoordinateFactory();
            var vertexFactory = new TestVertexFactory();
            return new GraphAssemble<TestGraph, TestVertex>(vertexFactory,
                coordinateFactory, graphFactory);
        }
    }
}
