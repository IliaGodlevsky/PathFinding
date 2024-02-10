using Pathfinding.App.Console.DAL.Models.TransferObjects.Undefined;

namespace Pathfinding.App.Console.Messaging.Messages
{
    internal sealed class StatisticsMessage
    {
        public RunStatisticsDto Statistics { get; }

        public StatisticsMessage(RunStatisticsDto statistics)
        {
            Statistics = statistics;
        }
    }
}
