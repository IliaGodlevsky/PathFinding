using Autofac;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Interfaces;
using NullObject.Extensions;
using System.Windows.Input;
using WPFVersion3D.DependencyInjection;
using WPFVersion3D.Enums;
using WPFVersion3D.Infrastructure;
using WPFVersion3D.Messages;
using WPFVersion3D.View;

namespace WPFVersion3D.ViewModel.ButtonViewModels
{
    internal class ChangeOpacityViewModel
    {
        private readonly IMessenger messenger;

        private IGraph Graph { get; set; }

        public ICommand ChangeVerticesOpacityCommand { get; }

        public ChangeOpacityViewModel()
        {
            messenger = DI.Container.Resolve<IMessenger>();
            messenger.Register<GraphCreatedMessage>(this, MessageTokens.ChangeOpacityModel, OnGraphCreated);
            ChangeVerticesOpacityCommand = new RelayCommand(ExecuteChangeOpacityCommand, CanExecuteChangeOpacityCommand);
        }

        private void ExecuteChangeOpacityCommand(object param)
        {
            DI.Container.Resolve<OpacityChangeWindow>().Show();
        }

        private bool CanExecuteChangeOpacityCommand(object param)
        {
            return !Graph.IsNull();
        }

        private void OnGraphCreated(GraphCreatedMessage message)
        {
            Graph = message.Value;
        }
    }
}
