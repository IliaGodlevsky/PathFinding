using Autofac;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Input;
using WPFVersion3D.DependencyInjection;
using WPFVersion3D.Enums;
using WPFVersion3D.Extensions;
using WPFVersion3D.Messages;

namespace WPFVersion3D.ViewModel.ButtonViewModels
{
    internal class InterruptAllAlgorithmsViewModel
    {
        private readonly IMessenger messenger;

        private bool IsAllALgorithmsFinishedPathfinding { get; set; } = true;

        public ICommand InterruptAllAlgorithmsCommand { get; }

        public InterruptAllAlgorithmsViewModel()
        {
            messenger = DI.Container.Resolve<IMessenger>();
            messenger.Register<IsAllAlgorithmsFinishedMessage>(this, MessageTokens.InterruptAllAlgorithmsModel, OnAllAlgorithmsFinishedPathfinding);
        }

        private void ExecuteInterruptAllAlgorithmCommand(object param)
        {
            var message = new InterruptAllAlgorithmsMessage();
            messenger.Forward(message, MessageTokens.AlgorithmStatisticsModel);
        }

        private bool CanExecuteInterruptAllAlgorithmCommand(object param)
        {
            return !IsAllALgorithmsFinishedPathfinding;
        }

        private void OnAllAlgorithmsFinishedPathfinding(IsAllAlgorithmsFinishedMessage message)
        {
            IsAllALgorithmsFinishedPathfinding = message.IsAllAlgorithmsFinished;
        }
    }
}
