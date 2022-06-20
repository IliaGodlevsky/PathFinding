using System;
using ValueRange;

namespace WPFVersion3D
{
    internal static class Constants
    {
        public static InclusiveValueRange<double> DistanceBetweenVerticesValueRange { get; }

        public static InclusiveValueRange<double> OpacityValueRange { get; }

        public static InclusiveValueRange<double> AngleValueRange { get; }

        public static InclusiveValueRange<double> ObstaclePercentValueRange { get; }

        public static InclusiveValueRange<TimeSpan> AlgorithmDelayValueRange { get; }

        public static InclusiveValueRange<double> ZoomValueRange { get; }

        public static InclusiveValueRange<int> GraphWidthValueRange { get; }

        public static InclusiveValueRange<int> GraphLengthValueRange { get; }

        public static InclusiveValueRange<int> GraphHeightValueRange { get; }

        public static InclusiveValueRange<double> FieldOfViewValueRange { get; }

        public const int InitialVertexSize = 5;
        public const double DistanceBase = 20;
        public const double ZoomBase = 200;

        public const double InitialVisitedVertexOpacity = 0.15;
        public const double InitialEnqueuedVertexOpacity = 0.15;
        public const double InitialPathVertexOpacity = 0.9;
        public const double InitialSourceVertexOpacity = 1.0;
        public const double InitialTargetVertexOpacity = 1.0;
        public const double InitialRegularVertexOpacity = 0.25;
        public const double InitialObstacleVertexOpacity = 0.2;

        static Constants()
        {
            DistanceBetweenVerticesValueRange = new InclusiveValueRange<double>(DistanceBase * InitialVertexSize, 0);
            ZoomValueRange = new InclusiveValueRange<double>(ZoomBase * InitialVertexSize, 0);
            OpacityValueRange = new InclusiveValueRange<double>(1, 0);
            AngleValueRange = new InclusiveValueRange<double>(360, 0);
            ObstaclePercentValueRange = new InclusiveValueRange<double>(99, 0);
            AlgorithmDelayValueRange = new InclusiveValueRange<TimeSpan>(TimeSpan.FromMilliseconds(35), TimeSpan.FromMilliseconds(1));
            GraphWidthValueRange = new InclusiveValueRange<int>(9, 1);
            FieldOfViewValueRange = new InclusiveValueRange<double>(360, 0);
            GraphLengthValueRange = GraphWidthValueRange;
            GraphHeightValueRange = GraphLengthValueRange;
        }
    }
}
