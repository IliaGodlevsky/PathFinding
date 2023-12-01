using Pathfinding.AlgorithmLib.Core.Interface;

namespace Pathfinding.App.Console.Messages
{
    internal sealed class PathFoundMessage
    {
        public PathFoundMessage(IGraphPath path)
        {
            Path = path;
        }

        public IGraphPath Path { get; }
    }
}
