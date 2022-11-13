using Pathfinding.AlgorithmLib.Core.Abstractions;

namespace Pathfinding.App.Console.Messages
{
    internal sealed class SubscribeOnVisualizationMessage
    {
        public PathfindingProcess Algorithm { get; }

        public SubscribeOnVisualizationMessage(PathfindingProcess algorithm)
        {
            Algorithm = algorithm;
        }
    }
}
