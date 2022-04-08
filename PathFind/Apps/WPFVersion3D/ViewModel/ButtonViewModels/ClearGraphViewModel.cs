using Autofac;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Base.EndPoints;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using NullObject.Extensions;
using System.Windows.Input;
using WPFVersion3D.DependencyInjection;
using WPFVersion3D.Infrastructure.Commands;
using WPFVersion3D.Messages.ActionMessages;
using WPFVersion3D.Messages.PassValueMessages;

namespace WPFVersion3D.ViewModel.ButtonViewModels
{
    internal class ClearGraphViewModel
    {
        private readonly IMessenger messenger;
        private readonly BaseEndPoints endPoints;

        private IGraph Graph { get; set; }

        private bool IsAllAlgorithmFinishedPathfinding { get; set; } = true;

        public ICommand ClearGraphCommand { get; }

        public ClearGraphViewModel()
        {
            messenger = DI.Container.Resolve<IMessenger>();
            endPoints = DI.Container.Resolve<BaseEndPoints>();
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
