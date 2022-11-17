using System;

namespace Pathfinding.App.WPF._2D.Messages.DataMessages
{
    internal sealed class AlgorithmStatusMessage
    {
        public string Status { get; }

        public Guid Id { get; }

        public AlgorithmStatusMessage(string status, Guid id)
        {
            Id = id;
            Status = status;
        }
    }
}
