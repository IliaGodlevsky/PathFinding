using Autofac;
using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.WPF._2D.Infrastructure;
using Pathfinding.App.WPF._2D.Messages.DataMessages;
using Pathfinding.App.WPF._2D.Model;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations;
using Pathfinding.GraphLib.Smoothing;
using Pathfinding.GraphLib.Smoothing.Interface;
using Shared.Primitives.ValueRange;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using WPFVersion.DependencyInjection;

namespace Pathfinding.App.WPF._2D.ViewModel.ButtonViewModels
{
    internal class SmoothGraphViewModel
    {
        private readonly IMessenger messenger;
        private readonly IMeanCost meanCost;

        private IGraph<Vertex> Graph { get; set; } = Graph<Vertex>.Empty;

        public ISmoothLevel SelectSmoothLevel { get; set; }

        public IEnumerable<ISmoothLevel> Levels => WpfSmoothLevels.Levels;

        public ICommand SmoothCommand { get; }

        public SmoothGraphViewModel()
        {
            messenger = DI.Container.Resolve<IMessenger>();
            meanCost = DI.Container.Resolve<IMeanCost>();
            SmoothCommand = new RelayCommand(ExecuteSmoothCommand);
            messenger.Register<GraphCreatedMessage>(this, OnGraphCreated);
        }

        private void ExecuteSmoothCommand(object param)
        {
            Graph.Smooth(meanCost, SelectSmoothLevel.Level);
            int maxValue = Graph.Max(vertex => vertex.Cost.CurrentCost);
            int minValue = Graph.Min(vertex => vertex.Cost.CurrentCost);
            var range = new InclusiveValueRange<int>(maxValue, minValue);
            messenger.Send(new CostRangeChangedMessage(range), MessageTokens.CostColors);
        }

        private void OnGraphCreated(GraphCreatedMessage message)
        {
            Graph = message.Graph;
        }
    }
}
