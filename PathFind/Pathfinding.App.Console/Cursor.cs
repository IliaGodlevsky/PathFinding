using Shared.Primitives;
using System;
using System.Drawing;

namespace Pathfinding.App.Console
{
    /// <summary>
    /// Class, that represents a console cursor
    /// </summary>
    internal sealed class Cursor
    {
        private static string BufferLengthString
            => new(' ', Terminal.BufferWidth);

        private readonly int cursorLeft;
        private readonly int cursorRight;

        public static Point CurrentPosition
            => new(Terminal.CursorLeft, Terminal.CursorTop);

        private Cursor(int left, int right)
        {
            cursorLeft = left;
            cursorRight = right;
        }

        /// <summary>
        /// Hides cursor
        /// </summary>
        /// <returns>An object, disposing of 
        /// which shows cursor</returns>
        public static IDisposable HideCursor()
        {
            Terminal.CursorVisible = false;
            return Disposable.Use(ShowCursor);
        }

        /// <summary>
        /// Remembers the current position of the cursor
        /// </summary>
        /// <returns>An object, disposing which returns
        /// the cursor to the remembered position</returns>
        public static IDisposable UseCurrentPosition()
        {
            int cursorLeft = Terminal.CursorLeft;
            int cursorTop = Terminal.CursorTop;
            var cursor = new Cursor(cursorLeft, cursorTop);
            return Disposable.Use(cursor.RestorePosition);
        }

        /// <summary>
        /// Uses <paramref name="color"/> as 
        /// foreground color of console
        /// </summary>
        /// <param name="color"></param>
        /// <returns>An object, disposing which returns 
        /// previous foreground console color</returns>
        public static Disposable UseColor(ConsoleColor color)
        {
            var currentColor = Terminal.ForegroundColor;
            Terminal.ForegroundColor = color;
            void RestoreColor() => Terminal.ForegroundColor = currentColor;
            return Disposable.UseFast(RestoreColor);
        }

        public static void Write(ConsoleColor color, object text)
        {
            using (UseColor(color))
            {
                Terminal.Write(text);
            }
        }

        /// <summary>
        /// Remembers the current position of the cursor
        /// </summary>
        /// <returns>An object, disposing which 
        /// returns the cursor to the remembered 
        /// position and cleans all the input that has been
        /// perfromed after remembering the position </returns>
        public static IDisposable UseCurrentPositionWithClean()
        {
            var position = CurrentPosition;
            return Disposable.Use(() => CleanUpTo(position));
        }

        private static void CleanUpTo(Point offset)
        {
            int limit = Cursor.CurrentPosition.Y - offset.Y;
            SetPosition(offset);
            using (Cursor.UseCurrentPosition())
            {
                string emptyLine = BufferLengthString;
                while (limit-- >= 0)
                {
                    Terminal.Write(emptyLine);
                }
            }
        }

        public static void SetPosition(Point point)
        {
            Terminal.SetCursorPosition(point.X, point.Y);
        }

        private void RestorePosition()
        {
            Terminal.SetCursorPosition(cursorLeft, cursorRight);
        }

        private static void ShowCursor()
        {
            Terminal.CursorVisible = true;
        }
    }
}