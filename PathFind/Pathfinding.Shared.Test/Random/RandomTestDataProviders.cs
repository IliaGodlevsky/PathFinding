using System.Collections;

namespace Pathfinding.Shared.Test.Random
{
    internal static class RandomTestDataProviders
    {
        public static IEnumerable XorshiftRandomDataProvider
        {
            get
            {
                var list = new List<TestCaseData>();
                int limit = 5;
                while (limit-- > 1)
                {
                    var testCase = new TestCaseData(1000 * limit, limit * 0.75);
                    list.Add(testCase);
                }
                return list;
            }
        }

        public static IEnumerable CryptoRandomDataProvider
        {
            get
            {
                var list = new List<TestCaseData>();
                int limit = 5;
                while (limit-- > 1)
                {
                    var testCase = new TestCaseData(1000 * limit, limit * 1.25);
                    list.Add(testCase);
                }
                return list;
            }
        }

        public static IEnumerable CongruentialRandomDataProvider
        {
            get
            {
                var list = new List<TestCaseData>();
                int limit = 5;
                while (limit-- > 1)
                {
                    var testCase = new TestCaseData(1000 * limit, limit * 0.1);
                    list.Add(testCase);
                }
                return list;
            }
        }
    }
}
