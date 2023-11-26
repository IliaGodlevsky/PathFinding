using Pathfinding.App.Console.DataAccess;

namespace Pathfinding.App.Console.Messages
{
    internal sealed class HistoryImportedMessage
    {
        public GraphsPathfindingHistory History { get; }

        public HistoryImportedMessage(GraphsPathfindingHistory history)
        {
            History = history;
        }
    }
}
