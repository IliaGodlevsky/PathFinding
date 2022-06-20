using System;
using ValueRange;

namespace WindowsFormsVersion
{
    internal static class Constants
    {
        public static InclusiveValueRange<int> GraphWidthValueRange { get; }

        public static InclusiveValueRange<int> GraphLengthValueRange { get; }

        public static InclusiveValueRange<int> ObstaclesPercentValueRange { get; }

        public static InclusiveValueRange<TimeSpan> AlgorithmDelayTimeValueRange { get; }

        public const int DistanceBetweenVertices = 1;
        public const int VertexSize = 24;
        public const float TextToSizeRatio = 0.35625f;

        static Constants()
        {
            GraphWidthValueRange = new InclusiveValueRange<int>(67, 1);
            GraphLengthValueRange = new InclusiveValueRange<int>(32, 1);
            ObstaclesPercentValueRange = new InclusiveValueRange<int>(99, 0);
            AlgorithmDelayTimeValueRange = new InclusiveValueRange<TimeSpan>(TimeSpan.FromMilliseconds(35), TimeSpan.FromMilliseconds(1));
        }
    }
}
