using Autofac;
using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.WPF._2D.Infrastructure;
using Pathfinding.App.WPF._2D.Messages.DataMessages;
using System.Windows.Input;
using WPFVersion.DependencyInjection;

namespace Pathfinding.App.WPF._2D.ViewModel.BaseViewModels
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
