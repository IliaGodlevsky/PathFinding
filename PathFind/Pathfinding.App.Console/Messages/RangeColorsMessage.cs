using System;

namespace Pathfinding.App.Console.Messages
{
    internal sealed class RangeColorsMessage
    {
        public ConsoleColor SourceColor { get; }

        public ConsoleColor TargetColor { get; }

        public ConsoleColor TransitColor { get; }

        public RangeColorsMessage(ConsoleColor sourceColor, ConsoleColor targetColor, ConsoleColor transitColor)
        {
            SourceColor = sourceColor;
            TargetColor = targetColor;
            TransitColor = transitColor;
        }
    }
}
