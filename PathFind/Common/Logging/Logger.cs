using NLog;
using System;

namespace Common
{
    public sealed class Logger
    {
        public static Logger Instance
        {
            get
            {
                if (instance == null)
                    instance = new Logger();
                return instance;                
            }
        }

        public void Trace(string message)
        {
            infoLogger.Trace(message);
        }

        public void Warn(Exception ex, string message=null)
        {
            if (string.IsNullOrEmpty(message))
            {
                infoLogger.Warn(ex, string.Empty);
            }
            else
            {
                infoLogger.Warn(ex, message);
            }
        }

        public void Error(Exception ex, string message=null)
        {
            if (string.IsNullOrEmpty(message))
            {
                errorLogger.Error(ex, string.Empty);
            }
            else
            {
                errorLogger.Error(ex, message);
            }
        }

        public void Fatal(Exception ex, string message=null)
        {            
            if(string.IsNullOrEmpty(message))
            {
                errorLogger.Fatal(ex, string.Empty);
            }
            else
            {
                errorLogger.Fatal(ex, message);
            }
        }

        public void Info(string message)
        {
            infoLogger.Info(message);
        }

        public void Debug(string message)
        {
#if DEBUG
            debugLogger.Debug(message);
#endif
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

        private static Logger instance = null;
    }
}
