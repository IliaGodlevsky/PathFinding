﻿using Shared.Primitives.ValueRange;
using System;

namespace Pathfinding.App.WPF._2D
{
    internal static class Constants
    {
        public static InclusiveValueRange<double> CostValueRange { get; }

        public static InclusiveValueRange<double> GraphFieldScaleValueRange { get; }

        public static InclusiveValueRange<int> GraphWidthValueRange { get; }

        public static InclusiveValueRange<int> GraphLengthValueRange { get; }

        public static InclusiveValueRange<double> ObstaclesPercentValueRange { get; }

        public static InclusiveValueRange<TimeSpan> AlgorithmDelayTimeValueRange { get; }

        public static InclusiveValueRange<double> OffsetValueRange { get; }

        public const int DistanceBetweenVertices = 1;
        public const int VertexSize = 24;
        private const int WidthBase = 12;
        private const int LengthBase = 5;
        private const int Leverage = 50;

        static Constants()
        {
            CostValueRange = new InclusiveValueRange<double>(99, 1);
            GraphFieldScaleValueRange = new InclusiveValueRange<double>(2.5, 0.1);
            GraphWidthValueRange = new InclusiveValueRange<int>(Leverage * WidthBase, 1);
            GraphLengthValueRange = new InclusiveValueRange<int>(Leverage * LengthBase, 1);
            ObstaclesPercentValueRange = new InclusiveValueRange<double>(99, 0);
            AlgorithmDelayTimeValueRange = new InclusiveValueRange<TimeSpan>(TimeSpan.FromMilliseconds(35), TimeSpan.FromMilliseconds(5));
            OffsetValueRange = new InclusiveValueRange<double>(-1000, 1000);
        }
    }
}
