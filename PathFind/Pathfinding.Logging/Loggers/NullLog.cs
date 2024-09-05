using Pathfinding.Logging.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinding.Logging.Loggers
{
    public sealed class NullLog : ILog
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
    }
}
