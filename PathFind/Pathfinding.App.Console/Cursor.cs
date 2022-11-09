using Pathfinding.GraphLib.Core.Realizations.Coordinates;
using Shared.Primitives;
using System;
using ColorfulConsole = Colorful.Console;

namespace Pathfinding.App.Console
{
    internal static class Cursor
    {
        private static int CursorLeft;
        private static int CursorRight;

        public static IDisposable HideCursor()
        {
            ColorfulConsole.CursorVisible = false;
            return Disposable.Use(ShowCursor);
        }

        public static IDisposable UseCurrentPosition()
        {
            CursorLeft = ColorfulConsole.CursorLeft;
            CursorRight = ColorfulConsole.CursorTop;
            return Disposable.Use(RestoreCursorPosition);
        }

        public static void SetPosition(Coordinate2D point)
        {
            ColorfulConsole.SetCursorPosition(point.X, point.Y);
        }

        private static void RestoreCursorPosition()
        {
            ColorfulConsole.SetCursorPosition(CursorLeft, CursorRight);
        }

        private static void ShowCursor()
        {
            ColorfulConsole.CursorVisible = true;
        }
    }
}