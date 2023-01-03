using Pathfinding.App.Console.Units;
using System;
using System.Linq;

namespace Pathfinding.App.Console.DependencyInjection
{
    internal static class RegistrationConstants
    {
        public const int IncludeCommand = 0;
        public const int ExcludeCommand = 1;

        public const string UnitTypeKey = "UnitType";
        public const string Order = "Order";
        public const string Key = "Key";

        public static readonly Type[] Units;

        public static readonly Type Main = typeof(MainUnit);
        public static readonly Type Graph = typeof(GraphUnit);
        public static readonly Type History = typeof(PathfindingHistoryUnit);
        public static readonly Type Process = typeof(PathfindingProcessUnit);
        public static readonly Type Statistics = typeof(PathfindingStatisticsUnit);
        public static readonly Type Visual = typeof(PathfindingVisualizationUnit);
        public static readonly Type Range = typeof(PathfindingRangeUnit);

        static RegistrationConstants()
        {
            Units = typeof(RegistrationConstants).GetFields()
                .Where(field => field.FieldType == typeof(Type))
                .Select(field => (Type)field.GetValue(null))
                .ToArray();
        }
    }
}