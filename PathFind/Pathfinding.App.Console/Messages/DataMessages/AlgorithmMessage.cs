using Pathfinding.AlgorithmLib.Core.Abstractions;

namespace Pathfinding.App.Console.Messages.DataMessages
{
    internal class AlgorithmMessage<T> : DataMessage<T>
    {
        public PathfindingProcess Algorithm { get; }

        public AlgorithmMessage(PathfindingProcess algorithm, T value)
            : base(value)
        {
            Algorithm = algorithm;
        }
    }
}
