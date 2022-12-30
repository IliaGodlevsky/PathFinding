using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Pathfinding.App.WPF._3D.View
{
    /// <summary>
    /// Interaction logic for AlgorithmStatisticsUserControl.xaml
    /// </summary>
    public partial class AlgorithmStatisticsUserControl : UserControl
    {
        public AlgorithmStatisticsUserControl()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, EventArgs e)
        {
            Loaded -= OnLoaded;
            var window = Window.GetWindow(this);
            void OnWindowClosing(object s, CancelEventArgs args)
            {
                (DataContext as IDisposable)?.Dispose();
                window.Closing -= OnWindowClosing;
            }
            window.Closing += OnWindowClosing;
        }
    }
}
