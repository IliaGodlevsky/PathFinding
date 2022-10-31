using Autofac;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Interfaces;
using GraphLib.Realizations.Graphs;
using GraphLib.Serialization.Extensions;
using GraphLib.Serialization.Interfaces;
using Logging.Interface;
using NullObject.Extensions;
using System;
using System.Windows.Input;
using WPFVersion3D.DependencyInjection;
using WPFVersion3D.Infrastructure.Commands;
using WPFVersion3D.Messages.PassValueMessages;
using WPFVersion3D.Model;

namespace WPFVersion3D.ViewModel.ButtonViewModels
{
    internal class SaveGraphViewModel
    {
        private readonly IMessenger messenger;
        private readonly IGraphSerializationModule<Graph3D<Vertex3D>, Vertex3D> module;
        private readonly ILog log;

        private IGraph<Vertex3D> Graph { get; set; }

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
            return !Graph.IsNull();
        }

        private void OnGraphCreated(GraphCreatedMessage message)
        {
            Graph = message.Value;
        }
    }
}
