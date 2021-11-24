using Common.Interface;
using System;
using System.Windows;
using WPFVersion.Attributes;

namespace WPFVersion.View.Windows
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
