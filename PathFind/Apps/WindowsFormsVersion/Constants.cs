using Common;

namespace WindowsFormsVersion
{
    internal static class Constants
    {
        public static ValueRange GraphWidthValueRange { get; }
        public static ValueRange GraphLengthValueRange { get; }
        public static ValueRange ObstaclesPercentValueRange { get; }
        public static ValueRange AlgorithmDelayTimeValueRange { get; }

        public const int DistanceBetweenVertices = 1;
        public const int VertexSize = 24;
        public const float TextToSizeRatio = 0.35625f;

        static Constants()
        {
            GraphWidthValueRange = new ValueRange(67, 1);
            GraphLengthValueRange = new ValueRange(32, 1);
            ObstaclesPercentValueRange = new ValueRange(99, 0);
            AlgorithmDelayTimeValueRange = new ValueRange(35, 0);
        }
    }
}
