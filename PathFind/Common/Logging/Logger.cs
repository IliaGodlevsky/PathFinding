using System;
using Common.Interface;
using NLog;

namespace Common.Logging
{
    public sealed class Logger : ILog
    {
        public static ILog Instance => instance ?? (instance = new Logger());

        public void Trace(string message)
        {
            Log(message, infoLogger.Trace);
        }

        public void Warn(Exception ex, string message = null)
        {
            Log(ex, message, infoLogger.Warn);
        }

        public void Error(Exception ex, string message = null)
        {
            Log(ex, message, errorLogger.Error);
        }

        public void Fatal(Exception ex, string message = null)
        {
            Log(ex, message, errorLogger.Fatal);
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

        private static void Log(string message, Action<string> action)
        {
            action(message);
        }

        private static void Log(Exception ex, string message,
            Action<Exception, string> action)
        {
            action(ex, string.IsNullOrEmpty(message) ? string.Empty : message);
        }

        private Logger()
        {
            infoLogger = LogManager.GetLogger("Info");
            errorLogger = LogManager.GetLogger("Error");
            debugLogger = LogManager.GetLogger("Debug");
        }

        private readonly NLog.Logger infoLogger;
        private readonly NLog.Logger debugLogger;
        private readonly NLog.Logger errorLogger;

        private static ILog instance;
    }
}
