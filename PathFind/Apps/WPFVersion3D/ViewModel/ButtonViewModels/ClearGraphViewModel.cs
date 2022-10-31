using Autofac;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Base.EndPoints;
using GraphLib.Extensions;
using GraphLib.Realizations.Graphs;
using NullObject.Extensions;
using System.Windows.Input;
using WPFVersion3D.DependencyInjection;
using WPFVersion3D.Infrastructure.Commands;
using WPFVersion3D.Messages.ActionMessages;
using WPFVersion3D.Messages.PassValueMessages;
using WPFVersion3D.Model;

namespace WPFVersion3D.ViewModel.ButtonViewModels
{
    internal class ClearGraphViewModel
    {
        private readonly IMessenger messenger;
        private readonly BaseEndPoints<Vertex3D> endPoints;

        private Graph3D<Vertex3D> Graph { get; set; }

        private bool IsAllAlgorithmFinishedPathfinding { get; set; } = true;

        public ICommand ClearGraphCommand { get; }

        public ClearGraphViewModel()
        {
            messenger = DI.Container.Resolve<IMessenger>();
            endPoints = DI.Container.Resolve<BaseEndPoints<Vertex3D>>();
            messenger.Register<IsAllAlgorithmsFinishedMessage>(this, OnAllAlgorithmFinishedPathfinding);
            messenger.Register<GraphCreatedMessage>(this, OnGraphCreated);
            ClearGraphCommand = new RelayCommand(ExecuteClearGraphCommand, CanExecuteClearGraphCommand);
        }

        private void ExecuteClearGraphCommand(object param)
        {
            Graph.Refresh();
            endPoints.Reset();
            messenger.Send(new ClearStatisticsMessage());
        }

        private bool CanExecuteClearGraphCommand(object param)
        {
            return IsAllAlgorithmFinishedPathfinding && !Graph.IsNull();
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
