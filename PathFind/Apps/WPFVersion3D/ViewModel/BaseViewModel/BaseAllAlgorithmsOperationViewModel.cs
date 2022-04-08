using Autofac;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Input;
using WPFVersion3D.DependencyInjection;
using WPFVersion3D.Infrastructure.Commands;
using WPFVersion3D.Messages.PassValueMessages;

namespace WPFVersion3D.ViewModel.BaseViewModel
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
