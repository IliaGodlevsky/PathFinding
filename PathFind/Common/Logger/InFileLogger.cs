using Common.Logger.Interface;
using System;
using System.IO;

namespace Common.Logger
{
    public sealed class InFileLogger : ILog
    {
        public static InFileLogger Instance
        {
            get
            {
                if (instance == null)
                    instance = new InFileLogger();
                return instance;
            }
        }

        public string Path { get; set; }

        public void Log(Exception ex)
        {
            using (stream = new StreamWriter(Path, append: true))
            {
                string date = string.Format("{0}-{1}-{2}: {3}:{4}:{5}",
                      DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year,
                      DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

                string log = string.Format("Date: {0}, exception type {1}, stack trace: {2}, message: {3}",
                    date, ex.GetType().Name, ex.StackTrace, ex.Message + "\n");
                stream.Write(log);
            }
        }

        public void Log(string format, params object[] paramters)
        {
            using (stream = new StreamWriter(Path, append: true))
            { 
                stream.Write(string.Format(format, paramters)); 
            }
        }

        private InFileLogger()
        {
            Path = "logfile.txt";
        }

        private StreamWriter stream;
        private static InFileLogger instance = null;
    }
}
