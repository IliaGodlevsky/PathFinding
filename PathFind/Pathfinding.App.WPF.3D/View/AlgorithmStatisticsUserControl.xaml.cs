using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
