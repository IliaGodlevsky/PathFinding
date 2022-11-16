using GalaSoft.MvvmLight.Messaging;
using Pathfinding.Logging.Interface;
using System;
using System.Windows.Input;
using WPFVersion.DependencyInjection;
using Pathfinding.App.WPF._2D.Model;
using Pathfinding.App.WPF._2D.Messages.DataMessages;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.App.WPF._2D.Infrastructure;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions;
using Autofac;

namespace Pathfinding.App.WPF._2D.ViewModel.ButtonViewModels
{
    internal class SaveGraphViewModel
    {
        private readonly IMessenger messenger;
        private readonly IGraphSerializationModule<Graph2D<Vertex>, Vertex> module;
        private readonly ILog log;

        private Graph2D<Vertex> Graph { get; set; }

        public ICommand SaveGraphCommand { get; }

        public SaveGraphViewModel()
        {
            module = DI.Container.Resolve<IGraphSerializationModule<Graph2D<Vertex>, Vertex>>();
            messenger = DI.Container.Resolve<IMessenger>();
            log = DI.Container.Resolve<ILog>();
            SaveGraphCommand = new RelayCommand(ExecuteSaveGraphCommand, CanExecuteSaveGraphCommand);
            messenger.Register<GraphCreatedMessage>(this, OnGraphCreated);
        }

        private async void ExecuteSaveGraphCommand(object param)
        {
            try
            {
                await module.SaveGraphAsync(Graph);
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
