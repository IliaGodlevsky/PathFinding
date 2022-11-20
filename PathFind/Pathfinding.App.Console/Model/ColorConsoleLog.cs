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
            ShowMessage(message + ex.ToString(), Color.Red);
        }

        public void Error(string message)
        {
            ShowMessage(message, Color.Red);
        }

        public void Fatal(Exception ex, string message = null)
        {
            ShowMessage(message + ex.ToString(), Color.DarkRed);
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
            ShowMessage(message + ex.ToString(), Color.Orange);
        }

        public void Warn(string message)
        {
            ShowMessage(message, Color.Orange);
        }

        private void ShowMessage(string message, Color color)
        {
            //Screen.SetCursorPositionUnderMenu(1);            
            using (Cursor.UsePositionAndClear())
            {
                ColorfulConsole.Write(message, color);
                ColorfulConsole.ReadKey();
            }
        }
    }
}
