using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace WPFVersion3D.View
{
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
