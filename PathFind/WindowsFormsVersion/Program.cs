using Autofac;
using Common;
using System;
using System.Configuration;
using System.Windows.Forms;
using WindowsFormsVersion.Configure;

namespace WindowsFormsVersion
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            string path = ConfigurationManager.AppSettings["logfile"];
            string cacheLimit = ConfigurationManager.AppSettings["cacheLimit"];

            Logger.Instance.Path = path;
            Logger.Instance.CacheLimit = Convert.ToInt32(cacheLimit);

            var container = ContainerConfigure.Configure();
            using (var scope = container.BeginLifetimeScope())
            {
                Application.ApplicationExit += OnApplicationClosed;
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(scope.Resolve<Form>());
            }
        }

        private static void OnApplicationClosed(object sender, EventArgs e)
        {
            Logger.Instance.LogCachedLogs();
        }
    }
}
