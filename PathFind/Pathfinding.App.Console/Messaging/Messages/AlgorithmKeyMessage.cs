using System;

namespace Pathfinding.App.Console.Messaging.Messages
{
    internal sealed class AlgorithmKeyMessage
    {
        public int AlgorithmKey { get; }

        public AlgorithmKeyMessage(int algorithmKey)
        {
            AlgorithmKey = algorithmKey;
        }
    }
}
