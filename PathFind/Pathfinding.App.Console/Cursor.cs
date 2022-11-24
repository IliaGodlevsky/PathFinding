using Pathfinding.GraphLib.Core.Realizations.Coordinates;
using Shared.Primitives;
using System;

namespace Pathfinding.App.Console
{
    internal sealed class Cursor
    {
        private static string BufferLengthString => new string(' ', System.Console.BufferWidth);

        private readonly int cursorLeft;
        private readonly int cursorRight;

        public static Coordinate2D CurrentPosition 
            => new Coordinate2D(System.Console.CursorLeft, System.Console.CursorTop);

        private Cursor(int left, int right)
        {
            cursorLeft = left;
            cursorRight = right;
        }

        public static IDisposable HideCursor()
        {
            System.Console.CursorVisible = false;
            return Disposable.Use(ShowCursor);
        }

        public static IDisposable RestoreCurrentPosition()
        {
            var cursorLeft = System.Console.CursorLeft;
            var cursorRight = System.Console.CursorTop;
            var cursor = new Cursor(cursorLeft, cursorRight);
            return Disposable.Use(cursor.RestoreCursorPosition);
        }

        public static IDisposable UseColor(ConsoleColor color)
        {
            var currentColor = System.Console.ForegroundColor;
            void Restore() => System.Console.ForegroundColor = currentColor;
            System.Console.ForegroundColor = color;
            return Disposable.Use(Restore);
        }

        public static IDisposable ClearUpAfter()
        {
            var left = System.Console.CursorLeft;
            var top = System.Console.CursorTop;
            var position = new Coordinate2D(left, top);
            return Disposable.Use(() => ClearArea(position));
        }

        private static void ClearArea(Coordinate2D offset)
        {
            int limit = Cursor.CurrentPosition.Y - offset.Y;
            SetPosition(offset);
            using (Cursor.RestoreCurrentPosition())
            {
                string emptyLine = BufferLengthString;
                while (limit-- >= 0)
                {
                    System.Console.Write(emptyLine);
                }
            }
        }

        public static void SetPosition(Coordinate2D point)
        {
            System.Console.SetCursorPosition(point.X, point.Y);
        }

        private void RestoreCursorPosition()
        {
            System.Console.SetCursorPosition(cursorLeft, cursorRight);
        }

        private static void ShowCursor()
        {
            System.Console.CursorVisible = true;
        }
    }
}