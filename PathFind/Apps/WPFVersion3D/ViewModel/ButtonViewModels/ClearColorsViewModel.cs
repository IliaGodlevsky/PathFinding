using Autofac;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Base.EndPoints;
using GraphLib.Extensions;
using GraphLib.Realizations.Graphs;
using NullObject.Extensions;
using System.Windows.Input;
using WPFVersion3D.DependencyInjection;
using WPFVersion3D.Infrastructure.Commands;
using WPFVersion3D.Messages.PassValueMessages;
using WPFVersion3D.Model;

namespace WPFVersion3D.ViewModel.ButtonViewModels
{
    internal class ClearColorsViewModel
    {
        private readonly IMessenger messenger;
        private readonly BaseEndPoints<Vertex3D> endPoints;

        private Graph3D<Vertex3D> Graph { get; set; } = Graph3D<Vertex3D>.Empty;

        private bool IsAllAlgorithmFinishedPathfinding { get; set; } = true;

        public ICommand ClearColorsCommand { get; }

        public ClearColorsViewModel()
        {
            messenger = DI.Container.Resolve<IMessenger>();
            endPoints = DI.Container.Resolve<BaseEndPoints<Vertex3D>>();
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
            IsAllAlgorithmFinishedPathfinding = message.Value;
        }

        private void OnGraphCreated(GraphCreatedMessage message)
        {
            Graph = message.Value;
        }
    }
}
