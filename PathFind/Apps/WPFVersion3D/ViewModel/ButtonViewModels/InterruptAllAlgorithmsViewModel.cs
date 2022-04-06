using Autofac;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Input;
using WPFVersion3D.DependencyInjection;
using WPFVersion3D.Extensions;
using WPFVersion3D.Infrastructure;
using WPFVersion3D.Messages;

namespace WPFVersion3D.ViewModel.ButtonViewModels
{
    internal class InterruptAllAlgorithmsViewModel
    {
        private readonly IMessenger messenger;

        private bool IsAllAlgorithmsFinishedPathfinding { get; set; }

        public ICommand InterruptAllAlgorithmsCommand { get; }

        public InterruptAllAlgorithmsViewModel()
        {
            messenger = DI.Container.Resolve<IMessenger>();
            messenger.Register<IsAllAlgorithmsFinishedMessage>(this, Tokens.InterruptAllAlgorithmsModel, OnAllAlgorithmsFinishedPathfinding);
            InterruptAllAlgorithmsCommand = new RelayCommand(ExecuteInterruptAllAlgorithmCommand, CanExecuteInterruptAllAlgorithmCommand);
        }

        private void ExecuteInterruptAllAlgorithmCommand(object param)
        {
            var message = new InterruptAllAlgorithmsMessage();
            messenger.Forward(message, Tokens.AlgorithmStatisticsModel);
        }

        private bool CanExecuteInterruptAllAlgorithmCommand(object param)
        {
            return !IsAllAlgorithmsFinishedPathfinding;
        }

        private void OnAllAlgorithmsFinishedPathfinding(IsAllAlgorithmsFinishedMessage message)
        {
            IsAllAlgorithmsFinishedPathfinding = message.Value;
        }
    }
}
