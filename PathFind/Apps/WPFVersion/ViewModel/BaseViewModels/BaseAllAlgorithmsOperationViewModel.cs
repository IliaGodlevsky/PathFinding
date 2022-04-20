using Autofac;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Input;
using WPFVersion.DependencyInjection;
using WPFVersion.Infrastructure;
using WPFVersion.Messages.DataMessages;

namespace WPFVersion.ViewModel.BaseViewModels
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
            IsAllAlgorithmsFinishedPathfinding = message.IsAllAlgorithmsFinished;
        }
    }
}
