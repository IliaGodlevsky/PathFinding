using Common.ValueRanges;

namespace WPFVersion3D
{
    internal static class Constants
    {
        public static IValueRange ObstaclePercentValueRange { get; }
        public static IValueRange AlgorithmDelayValueRange { get; }
        public static IValueRange GraphWidthValueRange { get; }
        public static IValueRange GraphLengthValueRange { get; }
        public static IValueRange GraphHeightValueRange { get; }
        public static ValueRanges GraphParamsValueRanges { get; }

        public const double InitialRotationAnimationDuration = 3000;

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
            ObstaclePercentValueRange = new LowInclusiveValueRange(100, 0);
            AlgorithmDelayValueRange = new InclusiveValueRange(35, 0);
            GraphWidthValueRange = new UpInclusiveValueRange(30, 0);
            GraphLengthValueRange = new UpInclusiveValueRange(30, 0);
            GraphHeightValueRange = new UpInclusiveValueRange(30, 0);
            GraphParamsValueRanges = new ValueRanges(GraphWidthValueRange,
                GraphLengthValueRange,
                GraphHeightValueRange
                );
        }
    }
}
