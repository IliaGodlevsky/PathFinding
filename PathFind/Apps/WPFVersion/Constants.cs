using Common;

namespace WPFVersion
{
    internal static class Constants
    {
        public static ValueRange GraphWidthValueRange { get; }
        public static ValueRange GraphLengthValueRange { get; }
        public static ValueRange ObstaclesPercentValueRange { get; }
        public static ValueRange AlgorithmDelayTimeValueRange { get; }
        public static ValueRange VertexSizeRange { get; }

        public const int DistanceBetweenVertices = 1;
        public const int VertexSize = 24;
        public const double TextToSizeRatio = 0.475;

        static Constants()
        {
            GraphWidthValueRange = new ValueRange(150, 1);
            GraphLengthValueRange = new ValueRange(75, 1);
            ObstaclesPercentValueRange = new ValueRange(99, 0);
            AlgorithmDelayTimeValueRange = new ValueRange(35, 0);
            VertexSizeRange = new ValueRange(30, 5);
        }
    }
}
