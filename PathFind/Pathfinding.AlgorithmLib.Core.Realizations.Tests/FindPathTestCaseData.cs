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
                yield return GenerateTestCase(new DijkstraAlgorithmFactory(), new TestPathfindingRange(), DijkstraExpectedPath.Instance);
                yield return GenerateTestCase(new AStarAlgorithmFactory(), new TestPathfindingRange(), DijkstraExpectedPath.Instance);
                yield return GenerateTestCase(new IDAStarAlgorithmFactory(), new TestPathfindingRange(), DijkstraExpectedPath.Instance);
                yield return GenerateTestCase(new LeeAlgorithmFactory(), new TestPathfindingRange(), LeeAlgorithmExpectedPath.Instance);
            }
        }

        private static TestCaseData GenerateTestCase(IAlgorithmFactory<IAlgorithm<IGraphPath>> factory, IEnumerable<IVertex> range, IGraphPath examplePath)
        {
            string rangeCount = $"Range: {range.Count()} vertices";
            string expectedPath = $"Expected: Cost: {examplePath.Cost}, Count: {examplePath.Count}";
            return new TestCaseData(factory, range, examplePath).SetArgDisplayNames(factory.ToString(), rangeCount, expectedPath);
        }
    }
}