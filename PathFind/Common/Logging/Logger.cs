using Common.Interface;
using NLog;
using System;

namespace Common.Logging
{
    public sealed class Logger : ILog
    {
        public static ILog Instance => instance ?? (instance = new Logger());

        public void Trace(string message)
        {
            Log(message, traceLogger.Trace);
        }

        public void Warn(Exception ex, string message = null)
        {
            Log(ex, message, errorLogger.Warn);
        }

        public void Warn(string message)
        {
            errorLogger.Warn(message);
        }

        public void Error(Exception ex, string message = null)
        {
            Log(ex, message, errorLogger.Error);
        }

        public void Error(string message)
        {
            errorLogger.Error(message);
        }

        public void Fatal(Exception ex, string message = null)
        {
            Log(ex, message, errorLogger.Fatal);
        }

        public void Fatal(string message)
        {
            errorLogger.Fatal(message);
        }

        public void Info(string message)
        {
            Log(message, infoLogger.Info);
        }

        public void Debug(string message)
        {
#if DEBUG
            Log(message, debugLogger.Debug);
#endif
        }

        private void Log(string message, Action<string> action)
        {
            action(message);
        }

        private void Log(Exception ex, string message,
            Action<Exception, string> action)
        {
            action(ex, string.IsNullOrEmpty(message) ? string.Empty : message);
        }

        private Logger()
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

        private static ILog instance;
    }
}
