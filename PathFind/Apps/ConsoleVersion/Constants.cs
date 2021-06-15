using Common.ValueRanges;

namespace ConsoleVersion
{
    internal static class Constants
    {
        public static InclusiveValueRange<int> GraphWidthValueRange { get; }
        public static InclusiveValueRange<int> GraphLengthValueRange { get; }
        public static InclusiveValueRange<int> ObstaclesPercentValueRange { get; }
        public static InclusiveValueRange<int> AlgorithmDelayTimeValueRange { get; }

        public const int LateralDistanceBetweenVertices = 3;

        static Constants()
        {
            GraphWidthValueRange = new InclusiveValueRange<int>(80, 1);
            GraphLengthValueRange = new InclusiveValueRange<int>(50, 1);
            ObstaclesPercentValueRange = new InclusiveValueRange<int>(99, 0);
            AlgorithmDelayTimeValueRange = new InclusiveValueRange<int>(35, 0);
        }
    }
}
