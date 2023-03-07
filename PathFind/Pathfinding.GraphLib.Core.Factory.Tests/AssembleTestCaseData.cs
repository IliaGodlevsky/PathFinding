using NUnit.Framework;
using System.Collections;

namespace Pathfinding.GraphLib.Core.Factory.Tests
{
    internal static class AssembleTestCaseData
    {
        private static readonly string Description =
            "Test of assembling logic of the GraphAssemble class";

        public static IEnumerable Data
        {
            get
            {
                yield return GenerateTestCase(10, 20);
                yield return GenerateTestCase(8, 12, 16);
                yield return GenerateTestCase(5, 6, 7, 8);
                yield return GenerateTestCase(2, 3, 4, 5, 6);
            }
        }

        private static TestCaseData GenerateTestCase(params int[] dimensionSizes)
        {
            return new TestCaseData(dimensionSizes).SetDescription(Description);
        }
    }
}
