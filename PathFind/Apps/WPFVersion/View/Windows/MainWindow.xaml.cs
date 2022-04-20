using System;
using System.Windows;
using WPFVersion.Attributes;
using WPFVersion.ViewModel;

namespace WPFVersion
{
    [AppWindow]
    public partial class MainWindow : Window
    {
        public MainWindow(MainWindowViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        protected override void OnClosed(EventArgs e)
        {
            (DataContext as IDisposable)?.Dispose();
            base.OnClosed(e);
        }
    }
}
