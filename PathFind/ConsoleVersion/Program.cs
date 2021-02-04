using Autofac;
using Common;
using ConsoleVersion.App;
using ConsoleVersion.Configure;
using System;
using System.Configuration;

namespace ConsoleVersion
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string path = ConfigurationManager.AppSettings["logfile"];
            string cacheLimit = ConfigurationManager.AppSettings["cacheLimit"];

            Logger.Instance.Path = path;
            Logger.Instance.CacheLimit = Convert.ToInt32(cacheLimit);

            var container = ContainerConfigure.Configure();

            using (var scope = container.BeginLifetimeScope())
            {
                var app = scope.Resolve<Application>();
                app.Run();
            }

            Logger.Instance.LogCachedLogs();
        }
    }
}
