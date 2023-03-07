using System;

namespace Pathfinding.App.Console.Messages
{
    internal sealed class PathfindingColorsMessage
    {
        public ConsoleColor VisitColor { get; }

        public ConsoleColor EnqueuColor { get; }

        public PathfindingColorsMessage(ConsoleColor visitColor, ConsoleColor enqueuColor)
        {
            VisitColor = visitColor;
            EnqueuColor = enqueuColor;
        }
    }
}