using Common.Interface;
using NLog;
using System;

namespace Common
{
    public sealed class Logger : ILog
    {
        public static ILog Instance
        {
            get
            {
                if (instance == null)
                    instance = new Logger();
                return instance;
            }
        }

        public void Trace(string message) => Log(message, infoLogger.Trace);

        public void Warn(Exception ex, string message = null) => Log(ex, message, infoLogger.Warn);

        public void Error(Exception ex, string message = null) => Log(ex, message, errorLogger.Error);

        public void Fatal(Exception ex, string message = null) => Log(ex, message, errorLogger.Fatal);

        public void Info(string message) => Log(message, infoLogger.Info);

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
            if (string.IsNullOrEmpty(message))
            {
                action(ex, string.Empty);
            }
            else
            {
                action(ex, message);
            }
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

        private static ILog instance = null;
    }
}
