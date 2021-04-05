using Autofac;
using System.Windows;
using WPFVersion3D.Configure;
using WPFVersion3D.ViewModel;

namespace WPFVersion3D
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