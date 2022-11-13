﻿using Shared.Primitives.ValueRange;
using System;

namespace Pathfinding.App.Console
{
    internal static class Constants
    {
        public static int HeightOfAbscissaView => 2;

        public static int HeightOfGraphParametresView => 1;

        public static int YCoordinatePadding => WidthOfOrdinateView - 1;

        public static int WidthOfOrdinateView => (GraphLengthValueRange.UpperValueOfRange - 1).ToString().Length + 1;

        public static InclusiveValueRange<int> GraphWidthValueRange { get; }

        public static InclusiveValueRange<int> GraphLengthValueRange { get; }

        public static InclusiveValueRange<int> ObstaclesPercentValueRange { get; }

        public static InclusiveValueRange<TimeSpan> AlgorithmDelayTimeValueRange { get; }

        public static InclusiveValueRange<int> VerticesCostRange { get; }

        static Constants()
        {
            VerticesCostRange = new InclusiveValueRange<int>(99, 1);
            GraphWidthValueRange = new InclusiveValueRange<int>(75, 1);
            GraphLengthValueRange = new InclusiveValueRange<int>(45, 1);
            ObstaclesPercentValueRange = new InclusiveValueRange<int>(99);
            AlgorithmDelayTimeValueRange = new InclusiveValueRange<TimeSpan>(TimeSpan.FromMilliseconds(35), TimeSpan.FromMilliseconds(1));
        }
    }
}