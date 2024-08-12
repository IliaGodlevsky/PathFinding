using Pathfinding.Service.Interface.Models.Undefined;

namespace Pathfinding.App.Console.Messaging.Messages
{
    internal sealed class StatisticsMessage
    {
        public RunStatisticsModel Statistics { get; }

        public StatisticsMessage(RunStatisticsModel statistics)
        {
            Statistics = statistics;
        }
    }
}
