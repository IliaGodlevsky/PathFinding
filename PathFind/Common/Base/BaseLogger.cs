using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Common.Base
{
    public abstract class BaseLogger : ILog
    {
        public int CacheLimit { get; set; }

        public string LogsSeparator { get; set; }

        public string Path { get; set; }

        public virtual void Log(Exception ex)
        {
            string date = $"Date: {DateTime.Now:G}";
            string exceptionType = $"Exception type: { ex.GetType().Name}";
            string stackTrace = $"Stack trace: { ex.StackTrace}";
            string message = $"Message: { ex.Message}";
            string source = $"Source: {ex.Source}";
            string targetSite = $"Target site: {ex.TargetSite.Name}";
            string hResult = $"HResult: {ex.HResult}";

            string log = string.Join(separator: "\n", date, exceptionType, stackTrace,
                    message, source, targetSite, hResult);

            Log(log);
        }

        public virtual void Log(string format, params object[] paramters)
        {
            if (isEneable)
            {
                var log = string.Format(format, paramters);
                PutInCache(log);
            }
        }

        protected virtual void PutInCache(string log)
        {
            logCache.Add(log);
            if (logCache.Count >= CacheLimit)
            {
                LogCachedLogs();
            }
        }

        public virtual void Disable()
        {
            isEneable = false;
            LogCachedLogs();
        }

        public virtual void Enable()
        {
            isEneable = true;
        }

        public virtual void LogCachedLogs()
        {
            Task.Run(WriteCache);
        }

        protected virtual void WriteCache()
        {
            var builder = new StringBuilder();
            logCache.ForEach(log => builder.AppendLine(log).Append(LogsSeparator));
            logCache.Clear();

            try
            {
                using (var stream = new StreamWriter(Path, append: true))
                {
                    stream.Write(builder.ToString());
                }
            }
            catch(Exception ex)
            {
                string e = ex.Message;
                var charArray = e.ToCharArray();
            }
            
        }

        protected BaseLogger()
        {
            logCache = new List<string>();
            isEneable = true;
            LogsSeparator = new string('-', 15) + "\n";
        }

        private bool isEneable;

        private readonly List<string> logCache;
    }
}
