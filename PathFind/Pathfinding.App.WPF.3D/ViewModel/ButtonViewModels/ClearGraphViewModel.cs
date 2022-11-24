using Autofac;
using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.WPF._3D.DependencyInjection;
using Pathfinding.App.WPF._3D.Infrastructure.Commands;
using Pathfinding.App.WPF._3D.Messages.ActionMessages;
using Pathfinding.App.WPF._3D.Messages.PassValueMessages;
using Pathfinding.App.WPF._3D.Model;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.Visualization.Core.Abstractions;
using Pathfinding.Visualization.Extensions;
using System.Windows.Input;

namespace Pathfinding.App.WPF._3D.ViewModel.ButtonViewModels
{
    internal class ClearGraphViewModel
    {
        private readonly IMessenger messenger;
        private readonly VisualPathfindingRange<Vertex3D> adapter;

        private Graph3D<Vertex3D> Graph { get; set; } = Graph3D<Vertex3D>.Empty;

        private bool IsAllAlgorithmFinishedPathfinding { get; set; } = true;

        public ICommand ClearGraphCommand { get; }

        public ClearGraphViewModel()
        {
            messenger = DI.Container.Resolve<IMessenger>();
            adapter = DI.Container.Resolve<VisualPathfindingRange<Vertex3D>>();
            messenger.Register<IsAllAlgorithmsFinishedMessage>(this, OnAllAlgorithmFinishedPathfinding);
            messenger.Register<GraphCreatedMessage>(this, OnGraphCreated);
            ClearGraphCommand = new RelayCommand(ExecuteClearGraphCommand, CanExecuteClearGraphCommand);
        }

        private void ExecuteClearGraphCommand(object param)
        {
            Graph.RestoreVerticesVisualState();
            adapter.Undo();
            messenger.Send(new ClearStatisticsMessage());
        }

        private bool CanExecuteClearGraphCommand(object param)
        {
            return IsAllAlgorithmFinishedPathfinding && Graph != Graph3D<Vertex3D>.Empty;
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
