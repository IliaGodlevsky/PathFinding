using Autofac;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Base.EndPoints;
using GraphLib.Interfaces;
using GraphLib.NullRealizations;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WPFVersion3D.DependencyInjection;
using WPFVersion3D.Enums;
using WPFVersion3D.Extensions;
using WPFVersion3D.Messages;

namespace WPFVersion3D.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly IVertexEventHolder eventHolder;
        private readonly IGraphFieldFactory fieldFactory;
        private readonly BaseEndPoints endPoints;
        private readonly IMessenger messenger;

        private string graphParametres;

        private IGraph Graph { get; set; }

        private bool IsAllAlgorithmsFinished { get; set; } = true;

        public string GraphParametres
        {
            get => graphParametres;
            set { graphParametres = value; OnPropertyChanged(); }
        }

        public MainWindowViewModel(IGraphFieldFactory fieldFactory, IVertexEventHolder eventHolder, BaseEndPoints endPoints)
        {
            Graph = NullGraph.Instance;
            this.fieldFactory = fieldFactory;
            this.endPoints = endPoints;
            this.eventHolder = eventHolder;
            messenger = DI.Container.Resolve<IMessenger>();
            messenger.Register<GraphCreatedMessage>(this, MessageTokens.MainModel, SetGraph);
        }

        public void Dispose()
        {
            messenger.Unregister(this);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ConnectNewGraph(IGraph graph)
        {
            endPoints.UnsubscribeFromEvents(Graph);
            endPoints.Reset();
            eventHolder.UnsubscribeVertices(Graph);
            Graph = graph;
            var graphField = fieldFactory.CreateGraphField(Graph);
            endPoints.SubscribeToEvents(Graph);
            eventHolder.SubscribeVertices(Graph);
            GraphParametres = Graph.ToString();
            messenger
                .Forward(new ClearStatisticsMessage(), MessageTokens.AlgorithmStatisticsModel)
                .Forward(new GraphFieldCreatedMessage(graphField), MessageTokens.Everyone);
        }

        private void SetGraph(GraphCreatedMessage message)
        {
            ConnectNewGraph(message.Value);
        }
    }
}