using GraphViewModel.Interfaces;
using System;
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
        private Wpf3dGraphField GetGraphField()
        {
            var model = DataContext as MainWindowViewModel;
            if (model == null)
                return null;
            return (model.GraphField as Wpf3dGraphField);
        }

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

        public void IsolateXAxis(object sender, RoutedEventArgs e)
        {
            var graphField = GetGraphField();
            graphField?.IsolateXAxis();
        }

        public void ReturnXAxis(object sender, RoutedEventArgs e)
        {
            var graphField = GetGraphField();
            graphField?.ReturnXAxis();
        }

        public void IsolateYAxis(object sender, RoutedEventArgs e)
        {
            var graphField = GetGraphField();
            graphField?.IsolateYAxis();
        }

        public void ReturnYAxis(object sender, RoutedEventArgs e)
        {
            var graphField = GetGraphField();
            graphField?.ReturnYAxis();
        }

        public void IsolateZAxis(object sender, RoutedEventArgs e)
        {
            var graphField = GetGraphField();
            graphField?.IsolateZAxis();
        }

        public void ReturnZAxis(object sender, RoutedEventArgs e)
        {
            var graphField = GetGraphField();
            graphField?.ReturnZAxis();
        }
    }
}
