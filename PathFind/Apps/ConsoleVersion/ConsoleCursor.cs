using Common;
using System;

namespace ConsoleVersion
{
    internal static class ConsoleCursor
    {
        public static IDisposable UseCursorPosition(int left, int top)
        {
            SaveCursorPosition();
            Console.SetCursorPosition(left, top);
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
