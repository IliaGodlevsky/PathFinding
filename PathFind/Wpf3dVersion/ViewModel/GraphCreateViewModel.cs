using GraphLib.Graphs.Factories;
using GraphLib.Vertex.Interface;
using GraphLib.ViewModel;
using GraphViewModel.Interfaces;
using System;
using System.Windows;
using System.Windows.Media.Media3D;
using Wpf3dVersion.Infrastructure;
using Wpf3dVersion.Model;

namespace Wpf3dVersion.ViewModel
{
    internal class GraphCreatingViewModel : GraphCreatingModel
    {
        public int Length { get; set; }
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
            (model.GraphField as Wpf3dGraphField).CenterGraph(model.Graph);
            (Application.Current.MainWindow as MainWindow).GraphField.Children.Clear();
            (Application.Current.MainWindow as MainWindow).GraphField.Children.Add(model.GraphField as ModelVisual3D);
            (model as MainWindowViewModel).Window.Close();
        }

        public override void CreateGraph(Func<IVertex> vertexFactory)
        {
            var graphfactory = new Graph3dFactory(Width, Height, Length, ObstaclePercent);
            graph = graphfactory.CreateGraph(vertexFactory);
            model.ConnectNewGraph(graph);
        }
    }
}
