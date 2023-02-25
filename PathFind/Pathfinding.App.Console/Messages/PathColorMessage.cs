using System;

namespace Pathfinding.App.Console.Messages
{
    internal sealed class PathColorMessage
    {
        public ConsoleColor PathColor { get; }

        public PathColorMessage(ConsoleColor pathColor)
        {
            PathColor = pathColor;
        }
    }
}
