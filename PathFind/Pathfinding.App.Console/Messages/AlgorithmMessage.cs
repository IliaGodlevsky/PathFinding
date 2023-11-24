using Pathfinding.AlgorithmLib.Core.Abstractions;

namespace Pathfinding.App.Console.Messages
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
