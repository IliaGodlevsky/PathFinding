using Pathfinding.App.WPF._3D.Attributes;
using Pathfinding.App.WPF._3D.Interface;
using System;
using System.Windows;

namespace Pathfinding.App.WPF._3D.View
{
    [AppWindow]
    public abstract class ViewModelWindow : Window
    {
        protected ViewModelWindow(IViewModel viewModel)
        {
            DataContext = viewModel;
            viewModel.WindowClosed += Close;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        protected override void OnClosed(EventArgs e)
        {
            (DataContext as IDisposable)?.Dispose();
            base.OnClosed(e);
        }
    }
}
