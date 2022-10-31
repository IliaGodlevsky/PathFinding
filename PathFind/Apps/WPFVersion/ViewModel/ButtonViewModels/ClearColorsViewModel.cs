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
using WPFVersion.Model;

namespace WPFVersion.ViewModel.ButtonViewModels
{
    internal class ClearColorsViewModel
    {
        private readonly IMessenger messenger;
        private readonly BaseEndPoints<Vertex> endPoints;

        private IGraph<Vertex> Graph { get; set; }

        private bool IsAllAlgorithmFinishedPathfinding { get; set; } = true;

        public ICommand ClearColorsCommand { get; }

        public ClearColorsViewModel()
        {
            messenger = DI.Container.Resolve<IMessenger>();
            endPoints = DI.Container.Resolve<BaseEndPoints<Vertex>>();
            messenger.Register<GraphCreatedMessage>(this, OnGraphCreated);
            messenger.Register<IsAllAlgorithmsFinishedMessage>(this, OnAllAlgorithmFinishedPathfinding);
            ClearColorsCommand = new RelayCommand(ExecuteClearColorsCommand, CanExecuteClearColorsCommand);
        }

        private void ExecuteClearColorsCommand(object param)
        {
            Graph.Refresh();
            endPoints.RestoreCurrentColors();
        }

        private bool CanExecuteClearColorsCommand(object param)
        {
            return !Graph.IsNull() && endPoints.HasSourceAndTargetSet() && IsAllAlgorithmFinishedPathfinding;
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
