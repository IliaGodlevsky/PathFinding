using Pathfinding.AlgorithmLib.Core.Abstractions;

namespace Pathfinding.App.Console.Messages
{
    internal sealed class SubscribeOnStatisticsMessage
    {
        public PathfindingProcess Algorithm { get; }

        public SubscribeOnStatisticsMessage(PathfindingProcess algorithm)
        {
            Algorithm = algorithm;
        }
    }
}
