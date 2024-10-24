using Pathfinding.Shared.Primitives;
using System.Collections;

namespace Pathfinding.Infrastructure.Business.Test.Mappings
{
    internal static class MappingTestsDataProvider
    {
        public static IEnumerable Coordinates
        {
            get
            {
                return new TestCaseData[]
                {
                    new TestCaseData(new int[] { 1, 2 }),
                    new TestCaseData(new int[] { 3, 4, 5 }),
                    new TestCaseData(new int[] { 6, 7, 8, 9 }),
                    new TestCaseData(new int[] { 10, 11, 12, 13, 14 })
                };
            }
        }

        public static IEnumerable CoordinateValuesToBytes
        {
            get
            {
                return new TestCaseData[]
                {
                    new TestCaseData(new int[] { 1, 2 })
                        .Returns(new byte[] { 91, 49, 44, 50, 93 }),

                    new TestCaseData(new int[] { 3, 4, 5 })
                        .Returns(new byte[] { 91, 51, 44, 52, 44, 53, 93 }),

                    new TestCaseData(new int[] { 6, 7, 8, 9 })
                        .Returns(new byte[] { 91, 54, 44, 55, 44, 56, 44, 57, 93 }),

                    new TestCaseData(new int[] { 10, 11, 12, 13, 14 })
                        .Returns(new byte[] { 91, 49, 48, 44, 49, 49, 44, 49, 50, 44, 49, 51, 44, 49, 52, 93 }),
                };
            }
        }

        public static IEnumerable CoordinateToBytes
        {
            get
            {
                return new[]
                {
                    new TestCaseData(new Coordinate(1, 2))
                        .Returns(new byte[] { 91, 49, 44, 50, 93 }),

                    new TestCaseData(new Coordinate(3, 4, 5))
                        .Returns(new byte[] { 91, 51, 44, 52, 44, 53, 93 }),

                    new TestCaseData(new Coordinate(6, 7, 8, 9))
                        .Returns(new byte[] { 91, 54, 44, 55, 44, 56, 44, 57, 93 }),

                    new TestCaseData(new Coordinate(10, 11, 12, 13, 14))
                        .Returns(new byte[] { 91, 49, 48, 44, 49, 49, 44, 49, 50, 44, 49, 51, 44, 49, 52, 93 })
                };
            }
        }
    }
}
