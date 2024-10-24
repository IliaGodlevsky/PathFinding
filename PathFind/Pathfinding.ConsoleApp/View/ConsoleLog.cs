using Pathfinding.Logging.Interface;
using System;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed class ConsoleLog : ILog
    {
        private void QueryMessageBox(string title, string message)
        {
            Application.MainLoop.Invoke(() =>
            {
                MessageBox.Query(title, message, "Ok");
            });
        }

        public void Debug(string message)
        {

        }

        public void Error(Exception ex, string message = null)
        {
            QueryMessageBox("Error", ex.Message);
        }

        public void Error(string message)
        {
            QueryMessageBox("Error", message);
        }

        public void Fatal(Exception ex, string message = null)
        {
            QueryMessageBox("Fatal", ex.Message);
        }

        public void Fatal(string message)
        {
            QueryMessageBox("Fatal", message);
        }

        public void Info(string message)
        {
            QueryMessageBox("Info", message);
        }

        public void Trace(string message)
        {

        }

        public void Warn(Exception ex, string message = null)
        {
            QueryMessageBox("Warn", ex.Message);
        }

        public void Warn(string message)
        {
            QueryMessageBox("Warn", message);
        }
    }
}
