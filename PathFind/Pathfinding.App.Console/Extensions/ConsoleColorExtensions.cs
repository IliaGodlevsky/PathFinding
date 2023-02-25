using System;

namespace Pathfinding.App.Console.Extensions
{
    internal static class ConsoleColorExtensions
    {
        public static string GetName(this ConsoleColor color)
        {
            return color switch
            {
                ConsoleColor.DarkGreen => "Dark green",
                ConsoleColor.DarkCyan => "Dark cyan",
                ConsoleColor.DarkGray => "Dark gray",
                ConsoleColor.DarkBlue => "Dark blue",
                ConsoleColor.DarkMagenta => "Dark magenta",
                ConsoleColor.DarkRed => "Dark red",
                ConsoleColor.DarkYellow => "Dark yellow",
                _ => color.ToString()
            };
        }
    }
}
