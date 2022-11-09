using NLog;
using Pathfinding.Logging.Interface;
using System;

namespace Pathfinding.Logging.Loggers
{
    public sealed class FileLog : ILog
    {
        public void Trace(string message)
        {
            Write(message, traceLogger.Trace);
        }

        public void Warn(Exception ex, string message = null)
        {
            Write(ex, message, errorLogger.Warn);
        }

        public void Warn(string message)
        {
            errorLogger.Warn(message);
        }

        public void Error(Exception ex, string message = null)
        {
            Write(ex, message, errorLogger.Error);
        }

        public void Error(string message)
        {
            errorLogger.Error(message);
        }

        public void Fatal(Exception ex, string message = null)
        {
            Write(ex, message, errorLogger.Fatal);
        }

        public void Fatal(string message)
        {
            errorLogger.Fatal(message);
        }

        public void Info(string message)
        {
            Write(message, infoLogger.Info);
        }

        public void Debug(string message)
        {
            Write(message, debugLogger.Debug);
        }

        private void Write(string message, Action<string> action)
        {
            action(message);
        }

        private void Write(Exception ex, string message,
            Action<Exception, string> action)
        {
            action(ex, string.IsNullOrEmpty(message) ? string.Empty : message);
        }

        public FileLog()
        {
            infoLogger = LogManager.GetLogger("Info");
            errorLogger = LogManager.GetLogger("Error");
            debugLogger = LogManager.GetLogger("Debug");
            traceLogger = LogManager.GetLogger("Trace");
        }

        private readonly ILogger infoLogger;
        private readonly ILogger debugLogger;
        private readonly ILogger errorLogger;
        private readonly ILogger traceLogger;
    }
}
