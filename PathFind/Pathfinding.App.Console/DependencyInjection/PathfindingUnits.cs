using Pathfinding.App.Console.Units;
using Shared.Extensions;
using System;
using System.Linq;

namespace Pathfinding.App.Console.DependencyInjection
{
    internal static class PathfindingUnits
    {
        public static readonly Type[] AllUnits;
        public static readonly Type[] WithoutMain;
        public static readonly Type[] WithoutVisual;

        public static readonly Type Main = typeof(MainUnit);
        public static readonly Type Graph = typeof(GraphUnit);
        public static readonly Type History = typeof(PathfindingHistoryUnit);
        public static readonly Type Process = typeof(PathfindingProcessUnit);
        public static readonly Type Statistics = typeof(PathfindingStatisticsUnit);
        public static readonly Type Visual = typeof(PathfindingVisualizationUnit);
        public static readonly Type Range = typeof(PathfindingRangeUnit);
        public static readonly Type Colors = typeof(ColorsUnit);

        static PathfindingUnits()
        {
            AllUnits = typeof(PathfindingUnits).GetFields()
                .Where(field => field.FieldType == typeof(Type))
                .Select(field => (Type)field.GetValue(null))
                .ToArray();
            WithoutMain = AllUnits.Except(Main).ToArray();
            WithoutVisual = AllUnits.Except(Visual).ToArray();
        }
    }
}
