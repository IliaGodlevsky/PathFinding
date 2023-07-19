using Pathfinding.Logging.Interface;
using System;

namespace Pathfinding.App.Console.Loggers
{
    internal sealed class ConsoleLog : ILog
    {
        public void Debug(string message)
        {

        }

        public void Error(Exception ex, string message = null)
        {
            ShowMessage(message + ex.Message, ConsoleColor.Red);
        }

        public void Error(string message)
        {
            ShowMessage(message, ConsoleColor.Red);
        }

        public void Fatal(Exception ex, string message = null)
        {
            ShowMessage(message + ex.Message, ConsoleColor.DarkRed);
        }

        public void Fatal(string message)
        {
            ShowMessage(message, ConsoleColor.DarkRed);
        }

        public void Info(string message)
        {

        }

        public void Trace(string message)
        {

        }

        public void Warn(Exception ex, string message = null)
        {
            ShowMessage(message + ex.Message, ConsoleColor.DarkYellow);
        }

        public void Warn(string message)
        {
            ShowMessage(message, ConsoleColor.DarkYellow);
        }

        private void ShowMessage(string message, ConsoleColor color)
        {
            AppLayout.SetCursorPositionUnderGraphField();
            using (Cursor.UseCurrentPositionWithClean())
            {
                using (Cursor.UseColor(color))
                {
                    System.Console.Write(message);
                    System.Console.ReadKey();
                }
            }
        }
    }
}
