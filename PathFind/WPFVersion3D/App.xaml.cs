using Autofac;
using GraphViewModel.Interfaces;
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

            var viewModel = container.Resolve<IMainModel>();

            var mainWindow = new MainWindow
            {
                DataContext = viewModel
            };

            mainWindow.Show();
        }
    }
}