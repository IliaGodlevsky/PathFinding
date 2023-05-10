namespace Pathfinding.App.Console.DependencyInjection
{
    internal static class RegistrationConstants
    {
        public const int IncludeCommand = 0;
        public const int ExcludeCommand = 1;

        public const string UnitTypeKey = "UnitType";
        public const string Order = "Order";
        public const string Key = "Key";
        public const string PathfindingRange = "Range";
        public const string ChangeCost = "Cost";
        public const string Reverse = "Reverse";
        public const string VisualTypeKey = "VisualType";

        public const string Group = "Group";
        public const int GreedGroup = 0;
        public const int WaveGroup = 1;
        public const int BreadthGroup = 2;
    }
}