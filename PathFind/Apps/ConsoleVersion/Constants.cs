using Common;

namespace ConsoleVersion
{
    internal static class Constants
    {
        public static ValueRange GraphWidthValueRange { get; }
        public static ValueRange GraphLengthValueRange { get; }
        public static ValueRange ObstaclesPercentValueRange { get; }
        public static ValueRange AlgorithmDelayTimeValueRange { get; }

        public const int LateralDistanceBetweenVertices = 3;

        static Constants()
        {
            GraphWidthValueRange = new ValueRange(80, 1);
            GraphLengthValueRange = new ValueRange(50, 1);
            ObstaclesPercentValueRange = new ValueRange(99, 0);
            AlgorithmDelayTimeValueRange = new ValueRange(35, 0);
        }
    }
}
