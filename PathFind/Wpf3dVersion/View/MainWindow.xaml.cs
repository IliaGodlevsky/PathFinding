using System.Windows;
using Wpf3dVersion.Model;
using Wpf3dVersion.ViewModel;

namespace Wpf3dVersion
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var model = DataContext as MainWindowViewModel;
            if (model == null)
                return;
            var graphField = (model.GraphField as Wpf3dGraphField);
            graphField.DistanceBetween = e.NewValue;
            graphField.SetDistanceBetweenVertices(model.Graph);
            graphField.CenterGraph(model.Graph);
        }
    }
}
