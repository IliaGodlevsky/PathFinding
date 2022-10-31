using Autofac;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Base.EndPoints;
using GraphLib.Interfaces;
using GraphLib.Realizations.Graphs;
using System.Windows.Input;
using WPFVersion.DependencyInjection;
using WPFVersion.Infrastructure;
using WPFVersion.Messages;
using WPFVersion.Messages.DataMessages;
using WPFVersion.Model;
using WPFVersion.View.Windows;

namespace WPFVersion.ViewModel.ButtonViewModels
{
    internal class CreateGraphViewModel
    {
        private readonly IMessenger messenger;

        public ICommand CreateGraphCommand { get; }

        private Graph2D<Vertex> Graph { get; set; } = Graph2D<Vertex>.Empty;

        private bool IsAllAlgorithmsFinishedPathfinding { get; set; } = true;

        public CreateGraphViewModel()
        {
            messenger = DI.Container.Resolve<IMessenger>();
            messenger.Register<IsAllAlgorithmsFinishedMessage>(this, OnAllAlgorithmFinished);
            messenger.Register<GraphCreatedMessage>(this, OnGraphCreated);
            CreateGraphCommand = new RelayCommand(ExecuteCreateGraphCommand, CanExecuteCreateGraphCommand);
        }

        private void ExecuteCreateGraphCommand(object param)
        {
            var window = DI.Container.Resolve<GraphCreatesWindow>();
            window.Show();
        }

        private bool CanExecuteCreateGraphCommand(object param)
        {
            return IsAllAlgorithmsFinishedPathfinding;
        }

        private void OnGraphCreated(GraphCreatedMessage message)
        {
            var events = DI.Container.Resolve<IGraphEvents<Vertex>>();
            var endPoints = DI.Container.Resolve<BaseEndPoints<Vertex>>();
            events.Unsubscribe(Graph);
            endPoints.Reset();
            Graph = message.Graph;
            events.Subscribe(Graph);
            messenger.Send(new ClearStatisticsMessage());
        }

        private void OnAllAlgorithmFinished(IsAllAlgorithmsFinishedMessage message)
        {
            IsAllAlgorithmsFinishedPathfinding = message.IsAllAlgorithmsFinished;
        }
    }
}
