using Common.Interface;
using System;

namespace ConsoleVersion.Model
{
    internal sealed class ConsoleLog : ILog
    {
        public ConsoleLog()
        {

        }

        public void Debug(string message)
        {

        }

        public void Error(Exception ex, string message = null)
        {
            Console.WriteLine(ex.Message + "\n" + message);
            Console.ReadLine();
        }

        public void Error(string message)
        {
            Console.WriteLine(message);
            Console.ReadLine();
        }

        public void Fatal(Exception ex, string message = null)
        {
            Console.WriteLine(ex.Message + "\n" + message);
            Console.ReadLine();
        }

        public void Fatal(string message)
        {
            Console.WriteLine(message);
            Console.ReadLine();
        }

        public void Info(string message)
        {

        }

        public void Trace(string message)
        {

        }

        public void Warn(Exception ex, string message = null)
        {
            Console.WriteLine(ex.Message + "\n" + message);
            Console.ReadLine();
        }

        public void Warn(string message)
        {
            Console.WriteLine(message);
            Console.ReadLine();
        }
    }
}
