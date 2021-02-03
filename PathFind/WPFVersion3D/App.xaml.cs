using Autofac;
using Common;
using GraphViewModel.Interfaces;
using System;
using System.Configuration;
using System.Windows;
using WPFVersion3D.Configure;

namespace WPFVersion3D
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            string path = "WPFVersion3DLogs.txt";
            string cacheLimit = ConfigurationManager.AppSettings["cacheLimit"];

            Logger.Instance.Path = path;
            Logger.Instance.CacheLimit = Convert.ToInt32(cacheLimit);

            var container = ContainerConfigure.Configure();

            var viewModel = container.Resolve<IMainModel>();

            var mainWindow = new MainWindow
            {
                DataContext = viewModel
            };

            mainWindow.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            Logger.Instance.LogCachedLogs();
        }
    }
}
