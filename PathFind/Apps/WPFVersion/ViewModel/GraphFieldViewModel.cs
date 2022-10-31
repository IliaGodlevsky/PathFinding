using Autofac;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Realizations.Graphs;
using WPFVersion.DependencyInjection;
using WPFVersion.Messages.ActionMessages;
using WPFVersion.Messages.DataMessages;
using WPFVersion.Model;
using WPFVersion.ViewModel.BaseViewModels;

namespace WPFVersion.ViewModel
{
    internal class GraphFieldViewModel : NotifyPropertyChanged
    {
        private readonly IMessenger messenger;

        private Graph2D<Vertex> graph;
        private IGraphField<Vertex> field;
        private string graphParamtres;

        public IGraphField<Vertex> GraphField
        {
            get => field;
            private set => Set(ref field, value);
        }

        public string GraphParamtres
        {
            get => graphParamtres;
            private set => Set(ref graphParamtres, value);
        }

        public GraphFieldViewModel()
        {
            messenger = DI.Container.Resolve<IMessenger>();
            messenger.Register<GraphCreatedMessage>(this, OnGraphCreated);
            messenger.Register<GraphChangedMessage>(this, OnGraphChanged);
        }

        private void OnGraphCreated(GraphCreatedMessage message)
        {
            graph = message.Graph;
            var graphFieldFactory = DI.Container.Resolve<IGraphFieldFactory<Graph2D<Vertex>, Vertex, GraphField>>();
            GraphField = graphFieldFactory.CreateGraphField(message.Graph);
            GraphParamtres = graph.GetStringRepresentation();
        }

        private void OnGraphChanged(GraphChangedMessage message)
        {
            GraphParamtres = graph.GetStringRepresentation();
        }
    }
}
