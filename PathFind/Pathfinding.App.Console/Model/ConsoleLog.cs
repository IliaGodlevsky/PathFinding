using Pathfinding.Logging.Interface;
using System;
using System.Drawing;

using ColorfulConsole = Colorful.Console;

namespace Pathfinding.App.Console.Model
{
    internal sealed class ColorConsoleLog : ILog
    {
        public void Debug(string message)
        {
            
        }

        public void Error(Exception ex, string message = null)
        {
            Error(message);
            ColorfulConsole.Write(ex, Color.Red);
        }

        public void Error(string message)
        {
            ShowMessage(message, Color.Red);
        }

        public void Fatal(Exception ex, string message = null)
        {
            Fatal(message);
            ColorfulConsole.Write(ex, Color.DarkRed);
        }

        public void Fatal(string message)
        {
            ShowMessage(message, Color.DarkRed);
        }

        public void Info(string message)
        {
            
        }

        public void Trace(string message)
        {
            
        }

        public void Warn(Exception ex, string message = null)
        {
            Warn(message);
            ColorfulConsole.Write(ex, Color.Orange);
        }

        public void Warn(string message)
        {
            ShowMessage(message, Color.Orange);
        }

        private void ShowMessage(string message, Color color)
        {
            Screen.SetCursorPositionUnderMenu(1);
            ColorfulConsole.Write(message, color);
            ColorfulConsole.ReadKey();
        }
    }
}
