using Pathfinding.App.Console.Settings;
using Shared.Primitives.ValueRange;
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Windows.Forms.PropertyGridInternal;

namespace Pathfinding.App.Console
{
    internal static class Constants
    {
        private static readonly Parametres Settings = Parametres.Default;

        public static InclusiveValueRange<int> GraphWidthValueRange { get; }

        public static InclusiveValueRange<int> GraphLengthValueRange { get; }

        public static InclusiveValueRange<int> ObstaclesPercentValueRange { get; }

        public static InclusiveValueRange<TimeSpan> AlgorithmDelayTimeValueRange { get; }

        public static InclusiveValueRange<int> VerticesCostRange { get; }

        static Constants()
        {
            VerticesCostRange = new(Settings.MaxCost, Settings.MinCost);
            GraphWidthValueRange = new(Settings.MaxGraphWidth, Settings.MinGraphWidth);
            GraphLengthValueRange = new(Settings.MaxGraphLength, Settings.MinGraphLength);
            ObstaclesPercentValueRange = new(Settings.MaxObstaclePercentValue, Settings.MinObstaclePercentValue);
            double maxDelatTimeMilliseconds = Settings.VisualizationDelayMaxValue;
            double minDelatTimeMilliseconds = Settings.VisualizationDelayMinValue;
            AlgorithmDelayTimeValueRange = new(
                TimeSpan.FromMilliseconds(maxDelatTimeMilliseconds), 
                TimeSpan.FromMilliseconds(minDelatTimeMilliseconds));
        }
    }
}
