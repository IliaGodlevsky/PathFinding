using Autofac;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Realizations.Graphs;
using GraphLib.Serialization.Extensions;
using GraphLib.Serialization.Interfaces;
using Logging.Interface;
using NullObject.Extensions;
using System;
using System.Windows.Input;
using WPFVersion.DependencyInjection;
using WPFVersion.Infrastructure;
using WPFVersion.Messages.DataMessages;
using WPFVersion.Model;

namespace WPFVersion.ViewModel.ButtonViewModels
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
            return !Graph.IsNull();
        }

        private void OnGraphCreated(GraphCreatedMessage message)
        {
            Graph = message.Graph;
        }
    }
}
