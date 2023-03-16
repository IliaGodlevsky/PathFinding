using Pathfinding.AlgorithmLib.Core.Abstractions;
using System;

namespace Pathfinding.App.Console.Messages.DataMessages
{
    internal class AlgorithmMessage<T> : DataMessage<T>
    {
        public Guid Id => Algorithm.Id;

        public PathfindingProcess Algorithm { get; }

        public AlgorithmMessage(PathfindingProcess algorithm, T value)
            : base(value)
        {
            Algorithm = algorithm;
        }
    }
}
