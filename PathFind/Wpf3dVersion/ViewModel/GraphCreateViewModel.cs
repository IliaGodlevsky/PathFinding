using GraphLib.Coordinates;
using GraphLib.Coordinates.Interface;
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
            ConfirmCreateGraphCommand = new RelayCommand(
                ExecuteConfirmCreateGraphCommand, obj => true);
            CancelCreateGraphCommand = new RelayCommand(obj =>
            (model as MainWindowViewModel)?.Window.Close(), obj => true);
        }

        private void ExecuteConfirmCreateGraphCommand(object param)
        {
            CreateGraph(() => new Wpf3dVertex());

            var mainWindow = Application.Current.MainWindow as MainWindow;
            var field = model.GraphField as Wpf3dGraphField;

            field.CenterGraph();

            mainWindow.GraphField.Children.Clear();
            mainWindow.GraphField.Children.Add(field);

            (model as MainWindowViewModel).Window.Close();
        }

        public override void CreateGraph(Func<IVertex> vertexFactory)
        {
            var graphfactory = new GraphFactory<Graph3D>(ObstaclePercent, Width, Length, Height);

            ICoordinate coordinate3DFactory(IEnumerable<int> coordinates)
            {
                return new Coordinate3D(coordinates.ToArray());
            }

            graph = graphfactory.CreateGraph(vertexFactory, coordinate3DFactory);

            model.ConnectNewGraph(graph);
        }
    }
}
