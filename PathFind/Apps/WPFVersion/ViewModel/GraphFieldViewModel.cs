using Autofac;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Interfaces;
using GraphLib.Realizations.Graphs;
using WPFVersion.DependencyInjection;
using WPFVersion.Messages.ActionMessages;
using WPFVersion.Messages.DataMessages;
using WPFVersion.ViewModel.BaseViewModels;

namespace WPFVersion.ViewModel
{
    internal class GraphFieldViewModel : NotifyPropertyChanged
    {
        private readonly IMessenger messenger;

        private Graph2D graph;
        private IGraphField field;
        private string graphParamtres;

        public IGraphField GraphField { get => field; set => Set(ref field, value); }

        public string GraphParamtres { get => graphParamtres; set => Set(ref graphParamtres, value); }

        public GraphFieldViewModel()
        {
            messenger = DI.Container.Resolve<IMessenger>();
            messenger.Register<GraphFieldCreatedMessage>(this, OnFieldCreated);
            messenger.Register<GraphCreatedMessage>(this, OnGraphCreated);
            messenger.Register<GraphChangedMessage>(this, OnGraphChanged);
        }

        private void OnFieldCreated(GraphFieldCreatedMessage message)
        {
            GraphField = message.Field;
        }

        private void OnGraphCreated(GraphCreatedMessage message)
        {
            graph = (Graph2D)message.Graph;
            GraphParamtres = graph.ToString();
        }

        private void OnGraphChanged(GraphChangedMessage message)
        {
            GraphParamtres = graph.ToString();
        }
    }
}
