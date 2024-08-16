using System.Collections;

namespace Pathfinding.Infrastructure.Business.Test.Algorithms.DataProviders
{
    internal static class ChebyshevDistanceDataProvider
    {
        public static IEnumerable TestData
        {
            get
            {
                yield return new TestCaseData(new int[] { }, new int[] { }).Returns(0.0);
                yield return new TestCaseData(new int[] { 3 }, new int[] { 3 }).Returns(0.0);
                yield return new TestCaseData(new int[] { 1, 2, 3 }, new int[] { 1, 2, 3 }).Returns(0.0);
                yield return new TestCaseData(new int[] { 1, 2, 3 }, new int[] { 1, 4, 3 }).Returns(2.0);
                yield return new TestCaseData(new int[] { 1, 2, 3 }, new int[] { 3, 5, 1 }).Returns(3.0);
                yield return new TestCaseData(new int[] { 0, 0, 0 }, new int[] { 1, 2, 3 }).Returns(3.0);
                yield return new TestCaseData(new int[] { -1, -2, -3 }, new int[] { 1, 2, 3 }).Returns(6.0);
                yield return new TestCaseData(new int[] { 1, 2, 3, 4 }, new int[] { 1, 2 }).Returns(0.0);
                yield return new TestCaseData(new int[] { 1 }, new int[] { 3, 4, 5 }).Returns(2.0);
                yield return new TestCaseData(new int[] { -1, -2, -3 }, new int[] { 1, 2 }).Returns(4.0);
                yield return new TestCaseData(new int[] { 1, 2, 3 }, new int[] { 4, 2 }).Returns(3.0);
                yield return new TestCaseData(new int[] { }, new int[] { 1, 2, 3 }).Returns(0.0);
                yield return new TestCaseData(new int[] { 1, 2, 3 }, new int[] { }).Returns(0.0);
                yield return new TestCaseData(new int[] { 1, 2, -3 }, new int[] { -1, -2, 3 }).Returns(6.0);
                yield return new TestCaseData(new int[] { 5, 5, 5 }, new int[] { 5, 5, 5 }).Returns(0.0);
                yield return new TestCaseData(new int[] { 5, 5, 5 }, new int[] { 3, 3, 3 }).Returns(2.0);
            }
        }
    }

}
