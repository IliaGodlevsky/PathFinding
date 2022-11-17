namespace Pathfinding.App.Console.Messages
{
    internal sealed class UpdatePathfindingStatisticsMessage
    {
        public static readonly UpdatePathfindingStatisticsMessage Empty
            = new UpdatePathfindingStatisticsMessage(string.Empty);

        public string Statistics { get; }

        public UpdatePathfindingStatisticsMessage(string statistics)
        {
            Statistics = statistics;
        }
    }
}