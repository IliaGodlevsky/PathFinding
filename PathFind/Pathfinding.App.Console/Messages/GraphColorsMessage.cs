using System;

namespace Pathfinding.App.Console.Messages
{
    internal sealed class GraphColorsMessage
    {
        public ConsoleColor RegularColor { get; }

        public ConsoleColor ObstacleColor { get; }

        public GraphColorsMessage(ConsoleColor regularColor, ConsoleColor obstacleColor)
        {
            RegularColor = regularColor;
            ObstacleColor = obstacleColor;
        }
    }
}
