using Autofac;
using Pathfinding.App.WPF._3D.DependencyInjection;
using Pathfinding.App.WPF._3D.View;
using System.Windows;

namespace Pathfinding.App.WPF._3D
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
