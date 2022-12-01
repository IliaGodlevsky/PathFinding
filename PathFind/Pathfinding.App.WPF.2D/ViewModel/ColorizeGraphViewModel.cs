using Autofac;
using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.WPF._2D.Infrastructure;
using Pathfinding.App.WPF._2D.Messages.DataMessages;
using Pathfinding.App.WPF._2D.Model;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Shared.Primitives.ValueRange;
using System.Linq;
using System.Windows.Input;
using WPFVersion.DependencyInjection;

namespace Pathfinding.App.WPF._2D.ViewModel
{
    internal class ColorizeGraphViewModel
    {
        private readonly IMessenger messenger;       
        private CostColors colors;

        private bool IsAllAlgorithmsFinished { get; set; }

        private Graph2D<Vertex> Graph { get; set; }

        public ICommand ColorizeCommand { get; }

        public ICommand ResetColorizingCommand { get; }

        public ColorizeGraphViewModel()
        {
            this.messenger = DI.Container.Resolve<IMessenger>();
            this.messenger.Register<GraphCreatedMessage>(this, OnGraphCreated);
            this.messenger.Register<IsAllAlgorithmsFinishedMessage>(this, OnAllAlgorithmsFinished);
            this.messenger.Register<CostRangeChangedMessage>(this, MessageTokens.CostColors, OnCostRangeChanged);
            ColorizeCommand = new RelayCommand(ExecuteColorizeCommand, CanExecuteColorizeCommand);
            ResetColorizingCommand = new RelayCommand(ExecuteRestoreColorsCommand, CanExecuteColorizeCommand);
        }

        private void OnGraphCreated(GraphCreatedMessage message)
        {
            Graph = message.Graph;
            var min = Graph.Min(vertex => vertex.Cost.CostRange.LowerValueOfRange);
            var max = Graph.Max(vertex => vertex.Cost.CostRange.UpperValueOfRange);
            var range = new InclusiveValueRange<int>(max, min);
            colors = new CostColors(Graph) { CostRange = range };
        }

        private void OnCostRangeChanged(CostRangeChangedMessage message)
        {
            colors = new CostColors(Graph) { CostRange = message.Range };
        }

        private void OnAllAlgorithmsFinished(IsAllAlgorithmsFinishedMessage message)
        {
            IsAllAlgorithmsFinished = message.IsAllAlgorithmsFinished;
        }

        private void ExecuteColorizeCommand(object param)
        {
            colors.ColorizeAccordingToCost();
        }

        private void ExecuteRestoreColorsCommand(object param)
        {
            colors.ReturnPreviousColors();
        }

        private bool CanExecuteColorizeCommand(object param)
        {
            return colors != null && IsAllAlgorithmsFinished;
        }
    }
}
