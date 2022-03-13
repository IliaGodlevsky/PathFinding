using Common;
using GraphLib.Realizations.Coordinates;
using System;
using Console = Colorful.Console;

namespace ConsoleVersion
{
    internal static class Cursor
    {
        public static IDisposable HideCursor()
        {
            Console.CursorVisible = false;
            return new Disposable(ShowCursor);
        }

        public static IDisposable UseCurrentPosition()
        {
            CursorLeft = Console.CursorLeft;
            CursorRight = Console.CursorTop;
            return new Disposable(RestoreCursorPosition);
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

        private static int CursorLeft;
        private static int CursorRight;
    }
}