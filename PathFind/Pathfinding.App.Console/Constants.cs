using Shared.Primitives.ValueRange;
using System;

namespace Pathfinding.App.Console
{
    internal static class Constants
    {
        public static InclusiveValueRange<int> GraphWidthValueRange { get; }

        public static InclusiveValueRange<int> GraphLengthValueRange { get; }

        public static InclusiveValueRange<int> ObstaclesPercentValueRange { get; }

        public static InclusiveValueRange<TimeSpan> AlgorithmDelayTimeValueRange { get; }

        public static InclusiveValueRange<int> VerticesCostRange { get; }

        static Constants()
        {
            VerticesCostRange = new (99, 1);
            GraphWidthValueRange = new (75, 1);
            GraphLengthValueRange = new (45, 1);
            ObstaclesPercentValueRange = new (99);
            AlgorithmDelayTimeValueRange = new (TimeSpan.FromMilliseconds(50), TimeSpan.FromMilliseconds(1));
        }
    }
}
