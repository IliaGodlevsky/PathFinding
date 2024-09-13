using Pathfinding.Logging.Interface;
using System;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed class ConsoleLog : ILog
    {
        public void Debug(string message)
        {

        }

        public void Error(Exception ex, string message = null)
        {
            MessageBox.Query(50, 7, "Error", ex.Message, "OK");
        }

        public void Error(string message)
        {
            MessageBox.Query(50, 7, "Error", message, "OK");
        }

        public void Fatal(Exception ex, string message = null)
        {
            MessageBox.Query(50, 7, "Fatal", ex.Message, "OK");
        }

        public void Fatal(string message)
        {
            MessageBox.Query(50, 7, "Error", message, "OK");
        }

        public void Info(string message)
        {

        }

        public void Trace(string message)
        {

        }

        public void Warn(Exception ex, string message = null)
        {
            MessageBox.Query(50, 7, "Warn", ex.Message, "OK");
        }

        public void Warn(string message)
        {
            MessageBox.Query(50, 7, "Error", message, "OK");
        }
    }
}
