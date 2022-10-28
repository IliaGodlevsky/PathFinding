using Autofac;
using Logging.Interface;
using System.Threading.Tasks;
using System.Windows;
using WPFVersion.DependencyInjection;

namespace WPFVersion
{
    public partial class App : Application
    {
        private readonly ILog log;

        public App()
        {
            TaskScheduler.UnobservedTaskException += OnUnobservedTaskExceptionCaught;
            log = DI.Container.Resolve<ILog>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var window = DI.Container.Resolve<MainWindow>();
            window.Show();
        }

        private void OnUnobservedTaskExceptionCaught(object sender, UnobservedTaskExceptionEventArgs e)
        {
            log.Error(e.Exception);
        }
    }
}