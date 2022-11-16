using Autofac;
using Pathfinding.App.WPF._2D.View;
using System.Windows;
using WPFVersion.DependencyInjection;

namespace Pathfinding.App.WPF._2D
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var window = DI.Container.Resolve<MainWindow>();
            window.Show();
        }
    }
}
