using Pathfinding.AlgorithmLib.Core.Abstractions;

namespace Pathfinding.App.Console.Messaging.Messages
{
    internal sealed class AlgorithmMessage
    {
        public PathfindingProcess Algorithm { get; }

        public AlgorithmMessage(PathfindingProcess algorithm)
        {
            Algorithm = algorithm;
        }
    }
}
