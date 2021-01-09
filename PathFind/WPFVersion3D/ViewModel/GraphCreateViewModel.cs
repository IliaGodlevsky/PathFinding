using Common.Interfaces;
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
using WPFVersion3D.Infrastructure;
using WPFVersion3D.Model;

namespace WPFVersion3D.ViewModel
{
    internal class GraphCreatingViewModel : GraphCreatingModel, IViewModel
    {
        public event EventHandler OnWindowClosed;

        public int Height { get; set; }

        public RelayCommand ConfirmCreateGraphCommand { get; }

        public RelayCommand CancelCreateGraphCommand { get; }

        public GraphCreatingViewModel(IMainModel model) : base(model)
        {
            ConfirmCreateGraphCommand = new RelayCommand(ExecuteConfirmCreateGraphCommand);
            CancelCreateGraphCommand = new RelayCommand(obj => CloseWindow());
        }

        public override void CreateGraph(Func<IVertex> vertexFactory)
        {
            var graphfactory = new GraphFactory<Graph3D>(ObstaclePercent, Width, Length, Height);

            var graph = graphfactory.CreateGraph(vertexFactory, CreateCoordinate3D);

            model.ConnectNewGraph(graph);
        }

        private Coordinate3D CreateCoordinate3D(IEnumerable<int> coordinates)
        {
            return new Coordinate3D(coordinates.ToArray());
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
            OnWindowClosed?.Invoke(this, new EventArgs());
        }
    }
}
