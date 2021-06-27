using Common.ValueRanges;

namespace WPFVersion3D
{
    internal static class Constants
    {
        public static InclusiveValueRange<double> DistanceBetweenVerticesValueRange { get; }
        public static InclusiveValueRange<double> OpacityValueRange { get; }
        public static InclusiveValueRange<double> AngleValueRange { get; }
        public static InclusiveValueRange<double> ObstaclePercentValueRange { get; }
        public static InclusiveValueRange<double> AlgorithmDelayValueRange { get; }
        public static InclusiveValueRange<double> ZoomValueRange { get; }
        public static InclusiveValueRange<int> GraphWidthValueRange { get; }
        public static InclusiveValueRange<int> GraphLengthValueRange { get; }
        public static InclusiveValueRange<int> GraphHeightValueRange { get; }
        public static ValueRanges<int> GraphParamsValueRanges { get; }

        public const int InitialVertexSize = 5;

        public const double InitialVisitedVertexOpacity = 0.15;
        public const double InitialEnqueuedVertexOpacity = 0.15;
        public const double InitialPathVertexOpacity = 0.9;
        public const double InitialStartVertexOpacity = 1.0;
        public const double InitialEndVertexOpacity = 1.0;
        public const double InitialRegularVertexOpacity = 0.25;
        public const double InitialObstacleVertexOpacity = 0.2;

        static Constants()
        {
            DistanceBetweenVerticesValueRange = new InclusiveValueRange<double>(70, 0);
            ZoomValueRange = new InclusiveValueRange<double>(1000, 0);
            OpacityValueRange = new InclusiveValueRange<double>(1, 0);
            AngleValueRange = new InclusiveValueRange<double>(360, 0);
            ObstaclePercentValueRange = new InclusiveValueRange<double>(99, 0);
            AlgorithmDelayValueRange = new InclusiveValueRange<double>(35, 0);
            GraphWidthValueRange = new InclusiveValueRange<int>(25, 1);
            GraphLengthValueRange = GraphWidthValueRange;
            GraphHeightValueRange = GraphLengthValueRange;
            GraphParamsValueRanges = new ValueRanges<int>(
                GraphWidthValueRange,
                GraphLengthValueRange,
                GraphHeightValueRange
                );
        }
    }
}
