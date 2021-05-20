using Common.ValueRanges;

namespace WindowsFormsVersion
{
    internal static class Constants
    {
        public static IValueRange GraphWidthValueRange { get; }
        public static IValueRange GraphLengthValueRange { get; }
        public static IValueRange ObstaclesPercentValueRange { get; }
        public static IValueRange AlgorithmDelayTimeValueRange { get; }
        public static ValueRanges GraphParamsValueRanges { get; }

        public const int DistanceBetweenVertices = 1;
        public const int VertexSize = 24;
        public const float TextToSizeRatio = 0.35625f;

        static Constants()
        {
            GraphWidthValueRange = new UpInclusiveValueRange(67, 0);
            GraphLengthValueRange = new UpInclusiveValueRange(32, 0);
            ObstaclesPercentValueRange = new LowInclusiveValueRange(100, 0);
            AlgorithmDelayTimeValueRange = new InclusiveValueRange(35, 0);
            GraphParamsValueRanges = new ValueRanges(GraphWidthValueRange, GraphLengthValueRange);
        }
    }
}
