using Algorithm.Algorithms;
using Algorithm.PathFindingAlgorithms;
using NUnit.Framework;
using System.Collections;

namespace Algorithm.Tests.TestsInfrastructure.Factories
{
    internal class TestCasesFactory
    {
        internal static IEnumerable AlgorithmsWithGraphParamsTestCases
        {
            get
            {
                yield return new TestCaseData(new LeeAlgorithm(), new int[] { 67, 32 });
                yield return new TestCaseData(new BestFirstLeeAlgorithm(), new int[] { 4, 5, 6, 7 });
                yield return new TestCaseData(new DijkstraAlgorithm(), new int[] { 14, 15, 11 });
                yield return new TestCaseData(new AStarAlgorithm(), new int[] { 14 });
                yield return new TestCaseData(new AStarModified(), new int[] { 50, 24 });
                yield return new TestCaseData(new DistanceFirstAlgorithm(), new int[] { 12, 5, 9 });
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
    }
}
