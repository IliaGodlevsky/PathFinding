using Pathfinding.App.WPF._2D.Attributes;
using Pathfinding.App.WPF._2D.Infrastructure.EventArguments;
using Pathfinding.App.WPF._2D.ViewModel;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using System;
using System.Windows;

namespace Pathfinding.App.WPF._2D.View
{
    [AppWindow]
    public partial class MainWindow : Window
    {
        private const int WidthOffset = 2;
        private const int LengthOffset = 6;

        private const int DistanceBetweenVertices = Constants.DistanceBetweenVertices + Constants.VertexSize;

        public MainWindow(MainWindowViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            viewModel.GraphCreated += OnGraphCreated;
        }

        protected override void OnClosed(EventArgs e)
        {
            (DataContext as IDisposable)?.Dispose();
            var viewModel = DataContext as MainWindowViewModel;
            if (viewModel != null)
            {
                viewModel.GraphCreated -= OnGraphCreated;
            }
            base.OnClosed(e);
        }

        private void OnGraphCreated(object sender, GraphCreatedEventArgs e)
        {
            Width = (e.Graph.GetWidth() + WidthOffset) * DistanceBetweenVertices;
            Height = (e.Graph.GetLength() + LengthOffset) * DistanceBetweenVertices;
        }
    }
}
