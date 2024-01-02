using Pathfinding.AlgorithmLib.Core.Interface;

namespace Pathfinding.App.Console.Messaging.Messages
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
