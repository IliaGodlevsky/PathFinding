using Pathfinding.App.WPF._3D.Attributes;
using Pathfinding.App.WPF._3D.ViewModel;
using System;
using System.Windows;

namespace Pathfinding.App.WPF._3D.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
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
