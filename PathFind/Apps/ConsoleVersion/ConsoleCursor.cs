using Common;
using GraphLib.Realizations.Coordinates;
using System;
using Console = Colorful.Console;

namespace ConsoleVersion
{
    internal static class Cursor
    {
        public static IDisposable UsePosition(Coordinate2D point)
        {
            SaveCursorPosition();
            Console.SetCursorPosition(point.X, point.Y);
            return new Disposable(RestoreCursorPosition);
        }

        private static void SaveCursorPosition()
        {
            CursorLeft = Console.CursorLeft;
            CursorRight = Console.CursorTop;
        }

        private static void RestoreCursorPosition()
        {
            Console.SetCursorPosition(CursorLeft, CursorRight);
        }

        private static int CursorLeft;
        private static int CursorRight;
    }
}