using Pathfinding.Shared.Primitives;
using System.Collections;

namespace Pathfinding.Infrastructure.Business.Test.Mappings
{
    internal static class CoordinatesDataProvider
    {
        public static IEnumerable Coordinates
        {
            get
            {
                return new TestCaseData[]
                {
                    new TestCaseData(new int[] { 1, 2 } ),
                    new TestCaseData(new int[] { 3, 4, 5 }),
                    new TestCaseData(new int[] { 6, 7, 8, 9 }),
                    new TestCaseData(new int[] { 10, 11, 12, 13, 14 }),
                    new TestCaseData(new int[] { 15, 16 }),
                    new TestCaseData(new int[] { 17, 18, 19 }),
                    new TestCaseData(new int[] { 20, 21, 22, 23 }),
                    new TestCaseData(new int[] { 24, 25, 26, 27, 28 }),
                    new TestCaseData(new int[] { 29, 30 }),
                    new TestCaseData(new int[] { 31, 32, 33, 34, 35 })
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

                    new TestCaseData(new int[] { 15, 16 })
                        .Returns(new byte[] { 91, 49, 53, 44, 49, 54, 93 }),

                    new TestCaseData(new int[] { 17, 18, 19 })
                        .Returns(new byte[] { 91, 49, 55, 44, 49, 56, 44, 49, 57, 93 }),

                    new TestCaseData(new int[] { 20, 21, 22, 23 })
                        .Returns(new byte[] { 91, 50, 48, 44, 50, 49, 44, 50, 50, 44, 50, 51, 93 }),

                    new TestCaseData(new int[] { 24, 25, 26, 27, 28 })
                        .Returns(new byte[] { 91, 50, 52, 44, 50, 53, 44, 50, 54, 44, 50, 55, 44, 50, 56, 93 }),

                    new TestCaseData(new int[] { 29, 30 })
                        .Returns(new byte[] { 91, 50, 57, 44, 51, 48, 93 })
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
                        .Returns(new byte[] { 91, 49, 48, 44, 49, 49, 44, 49, 50, 44, 49, 51, 44, 49, 52, 93 }),

                    new TestCaseData(new Coordinate(15, 16))
                        .Returns(new byte[] { 91, 49, 53, 44, 49, 54, 93 }),

                    new TestCaseData(new Coordinate(17, 18, 19))
                        .Returns(new byte[] { 91, 49, 55, 44, 49, 56, 44, 49, 57, 93 }),

                    new TestCaseData(new Coordinate(20, 21, 22, 23))
                        .Returns(new byte[] { 91, 50, 48, 44, 50, 49, 44, 50, 50, 44, 50, 51, 93 }),

                    new TestCaseData(new Coordinate(24, 25, 26, 27, 28))
                        .Returns(new byte[] { 91, 50, 52, 44, 50, 53, 44, 50, 54, 44, 50, 55, 44, 50, 56, 93 }),

                    new TestCaseData(new Coordinate(29, 30))
                        .Returns(new byte[] { 91, 50, 57, 44, 51, 48, 93 })
                };
            }
        }
    }
}
