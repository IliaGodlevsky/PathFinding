using Common.ValueRanges;

namespace WPFVersion
{
    internal static class Constants
    {
        public static InclusiveValueRange<double> GraphFieldScaleValueRange { get; }
        public static InclusiveValueRange<int> GraphWidthValueRange { get; }
        public static InclusiveValueRange<int> GraphLengthValueRange { get; }
        public static InclusiveValueRange<double> ObstaclesPercentValueRange { get; }
        public static InclusiveValueRange<double> AlgorithmDelayTimeValueRange { get; }
        public static InclusiveValueRange<double> OffsetValueRange { get; }

        public const int DistanceBetweenVertices = 1;
        public const int VertexSize = 24;

        static Constants()
        {
            GraphFieldScaleValueRange = new InclusiveValueRange<double>(2.5, 0.1);
            GraphWidthValueRange = new InclusiveValueRange<int>(96, 1);
            GraphLengthValueRange = new InclusiveValueRange<int>(40, 1);
            ObstaclesPercentValueRange = new InclusiveValueRange<double>(99, 0);
            AlgorithmDelayTimeValueRange = new InclusiveValueRange<double>(35, 1);
            OffsetValueRange = new InclusiveValueRange<double>(-1000, 1000);
        }
    }
}
