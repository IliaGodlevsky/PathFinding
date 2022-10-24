using Autofac;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Base.EndPoints;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using NullObject.Extensions;
using System.Windows.Input;
using WPFVersion3D.DependencyInjection;
using WPFVersion3D.Infrastructure.Commands;
using WPFVersion3D.Messages.PassValueMessages;

namespace WPFVersion3D.ViewModel.ButtonViewModels
{
    internal class ClearColorsViewModel
    {
        private readonly IMessenger messenger;
        private readonly BasePathfindingRange pathfindingRange;

        private IGraph Graph { get; set; }

        private bool IsAllAlgorithmFinishedPathfinding { get; set; } = true;

        public ICommand ClearColorsCommand { get; }

        public ClearColorsViewModel()
        {
            messenger = DI.Container.Resolve<IMessenger>();
            pathfindingRange = DI.Container.Resolve<BasePathfindingRange>();
            messenger.Register<GraphCreatedMessage>(this, OnGraphCreated);
            messenger.Register<IsAllAlgorithmsFinishedMessage>(this, OnAllAlgorithmFinishedPathfinding);
            ClearColorsCommand = new RelayCommand(ExecuteClearColorsCommand, CanExecuteClearColorsCommand);
        }

        private void ExecuteClearColorsCommand(object param)
        {
            Graph.Refresh();
            pathfindingRange.RestoreCurrentColors();
        }

        private bool CanExecuteClearColorsCommand(object param)
        {
            return !Graph.IsNull() && pathfindingRange.HasSourceAndTargetSet() && IsAllAlgorithmFinishedPathfinding;
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
