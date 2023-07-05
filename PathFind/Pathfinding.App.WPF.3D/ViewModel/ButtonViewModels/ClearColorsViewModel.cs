using Autofac;
using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.WPF._3D.DependencyInjection;
using Pathfinding.App.WPF._3D.Infrastructure.Commands;
using Pathfinding.App.WPF._3D.Messages.PassValueMessages;
using Pathfinding.App.WPF._3D.Model;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Modules.Interface;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.Visualization.Extensions;
using System.Windows.Input;

namespace Pathfinding.App.WPF._3D.ViewModel.ButtonViewModels
{
    internal class ClearColorsViewModel
    {
        private readonly IMessenger messenger;
        private readonly IPathfindingRangeBuilder<Vertex3D> rangeBuilder;

        private Graph3D<Vertex3D> Graph { get; set; } = Graph3D<Vertex3D>.Empty;

        private bool IsAllAlgorithmFinishedPathfinding { get; set; } = true;

        public ICommand ClearColorsCommand { get; }

        public ClearColorsViewModel()
        {
            messenger = DI.Container.Resolve<IMessenger>();
            rangeBuilder = DI.Container.Resolve<IPathfindingRangeBuilder<Vertex3D>>();
            messenger.Register<GraphCreatedMessage>(this, OnGraphCreated);
            messenger.Register<IsAllAlgorithmsFinishedMessage>(this, OnAllAlgorithmFinishedPathfinding);
            ClearColorsCommand = new RelayCommand(ExecuteClearColorsCommand, CanExecuteClearColorsCommand);
        }

        private void ExecuteClearColorsCommand(object param)
        {
            Graph.RestoreVerticesVisualState();
            rangeBuilder.Range.RestoreVerticesVisualState();
        }

        private bool CanExecuteClearColorsCommand(object param)
        {
            return Graph != Graph3D<Vertex3D>.Empty
                && rangeBuilder.Range.HasSourceAndTargetSet()
                && IsAllAlgorithmFinishedPathfinding;
        }

        private void OnAllAlgorithmFinishedPathfinding(IsAllAlgorithmsFinishedMessage message)
        {
            IsAllAlgorithmFinishedPathfinding = message.Value;
        }

        private void OnGraphCreated(GraphCreatedMessage message)
        {
            Graph = message.Value;
        }
    }
}
