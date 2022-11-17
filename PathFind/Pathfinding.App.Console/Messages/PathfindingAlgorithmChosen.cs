using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Factory.Interface;

namespace Pathfinding.App.Console.Messages
{
    internal sealed class PathfindingAlgorithmChosen
    {
        public IAlgorithmFactory<PathfindingProcess> Algorithm { get; }

        public PathfindingAlgorithmChosen(IAlgorithmFactory<PathfindingProcess> algorithm)
        {
            Algorithm = algorithm;
        }
    }
}
