using Autofac;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Base.EndPoints;
using GraphLib.Interfaces;
using GraphLib.NullRealizations;
using GraphLib.Realizations.Graphs;
using System;
using WPFVersion.DependencyInjection;
using WPFVersion.Infrastructure.EventArguments;
using WPFVersion.Infrastructure.EventHandlers;
using WPFVersion.Messages;
using WPFVersion.Messages.DataMessages;

namespace WPFVersion.ViewModel
{
    public class MainWindowViewModel : IDisposable
    {
        internal event GraphCreatedEventHandler GraphCreated;

        private readonly IMessenger messenger;
        private readonly IVertexEventHolder eventHolder;
        private readonly IGraphFieldFactory fieldFactory;
        private readonly BaseEndPoints endPoints;

        private IGraph Graph { get; set; }

        public MainWindowViewModel(IGraphFieldFactory fieldFactory, IVertexEventHolder eventHolder, BaseEndPoints endPoints)
        {
            Graph = NullGraph.Instance;
            messenger = DI.Container.Resolve<IMessenger>();
            messenger.Register<GraphCreatedMessage>(this, SetGraph);
            this.endPoints = endPoints;
            this.fieldFactory = fieldFactory;
            this.eventHolder = eventHolder;
        }

        public void Dispose()
        {
            messenger.Unregister(this);
        }

        private void SetGraph(GraphCreatedMessage message)
        {
            endPoints.UnsubscribeFromEvents(Graph);
            endPoints.Reset();
            eventHolder.UnsubscribeVertices(Graph);
            Graph = message.Graph;
            var graphField = fieldFactory.CreateGraphField(Graph);
            endPoints.SubscribeToEvents(Graph);
            eventHolder.SubscribeVertices(Graph);
            GraphCreated?.Invoke(this, new GraphCreatedEventArgs((Graph2D)Graph));
            messenger.Send(new GraphFieldCreatedMessage(graphField));
            messenger.Send(new ClearStatisticsMessage());
        }
    }
}