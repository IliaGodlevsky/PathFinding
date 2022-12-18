﻿using Pathfinding.GraphLib.Core.Realizations.Coordinates;
using Shared.Primitives;
using System;

namespace Pathfinding.App.Console
{
    /// <summary>
    /// Class, that represents a console cursor
    /// </summary>
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

        /// <summary>
        /// Hides cursor
        /// </summary>
        /// <returns>An object, disposing of 
        /// which shows cursor</returns>
        public static IDisposable HideCursor()
        {
            System.Console.CursorVisible = false;
            return Disposable.Use(ShowCursor);
        }

        /// <summary>
        /// Remembers the current position of the cursor
        /// </summary>
        /// <returns>An object, disposing which returns
        /// the cursor to the remembered position</returns>
        public static IDisposable UseCurrentPosition()
        {
            var cursorLeft = System.Console.CursorLeft;
            var cursorRight = System.Console.CursorTop;
            var cursor = new Cursor(cursorLeft, cursorRight);
            return Disposable.Use(cursor.RestorePosition);
        }

        /// <summary>
        /// Uses <paramref name="color"/> as 
        /// foreground color of console
        /// </summary>
        /// <param name="color"></param>
        /// <returns>An object, disposing which returns 
        /// previous foreground console color</returns>
        public static IDisposable UseColor(ConsoleColor color)
        {
            var currentColor = System.Console.ForegroundColor;
            void RestoreColor()
            {
                System.Console.ForegroundColor = currentColor;
            }
            System.Console.ForegroundColor = color;
            return Disposable.Use(RestoreColor);
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
            var left = System.Console.CursorLeft;
            var top = System.Console.CursorTop;
            var position = new Coordinate2D(left, top);
            return Disposable.Use(() => CleanUpTo(position));
        }

        private static void CleanUpTo(Coordinate2D offset)
        {
            int limit = Cursor.CurrentPosition.Y - offset.Y;
            SetPosition(offset);
            using (Cursor.UseCurrentPosition())
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

        private void RestorePosition()
        {
            System.Console.SetCursorPosition(cursorLeft, cursorRight);
        }

        private static void ShowCursor()
        {
            System.Console.CursorVisible = true;
        }
    }
}