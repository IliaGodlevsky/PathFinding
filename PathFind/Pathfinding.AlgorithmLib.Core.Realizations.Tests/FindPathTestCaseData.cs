using NUnit.Framework;
using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.AlgorithmLib.Core.Realizations.Tests.Realizations;
using Pathfinding.AlgorithmLib.Core.Realizations.Tests.Realizations.GraphPaths;
using Pathfinding.AlgorithmLib.Factory;
using Pathfinding.AlgorithmLib.Factory.Interface;
using Pathfinding.GraphLib.Core.Interface;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.AlgorithmLib.Core.Realizations.Tests
{
    internal static class FindPathTestCaseData
    {
        public static IEnumerable Data
        {
            get
            {
                yield return GenerateTestCase(new DijkstraAlgorithmFactory(), TestPathfindingRange.Interface, DijkstraExpectedPath.Interface);
                yield return GenerateTestCase(new AStarAlgorithmFactory(), TestPathfindingRange.Interface, DijkstraExpectedPath.Interface);
                yield return GenerateTestCase(new IDAStarAlgorithmFactory(), TestPathfindingRange.Interface, DijkstraExpectedPath.Interface);
                yield return GenerateTestCase(new LeeAlgorithmFactory(), TestPathfindingRange.Interface, LeeAlgorithmExpectedPath.Interface);
            }
        }

        public static IEnumerable Factories
        {
            get
            {
                yield return GenerateFactory(new DijkstraAlgorithmFactory());
                yield return GenerateFactory(new AStarAlgorithmFactory());
                yield return GenerateFactory(new IDAStarAlgorithmFactory());
                yield return GenerateFactory(new LeeAlgorithmFactory());
                yield return GenerateFactory(new RandomAlgorithmFactory());
                yield return GenerateFactory(new AStarLeeAlgorithmFactory());
            }
        }

        private static TestCaseData GenerateFactory(IAlgorithmFactory<IAlgorithm<IGraphPath>> factory)
        {
            return new TestCaseData(factory).SetArgDisplayNames(factory.ToString());
        }

        private static TestCaseData GenerateTestCase(IAlgorithmFactory<IAlgorithm<IGraphPath>> factory, IEnumerable<IVertex> range, IGraphPath examplePath)
        {
            string rangeCount = $"Range: {range.Count()} vertices";
            string expectedPath = $"Expected: Cost: {examplePath.Cost}, Count: {examplePath.Count}";
            return new TestCaseData(factory, range, examplePath).SetArgDisplayNames(factory.ToString(), rangeCount, expectedPath);
        }
    }
}