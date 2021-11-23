using Autofac;
using System.Windows;
using WPFVersion.Configure;

namespace WPFVersion
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var container = ContainerConfigure.Configure();
            container.Resolve<MainWindow>();
        }
    }
}