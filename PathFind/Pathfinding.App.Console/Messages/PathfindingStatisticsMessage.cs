namespace Pathfinding.App.Console.Messages
{
    internal sealed class PathfindingStatisticsMessage
    {
        public static readonly PathfindingStatisticsMessage Empty
            = new PathfindingStatisticsMessage(string.Empty);

        public string Statistics { get; }

        public PathfindingStatisticsMessage(string statistics)
        {
            Statistics = statistics;
        }
    }
}