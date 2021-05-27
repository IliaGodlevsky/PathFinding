using Common.ValueRanges;

namespace ConsoleVersion
{
    internal static class Constants
    {
        public static IValueRange GraphWidthValueRange { get; }
        public static IValueRange GraphLengthValueRange { get; }
        public static IValueRange ObstaclesPercentValueRange { get; }
        public static IValueRange AlgorithmDelayTimeValueRange { get; }

        public const int LateralDistanceBetweenVertices = 3;

        static Constants()
        {
            GraphWidthValueRange = new InclusiveValueRange(80, 1);
            GraphLengthValueRange = new InclusiveValueRange(50, 1);
            ObstaclesPercentValueRange = new InclusiveValueRange(99, 0);
            AlgorithmDelayTimeValueRange = new InclusiveValueRange(35, 0);
        }
    }
}
