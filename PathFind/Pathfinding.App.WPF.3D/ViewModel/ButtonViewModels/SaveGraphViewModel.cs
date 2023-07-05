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
    internal class SaveGraphViewModel
    {
        private readonly IMessenger messenger;
        private readonly IGraphSerializationModule<Graph3D<Vertex3D>, Vertex3D> module;
        private readonly ILog log;

        private Graph3D<Vertex3D> Graph { get; set; } = Graph3D<Vertex3D>.Empty;

        public ICommand SaveGraphCommand { get; }

        public SaveGraphViewModel()
        {
            module = DI.Container.Resolve<IGraphSerializationModule<Graph3D<Vertex3D>, Vertex3D>>();
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
            return Graph != Graph3D<Vertex3D>.Empty;
        }

        private void OnGraphCreated(GraphCreatedMessage message)
        {
            Graph = message.Value;
        }
    }
}
