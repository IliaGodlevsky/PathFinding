using Autofac;
using System.Windows;
using WPFVersion3D.DependencyInjection;

namespace WPFVersion3D
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindow = DI.Container.Resolve<MainWindow>();
            mainWindow.Show();
        }
    }
}