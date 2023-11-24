using System;

namespace Pathfinding.App.Console.Messages
{
    internal sealed class AlgorithmKeyMessage
    {
        public Guid AlgorithmKey { get; }

        public AlgorithmKeyMessage(Guid algorithmKey)
        {
            AlgorithmKey = algorithmKey;
        }
    }
}
