using Autofac;
using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.WPF._2D.Infrastructure;
using Pathfinding.App.WPF._2D.Messages.DataMessages;
using Pathfinding.App.WPF._2D.Model;
using Pathfinding.GraphLib.Core.Modules.Commands;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions;
using Pathfinding.Logging.Interface;
using Shared.Primitives.ValueRange;
using System;
using System.Linq;
using System.Windows.Input;
using WPFVersion.DependencyInjection;

namespace Pathfinding.App.WPF._2D.ViewModel.ButtonViewModels
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
