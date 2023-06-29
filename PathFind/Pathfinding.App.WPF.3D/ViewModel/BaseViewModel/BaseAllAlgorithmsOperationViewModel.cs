using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.WPF._3D.DependencyInjection;
using Pathfinding.App.WPF._3D.Infrastructure.Commands;
using Pathfinding.App.WPF._3D.Messages.PassValueMessages;
using System.Windows.Input;

namespace Pathfinding.App.WPF._3D.ViewModel.BaseViewModel
{
    internal abstract class BaseAllAlgorithmsOperationViewModel
    {
        protected readonly IMessenger messenger;

        protected bool IsAllAlgorithmsFinishedPathfinding { get; set; }

        public ICommand Command { get; }

        protected BaseAllAlgorithmsOperationViewModel()
        {
            messenger = DI.Container.Resolve<IMessenger>();
            messenger.Register<IsAllAlgorithmsFinishedMessage>(this, OnAllAlgorithmsFinished);
            Command = new RelayCommand(ExecuteCommand, CanExecuteCommand);
        }

        protected abstract void ExecuteCommand(object param);

        protected virtual bool CanExecuteCommand(object param)
        {
            return !IsAllAlgorithmsFinishedPathfinding;
        }

        protected virtual void OnAllAlgorithmsFinished(IsAllAlgorithmsFinishedMessage message)
        {
            IsAllAlgorithmsFinishedPathfinding = message.Value;
        }
    }
}
