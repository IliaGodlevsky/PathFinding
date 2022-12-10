using System;

namespace Pathfinding.App.Console.Messages
{
    internal sealed class HistoryPageMessage
    {
        public Guid PageKey { get; }

        public HistoryPageMessage(Guid pageKey)
        {
            PageKey = pageKey;
        }
    }
}
