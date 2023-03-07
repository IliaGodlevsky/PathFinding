using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Interface;

namespace Pathfinding.App.Console.Messages
{
    internal sealed class PathFoundMessage
    {
        public IGraphPath Path { get; }

        public PathfindingProcess Algorithm { get; }

        public PathFoundMessage(IGraphPath path, PathfindingProcess algorithm)
        {
            Path = path;
            Algorithm = algorithm;
        }
    }
}
