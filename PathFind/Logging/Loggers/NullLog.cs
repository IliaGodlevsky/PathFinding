using Logging.Interface;
using SingletonLib;
using System;

namespace Logging.Loggers
{
    public sealed class NullLog : Singleton<NullLog, ILog>, ILog
    {
        public void Debug(string message)
        {
            
        }

        public void Error(Exception ex, string message = null)
        {
            
        }

        public void Error(string message)
        {
           
        }

        public void Fatal(Exception ex, string message = null)
        {
            
        }

        public void Fatal(string message)
        {
            
        }

        public void Info(string message)
        {
            
        }

        public void Trace(string message)
        {
            
        }

        public void Warn(Exception ex, string message = null)
        {
            
        }

        public void Warn(string message)
        {
            
        }

        private NullLog()
        {

        }
    }
}
