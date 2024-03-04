using Pathfinding.AlgorithmLib.Core.Abstractions;

namespace Pathfinding.App.Console.Messaging.Messages
{
    internal sealed class AlgorithmMessage(PathfindingProcess algorithm)
    {
        public PathfindingProcess Algorithm { get; } = algorithm;
    }
}
