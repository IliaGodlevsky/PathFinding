using Pathfinding.App.Console.Model.Notes;

namespace Pathfinding.App.Console.Messaging.Messages
{
    internal sealed class StatisticsMessage
    {
        public Statistics Statistics { get; }

        public StatisticsMessage(Statistics statistics)
        {
            Statistics = statistics;
        }
    }
}
