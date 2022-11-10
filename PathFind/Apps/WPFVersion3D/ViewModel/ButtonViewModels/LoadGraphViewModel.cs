using Autofac;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Realizations.Graphs;
using GraphLib.Serialization.Extensions;
using GraphLib.Serialization.Interfaces;
using Pathfinding.Logging.Interface;
using System;
using System.Windows.Input;
using WPFVersion3D.DependencyInjection;
using WPFVersion3D.Infrastructure.Commands;
using WPFVersion3D.Messages.PassValueMessages;
using WPFVersion3D.Model;

namespace WPFVersion3D.ViewModel.ButtonViewModels
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
