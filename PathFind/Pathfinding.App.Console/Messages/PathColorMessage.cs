using System;

namespace Pathfinding.App.Console.Messages
{
    internal sealed class PathColorMessage
    {
        public ConsoleColor PathColor { get; }

        public ConsoleColor CrossedPathColor { get; }

        public PathColorMessage(ConsoleColor pathColor, ConsoleColor crossedPathColor)
        {
            PathColor = pathColor;
            CrossedPathColor = crossedPathColor;
        }
    }
}
