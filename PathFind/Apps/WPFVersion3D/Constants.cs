using Common;

namespace WPFVersion3D
{
    public static class Constants
    {
        public static ValueRange ObstaclePercentValueRange { get; }
        public static ValueRange AlgorithmDelayValueRange { get; }
        public static ValueRange GraphWidthValueRange { get; }
        public static ValueRange GraphLengthValueRange { get; }
        public static ValueRange GraphHeightValueRange { get; }

        public const double InitialRotationAnimationDuration = 3000; // milliseconds

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
            ObstaclePercentValueRange = new ValueRange(99, 0);
            AlgorithmDelayValueRange = new ValueRange(35, 0);
            GraphWidthValueRange = new ValueRange(30, 0);
            GraphLengthValueRange = new ValueRange(30, 0);
            GraphHeightValueRange = new ValueRange(30, 0);
        }
    }
}
