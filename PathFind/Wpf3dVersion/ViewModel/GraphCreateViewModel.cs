using GraphLib.Graphs.Factories;
using GraphLib.Vertex.Interface;
using GraphLib.ViewModel;
using GraphViewModel.Interfaces;
using System;
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
