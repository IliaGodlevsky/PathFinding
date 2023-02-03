using Pathfinding.Logging.Interface;
using System;

using Log = System.Diagnostics.Debug;

namespace Pathfinding.App.Console.Model
{
    internal sealed class DebugLog : ILog
    {
        private void WriteLine(string message)
        {
            Log.WriteLine(message);
        }

        public void Debug(string message)
        {
            WriteLine(message);
        }

        public void Error(Exception ex, string message = null)
        {
            WriteLine(ex.ToString() + message);
        }

        public void Error(string message)
        {
            WriteLine(message);
        }

        public void Fatal(Exception ex, string message = null)
        {
            WriteLine(ex.ToString() + message);
        }

        public void Fatal(string message)
        {
            WriteLine(message);
        }

        public void Info(string message)
        {
            WriteLine(message);
        }

        public void Trace(string message)
        {
            WriteLine(message);
        }

        public void Warn(Exception ex, string message = null)
        {
            WriteLine(ex.ToString() + message);
        }

        public void Warn(string message)
        {
            WriteLine(message);
        }
    }
}
