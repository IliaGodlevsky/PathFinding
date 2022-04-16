using Autofac;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Base.EndPoints;
using GraphLib.Interfaces;
using GraphLib.NullRealizations;
using System;
using WPFVersion3D.DependencyInjection;
using WPFVersion3D.Messages.ActionMessages;
using WPFVersion3D.Messages.PassValueMessages;

namespace WPFVersion3D.ViewModel
{
    public class MainWindowViewModel : IDisposable
    {
        private readonly IVertexEventHolder eventHolder;
        private readonly IGraphFieldFactory fieldFactory;
        private readonly BaseEndPoints endPoints;
        private readonly IMessenger messenger;

        private IGraph Graph { get; set; }

        public MainWindowViewModel(IGraphFieldFactory fieldFactory, IVertexEventHolder eventHolder, BaseEndPoints endPoints)
        {
            Graph = NullGraph.Instance;
            this.fieldFactory = fieldFactory;
            this.endPoints = endPoints;
            this.eventHolder = eventHolder;
            messenger = DI.Container.Resolve<IMessenger>();
            messenger.Register<GraphCreatedMessage>(this, SetGraph);
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
            Graph = message.Value;
            endPoints.SubscribeToEvents(Graph);
            eventHolder.SubscribeVertices(Graph);
            var graphField = fieldFactory.CreateGraphField(Graph);
            messenger.Send(new ClearStatisticsMessage());
            messenger.Send(new GraphFieldCreatedMessage(graphField));
        }
    }
}