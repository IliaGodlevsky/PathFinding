using Autofac;
using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.WPF._2D.Infrastructure;
using Pathfinding.App.WPF._2D.Messages.DataMessages;
using Pathfinding.App.WPF._2D.Model;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions;
using Pathfinding.Logging.Interface;
using System;
using System.Windows.Input;
using WPFVersion.DependencyInjection;

namespace Pathfinding.App.WPF._2D.ViewModel.ButtonViewModels
{
    internal sealed class SendGraphViewModel
    {
        private readonly IMessenger messenger;
        private readonly IGraphSerializer<Graph2D<Vertex>, Vertex> serializer;
        private readonly ILog log;

        private Graph2D<Vertex> Graph { get; set; }

        public ICommand SendGraphCommand { get; }

        public SendGraphViewModel()
        {
            serializer = DI.Container.Resolve<IGraphSerializer<Graph2D<Vertex>, Vertex>>();
            messenger = DI.Container.Resolve<IMessenger>();
            log = DI.Container.Resolve<ILog>();
            SendGraphCommand = new RelayCommand(ExecuteSaveGraphCommand, CanExecuteSaveGraphCommand);
            messenger.Register<GraphCreatedMessage>(this, OnGraphCreated);
        }

        private async void ExecuteSaveGraphCommand(object param)
        {
            try
            {
                await serializer.SerializeToNetworkAsync(Graph, "localhost", 8080);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        private bool CanExecuteSaveGraphCommand(object param)
        {
            return Graph != Graph2D<Vertex>.Empty;
        }

        private void OnGraphCreated(GraphCreatedMessage message)
        {
            Graph = message.Graph;
        }
    }
}
