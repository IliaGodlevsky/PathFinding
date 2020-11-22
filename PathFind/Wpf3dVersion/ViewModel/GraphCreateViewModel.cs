using GraphLib.Coordinates;
using GraphLib.Coordinates.Abstractions;
using GraphLib.Graphs;
using GraphLib.Graphs.Factories;
using GraphLib.Vertex.Interface;
using GraphLib.ViewModel;
using GraphViewModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Wpf3dVersion.Infrastructure;
using Wpf3dVersion.Model;

namespace Wpf3dVersion.ViewModel
{
    internal class GraphCreatingViewModel : GraphCreatingModel
    {
        public int Height { get; set; }

        public RelayCommand ConfirmCreateGraphCommand { get; }

        public RelayCommand CancelCreateGraphCommand { get; }

        public GraphCreatingViewModel(IMainModel model) : base(model)
        {
            ConfirmCreateGraphCommand = new RelayCommand(ExecuteConfirmCreateGraphCommand, obj => true);
            CancelCreateGraphCommand = new RelayCommand(obj => CloseWindow(), obj => true);
        }

        public override void CreateGraph(Func<IVertex> vertexFactory)
        {
            var graphfactory = new GraphFactory<Graph3D>(ObstaclePercent, Width, Length, Height);

            ICoordinate CoordinateFactory(IEnumerable<int> coordinates)
            {
                return new Coordinate3D(coordinates.ToArray());
            }

            var graph = graphfactory.CreateGraph(vertexFactory, CoordinateFactory);

            model.ConnectNewGraph(graph);
        }

        private void ExecuteConfirmCreateGraphCommand(object param)
        {
            CreateGraph(() => new WpfVertex3D());

            var mainWindow = Application.Current.MainWindow as MainWindow;
            var field = model.GraphField as WpfGraphField3D;

            field.CenterGraph();

            mainWindow.GraphField.Children.Clear();
            mainWindow.GraphField.Children.Add(field);

            CloseWindow();
        }

        private void CloseWindow()
        {
            (model as MainWindowViewModel).Window.Close();
        }
    }
}
