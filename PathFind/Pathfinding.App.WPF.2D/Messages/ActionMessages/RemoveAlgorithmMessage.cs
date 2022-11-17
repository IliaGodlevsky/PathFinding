using System;

namespace Pathfinding.App.WPF._2D.Messages.ActionMessages
{
    internal sealed class RemoveAlgorithmMessage
    {
        public Guid Id { get; }

        public RemoveAlgorithmMessage(Guid id)
        {
            Id = id;
        }
    }
}
