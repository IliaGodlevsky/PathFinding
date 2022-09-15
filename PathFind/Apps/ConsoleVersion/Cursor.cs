using Common;
using GraphLib.Realizations.Coordinates;
using System;
using Console = Colorful.Console;

namespace ConsoleVersion
{
    internal static class Cursor
    {
        private static int CursorLeft;
        private static int CursorRight;

        public static IDisposable HideCursor()
        {
            Console.CursorVisible = false;
            return Disposing.Use(ShowCursor);
        }

        public static IDisposable UseCurrentPosition()
        {
            CursorLeft = Console.CursorLeft;
            CursorRight = Console.CursorTop;
            return Disposing.Use(RestoreCursorPosition);
        }

        public static void SetPosition(Coordinate2D point)
        {
            Console.SetCursorPosition(point.X, point.Y);
        }

        private static void RestoreCursorPosition()
        {
            Console.SetCursorPosition(CursorLeft, CursorRight);
        }

        private static void ShowCursor()
        {
            Console.CursorVisible = true;
        }
    }
}