using Pathfinding.App.Console.Units;
using System;

namespace Pathfinding.App.Console.DependencyInjection
{
    internal static class PathfindingUnits
    {
        public static readonly Type Main = typeof(MainUnit);
        public static readonly Type Graph = typeof(GraphUnit);
        public static readonly Type History = typeof(PathfindingHistoryUnit);
        public static readonly Type Process = typeof(PathfindingProcessUnit);
        public static readonly Type Statistics = typeof(PathfindingStatisticsUnit);
        public static readonly Type Visual = typeof(PathfindingVisualizationUnit);
        public static readonly Type Range = typeof(PathfindingRangeUnit);
        public static readonly Type Colors = typeof(ColorsUnit);
        public static readonly Type Editor = typeof(GraphEditorUnit);
        public static readonly Type KeysUnit = typeof(KeysUnit);
    }
}
