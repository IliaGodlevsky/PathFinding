using Autofac;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Realizations.Graphs;
using GraphLib.Serialization.Extensions;
using GraphLib.Serialization.Interfaces;
using Pathfinding.Logging.Interface;
using System;
using System.Windows.Input;
using WPFVersion.DependencyInjection;
using WPFVersion.Infrastructure;
using WPFVersion.Messages.DataMessages;
using WPFVersion.Model;

namespace WPFVersion.ViewModel.ButtonViewModels
{
    internal class LoadGraphViewModel
    {
        private readonly IMessenger messenger;
        private readonly IGraphSerializationModule<Graph2D<Vertex>, Vertex> module;
        private readonly ILog log;

        public ICommand LoadGraphCommand { get; }

        private bool IsAllAlgorithmsFinished { get; set; } = true;

        public LoadGraphViewModel()
        {
            messenger = DI.Container.Resolve<IMessenger>();
            messenger.Register<IsAllAlgorithmsFinishedMessage>(this, OnAlgorithmsFinished);
            log = DI.Container.Resolve<ILog>();
            module = DI.Container.Resolve<IGraphSerializationModule<Graph2D<Vertex>, Vertex>>();
            LoadGraphCommand = new RelayCommand(ExecuteLoadGraphCommand, CanExecuteLoadGrapgCommand);
        }

        private async void ExecuteLoadGraphCommand(object param)
        {
            try
            {
                var graph = await module.LoadGraphAsync();
                messenger.Send(new GraphCreatedMessage(graph));
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        private bool CanExecuteLoadGrapgCommand(object param)
        {
            return IsAllAlgorithmsFinished;
        }

        private void OnAlgorithmsFinished(IsAllAlgorithmsFinishedMessage message)
        {
            IsAllAlgorithmsFinished = message.IsAllAlgorithmsFinished;
        }
    }
}
