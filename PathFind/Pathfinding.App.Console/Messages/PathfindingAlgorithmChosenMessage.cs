using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Factory.Interface;

namespace Pathfinding.App.Console.Messages
{
    internal sealed class PathfindingAlgorithmChosenMessage
    {
        public IAlgorithmFactory<PathfindingProcess> Algorithm { get; }

        public PathfindingAlgorithmChosenMessage(IAlgorithmFactory<PathfindingProcess> algorithm)
        {
            Algorithm = algorithm;
        }
    }
}
