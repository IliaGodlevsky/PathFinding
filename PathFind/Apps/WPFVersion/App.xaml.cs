using Autofac;
using System.Windows;
using WPFVersion.DependencyInjection;

namespace WPFVersion
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