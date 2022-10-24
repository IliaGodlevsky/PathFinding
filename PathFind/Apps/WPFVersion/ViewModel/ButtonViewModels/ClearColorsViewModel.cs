using Autofac;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Base.EndPoints;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using NullObject.Extensions;
using System.Windows.Input;
using WPFVersion.DependencyInjection;
using WPFVersion.Infrastructure;
using WPFVersion.Messages.DataMessages;

namespace WPFVersion.ViewModel.ButtonViewModels
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
            IsAllAlgorithmFinishedPathfinding = message.IsAllAlgorithmsFinished;
        }

        private void OnGraphCreated(GraphCreatedMessage message)
        {
            Graph = message.Graph;
        }
    }
}
