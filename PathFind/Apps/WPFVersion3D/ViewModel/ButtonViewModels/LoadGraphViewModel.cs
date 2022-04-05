using Autofac;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Serialization;
using GraphLib.Serialization.Extensions;
using Logging.Interface;
using System;
using System.Windows.Input;
using WPFVersion3D.DependencyInjection;
using WPFVersion3D.Enums;
using WPFVersion3D.Extensions;
using WPFVersion3D.Infrastructure;
using WPFVersion3D.Messages;

namespace WPFVersion3D.ViewModel.ButtonViewModels
{
    internal class LoadGraphViewModel
    {
        private readonly IMessenger messenger;
        private readonly ILog log;
        private readonly GraphSerializationModule module;

        private bool IsAllAlgorithmsFinishedPathfinding { get; set; } = true;

        public ICommand LoadGraphCommand { get; }

        public LoadGraphViewModel()
        {
            messenger = DI.Container.Resolve<IMessenger>();
            messenger.Register<IsAllAlgorithmsFinishedMessage>(this, MessageTokens.LoadGraphModel, OnAllAlgorithmFinishedPathfinding);
            module = DI.Container.Resolve<GraphSerializationModule>();
            log = DI.Container.Resolve<ILog>();
            LoadGraphCommand = new RelayCommand(ExecuteLoadGraphCommand);
        }

        private async void ExecuteLoadGraphCommand(object param)
        {
            try
            {
                var graph = await module.LoadGraphAsync();
                messenger.Forward(new GraphCreatedMessage(graph), MessageTokens.Everyone);
            }
            catch(Exception ex)
            {
                log.Error(ex);
            }
        }

        private bool CanExecuteLoadGraphCommand(object param)
        {
            return IsAllAlgorithmsFinishedPathfinding;
        }

        private void OnAllAlgorithmFinishedPathfinding(IsAllAlgorithmsFinishedMessage message)
        {
            IsAllAlgorithmsFinishedPathfinding = message.IsAllAlgorithmsFinished;
        }
    }
}
