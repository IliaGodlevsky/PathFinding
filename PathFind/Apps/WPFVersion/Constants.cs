using ValueRange;

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
        private const int WidthBase = 12;
        private const int LengthBase = 5;
        private const int Leverage = 10;

        static Constants()
        {
            GraphFieldScaleValueRange = new InclusiveValueRange<double>(2.5, 0.1);
            GraphWidthValueRange = new InclusiveValueRange<int>(Leverage * WidthBase, 1);
            GraphLengthValueRange = new InclusiveValueRange<int>(Leverage * LengthBase, 1);
            ObstaclesPercentValueRange = new InclusiveValueRange<double>(99, 0);
            AlgorithmDelayTimeValueRange = new InclusiveValueRange<double>(35, 1);
            OffsetValueRange = new InclusiveValueRange<double>(-1000, 1000);
        }
    }
}
