using Pathfinding.AlgorithmLib.Core.Interface;
using System;

namespace Pathfinding.App.WPF._2D.Messages.DataMessages
{
    internal sealed class PathFoundMessage
    {
        public Guid Id { get; }

        public IGraphPath Path { get; }

        public PathFoundMessage(Guid id, IGraphPath path)
        {
            Id = id;
            Path = path;
        }
    }
}
