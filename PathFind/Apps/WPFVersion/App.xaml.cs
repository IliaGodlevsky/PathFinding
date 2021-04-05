using Autofac;
using System.Windows;
using WPFVersion.Configure;
using WPFVersion.ViewModel;

namespace WPFVersion
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var container = ContainerConfigure.Configure();
            var viewModel = container.Resolve<MainWindowViewModel>();
            var mainWindow = new MainWindow { DataContext = viewModel };
            mainWindow.Show();
        }
    }
}