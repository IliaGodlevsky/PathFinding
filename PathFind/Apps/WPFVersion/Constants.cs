using Common.ValueRanges;

namespace WPFVersion
{
    internal static class Constants
    {
        public static IValueRange GraphWidthValueRange { get; }
        public static IValueRange GraphLengthValueRange { get; }
        public static IValueRange ObstaclesPercentValueRange { get; }
        public static IValueRange AlgorithmDelayTimeValueRange { get; }
        public static IValueRange VertexSizeRange { get; }

        public static ValueRanges GraphParamsValueRanges { get; }

        public const int DistanceBetweenVertices = 1;
        public const int VertexSize = 24;
        public const double TextToSizeRatio = 0.475;

        static Constants()
        {
            GraphWidthValueRange = new UpInclusiveValueRange(150, 0);
            GraphLengthValueRange = new UpInclusiveValueRange(75, 0);
            GraphParamsValueRanges = new ValueRanges(GraphWidthValueRange, GraphLengthValueRange);
            ObstaclesPercentValueRange = new LowInclusiveValueRange(100, 0);
            AlgorithmDelayTimeValueRange = new InclusiveValueRange(35, 0);
            VertexSizeRange = new InclusiveValueRange(30, 5);
        }
    }
}
