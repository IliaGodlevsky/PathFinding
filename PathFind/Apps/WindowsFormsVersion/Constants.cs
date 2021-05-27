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
            GraphWidthValueRange = new InclusiveValueRange(67, 1);
            GraphLengthValueRange = new InclusiveValueRange(32, 1);
            ObstaclesPercentValueRange = new InclusiveValueRange(99, 0);
            AlgorithmDelayTimeValueRange = new InclusiveValueRange(35, 0);
            GraphParamsValueRanges = new ValueRanges(GraphWidthValueRange, GraphLengthValueRange);
        }
    }
}
