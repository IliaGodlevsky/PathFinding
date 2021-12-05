using System;

namespace ConsoleVersion
{
    internal static class ConsoleCursor
    {
        private static int CursorLeft;
        private static int CursorRight;

        public static void SaveCursorPosition()
        {
            CursorLeft = Console.CursorLeft;
            CursorRight = Console.CursorTop;
        }

        public static void RestoreCursorPosition()
        {
            Console.SetCursorPosition(CursorLeft, CursorRight);
        }
    }
}
