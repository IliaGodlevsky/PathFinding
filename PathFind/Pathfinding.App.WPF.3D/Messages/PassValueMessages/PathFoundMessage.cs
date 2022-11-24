using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Interface;
using System;

namespace Pathfinding.App.WPF._3D.Messages.PassValueMessages
{
    internal sealed class PathFoundMessage : PassValueMessage<IGraphPath>
    {
        public Guid Id { get; }

        public PathFoundMessage(IGraphPath value, Guid id) : base(value)
        {
            Id = id;
        }
    }
}
