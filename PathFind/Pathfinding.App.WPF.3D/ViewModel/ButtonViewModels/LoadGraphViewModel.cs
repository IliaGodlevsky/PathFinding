using Autofac;
using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.WPF._3D.DependencyInjection;
using Pathfinding.App.WPF._3D.Infrastructure.Commands;
using Pathfinding.App.WPF._3D.Messages.PassValueMessages;
using Pathfinding.App.WPF._3D.Model;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions;
using Pathfinding.Logging.Interface;
using System;
using System.Windows.Input;

namespace Pathfinding.App.WPF._3D.ViewModel.ButtonViewModels
{
    internal class LoadGraphViewModel
    {
        private readonly IMessenger messenger;
        private readonly ILog log;
        private readonly IGraphSerializationModule<Graph3D<Vertex3D>, Vertex3D> module;

        private bool IsAllAlgorithmsFinishedPathfinding { get; set; } = true;

        public ICommand LoadGraphCommand { get; }

        public LoadGraphViewModel()
        {
            messenger = DI.Container.Resolve<IMessenger>();
            messenger.Register<IsAllAlgorithmsFinishedMessage>(this, OnAllAlgorithmFinishedPathfinding);
            module = DI.Container.Resolve<IGraphSerializationModule<Graph3D<Vertex3D>, Vertex3D>>();
            log = DI.Container.Resolve<ILog>();
            LoadGraphCommand = new RelayCommand(ExecuteLoadGraphCommand);
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

        private bool CanExecuteLoadGraphCommand(object param)
        {
            return IsAllAlgorithmsFinishedPathfinding;
        }

        private void OnAllAlgorithmFinishedPathfinding(IsAllAlgorithmsFinishedMessage message)
        {
            IsAllAlgorithmsFinishedPathfinding = message.Value;
        }
    }
}
