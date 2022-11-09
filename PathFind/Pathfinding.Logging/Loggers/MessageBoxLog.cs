using NLog;
using Pathfinding.Logging.Interface;
using System;

namespace Pathfinding.Logging.Loggers
{
    public sealed class MessageBoxLog : ILog
    {
        public void Trace(string message)
        {
            Write(message, messageBoxLogger.Trace);
        }

        public void Warn(Exception ex, string message = null)
        {
            Write(ex.Message + "\n" + message, messageBoxLogger.Warn);
        }

        public void Warn(string message)
        {
            messageBoxLogger.Warn(message);
        }

        public void Error(Exception ex, string message = null)
        {
            Write(ex.Message + "\n" + message, messageBoxLogger.Error);
        }

        public void Error(string message)
        {
            messageBoxLogger.Error(message);
        }

        public void Fatal(Exception ex, string message = null)
        {
            Write(ex.Message + "\n" + message, messageBoxLogger.Fatal);
        }

        public void Fatal(string message)
        {
            messageBoxLogger.Fatal(message);
        }

        public void Info(string message)
        {
            Write(message, messageBoxLogger.Info);
        }

        public void Debug(string message)
        {
            Write(message, messageBoxLogger.Debug);
        }

        private void Write(string message, Action<string> action)
        {
            action(message);
        }

        public MessageBoxLog()
        {
            messageBoxLogger = LogManager.GetLogger("MsgBox");
        }

        private readonly ILogger messageBoxLogger;
    }
}
