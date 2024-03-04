using Pathfinding.AlgorithmLib.Core.Interface;

namespace Pathfinding.App.Console.Messaging.Messages
{
    internal sealed class PathFoundMessage(IGraphPath path)
    {
        public IGraphPath Path { get; } = path;
    }
}
