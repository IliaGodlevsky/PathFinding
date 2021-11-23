using Autofac;
using System.Windows;
using WPFVersion3D.Configure;

namespace WPFVersion3D
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var container = ContainerConfigure.Configure();
            var mainWindow = container.Resolve<MainWindow>();
            mainWindow.Show();
        }
    }
}