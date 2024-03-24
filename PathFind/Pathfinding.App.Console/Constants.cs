global using Terminal = System.Console;

using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.Settings;
using Shared.Primitives.ValueRange;
using System;
using System.ComponentModel;

namespace System.Runtime.CompilerServices
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public record IsExternalInit;
}

namespace Pathfinding.App.Console
{
    internal static class Constants
    {
        public const string Title = "Pathfinding";

        private static readonly Parametres Settings = Parametres.Default;

        public const string ObstacleColorKey = nameof(Languages.ObstacleColor);
        public const string RegularColorKey = nameof(Languages.RegularColor);
        public const string SourceColorKey = nameof(Languages.SourceColor);
        public const string TargetColorKey = nameof(Languages.TargetColor);
        public const string TransitColorKey = nameof(Languages.TransitColor);
        public const string PathColorKey = nameof(Languages.PathColor);
        public const string CrossedPathColorKey = nameof(Languages.CrossedPathColor);
        public const string VisitedColorKey = nameof(Languages.VisitedColor);
        public const string EnqueuedColorKey = nameof(Languages.EnqueuedColor);

        public static readonly string[] RangeColorKeys = new[]
        {
            SourceColorKey, TargetColorKey, TransitColorKey
        };

        public static readonly string[] PathColorKeys = new[]
        {
            PathColorKey, CrossedPathColorKey
        };

        public static InclusiveValueRange<int> GraphWidthValueRange { get; }

        public static InclusiveValueRange<int> GraphLengthValueRange { get; }

        public static InclusiveValueRange<int> ObstaclesPercentValueRange { get; }

        public static InclusiveValueRange<int> CycleModeValueRange { get; }

        public static InclusiveValueRange<TimeSpan> AlgorithmDelayTimeValueRange { get; }

        public static InclusiveValueRange<int> VerticesCostRange { get; }

        static Constants()
        {
            CycleModeValueRange = new(2, 1);
            VerticesCostRange = new(Settings.MaxCost, Settings.MinCost);
            GraphWidthValueRange = new(Settings.MaxGraphWidth, Settings.MinGraphWidth);
            GraphLengthValueRange = new(Settings.MaxGraphLength, Settings.MinGraphLength);
            ObstaclesPercentValueRange = new(
                Settings.MaxObstaclePercentValue,
                Settings.MinObstaclePercentValue);
            AlgorithmDelayTimeValueRange = new(
                TimeSpan.FromMilliseconds(Settings.VisualizationDelayMaxValue),
                TimeSpan.FromMilliseconds(Settings.VisualizationDelayMinValue));
        }
    }
}
