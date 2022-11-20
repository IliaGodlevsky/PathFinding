using Pathfinding.GraphLib.Core.Realizations.Coordinates;
using Shared.Primitives;
using System;

using ColorfulConsole = Colorful.Console;

namespace Pathfinding.App.Console
{
    internal sealed class Cursor
    {
        private static string BufferLengthString => new string(' ', ColorfulConsole.BufferWidth);

        private readonly int cursorLeft;
        private readonly int cursorRight;

        public static Coordinate2D CurrentPosition 
            => new Coordinate2D(ColorfulConsole.CursorLeft, ColorfulConsole.CursorTop);

        private Cursor(int left, int right)
        {
            cursorLeft = left;
            cursorRight = right;
        }

        public static IDisposable HideCursor()
        {
            ColorfulConsole.CursorVisible = false;
            return Disposable.Use(ShowCursor);
        }

        public static IDisposable UseCurrentPosition()
        {
            var cursorLeft = ColorfulConsole.CursorLeft;
            var cursorRight = ColorfulConsole.CursorTop;
            var cursor = new Cursor(cursorLeft, cursorRight);
            return Disposable.Use(cursor.RestoreCursorPosition);
        }

        public static IDisposable UsePositionAndClear()
        {
            var left = ColorfulConsole.CursorLeft;
            var top = ColorfulConsole.CursorTop;
            var position = new Coordinate2D(left, top);
            return Disposable.Use(() => ClearArea(position));
        }

        private static void ClearArea(Coordinate2D offset)
        {
            int limit = Cursor.CurrentPosition.Y - offset.Y;
            SetPosition(offset);
            using (Cursor.UseCurrentPosition())
            {
                string emptyLine = BufferLengthString;
                while (limit-- > 0)
                {
                    ColorfulConsole.Write(emptyLine);
                }
            }
        }

        public static void SetPosition(Coordinate2D point)
        {
            ColorfulConsole.SetCursorPosition(point.X, point.Y);
        }

        private void RestoreCursorPosition()
        {
            ColorfulConsole.SetCursorPosition(cursorLeft, cursorRight);
        }

        private static void ShowCursor()
        {
            ColorfulConsole.CursorVisible = true;
        }
    }
}