using Autofac;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Base.EndPoints;
using GraphLib.Interfaces;
using GraphLib.Realizations.Graphs;
using System.Windows.Input;
using WPFVersion3D.DependencyInjection;
using WPFVersion3D.Infrastructure.Commands;
using WPFVersion3D.Messages.ActionMessages;
using WPFVersion3D.Messages.PassValueMessages;
using WPFVersion3D.Model;
using WPFVersion3D.View;

namespace WPFVersion3D.ViewModel.ButtonViewModels
{
    internal sealed class CreateGraphViewModel
    {
        private readonly IMessenger messenger;

        public ICommand CreateGraphCommand { get; }

        private Graph3D<Vertex3D> Graph { get; set; } = Graph3D<Vertex3D>.Empty;

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
            DI.Container.Resolve<GraphCreateWindow>().Show();
        }

        private bool CanExecuteCreateGraphCommand(object param)
        {
            return IsAllAlgorithmsFinishedPathfinding;
        }

        private void OnGraphCreated(GraphCreatedMessage message)
        {
            var endPoints = DI.Container.Resolve<BaseEndPoints<Vertex3D>>();
            var events = DI.Container.Resolve<IGraphEvents<Vertex3D>>();
            events.Unsubscribe(Graph);
            endPoints.Reset();
            Graph = message.Value;
            events.Subscribe(Graph);
            messenger.Send(new ClearStatisticsMessage());
        }

        private void OnAllAlgorithmFinished(IsAllAlgorithmsFinishedMessage message)
        {
            IsAllAlgorithmsFinishedPathfinding = message.Value;
        }
    }
}
