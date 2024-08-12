using Pathfinding.Infrastructure.Business.Algorithms;

namespace Pathfinding.App.Console.Messaging.Messages
{
    internal sealed class AlgorithmMessage(PathfindingProcess algorithm)
    {
        public PathfindingProcess Algorithm { get; } = algorithm;
    }
}
