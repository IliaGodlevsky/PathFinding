using Pathfinding.AlgorithmLib.Core.Abstractions;

namespace Pathfinding.App.Console.Messages
{
    internal sealed class SubscribeOnHistoryMessage : IHistoryMessage
    {
        public PathfindingProcess Algorithm { get; }

        public SubscribeOnHistoryMessage(PathfindingProcess algorithm)
        {
            Algorithm = algorithm;
        }
    }
}
