using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.WPF._2D.Infrastructure;
using Pathfinding.App.WPF._2D.Messages.DataMessages;
using Pathfinding.App.WPF._2D.Model;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.GraphLib.Smoothing;
using Pathfinding.GraphLib.Smoothing.Interface;
using System.Collections.Generic;
using System.Windows.Input;
using WPFVersion.DependencyInjection;

namespace Pathfinding.App.WPF._2D.ViewModel.ButtonViewModels
{
    internal class SmoothGraphViewModel
    {
        private readonly IMessenger messenger;
        private readonly IMeanCost meanCost;
        private readonly IVertexCostFactory costFactory;

        private IGraph<Vertex> Graph { get; set; } = Graph2D<Vertex>.Empty;

        public ISmoothLevel SelectSmoothLevel { get; set; }

        public IEnumerable<ISmoothLevel> Levels => WpfSmoothLevels.Levels;

        public ICommand SmoothCommand { get; }

        public SmoothGraphViewModel()
        {
            messenger = DI.Container.Resolve<IMessenger>();
            meanCost = DI.Container.Resolve<IMeanCost>();
            costFactory = DI.Container.Resolve<IVertexCostFactory>();
            SmoothCommand = new RelayCommand(ExecuteSmoothCommand);
            messenger.Register<GraphCreatedMessage>(this, OnGraphCreated);
        }

        private void ExecuteSmoothCommand(object param)
        {
            Graph.Smooth(costFactory, meanCost, SelectSmoothLevel.Level);
        }

        private void OnGraphCreated(GraphCreatedMessage message)
        {
            Graph = message.Graph;
        }
    }
}
