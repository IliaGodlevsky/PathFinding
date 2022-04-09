using System;
using System.Windows;
using WPFVersion3D.Attributes;
using WPFVersion3D.ViewModel;

namespace WPFVersion3D
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
