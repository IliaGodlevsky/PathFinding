using Algorithm.Infrastructure.EventArguments;
using Autofac;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Interfaces;
using System;
using System.Threading.Tasks;
using Visualization;
using WPFVersion.DependencyInjection;
using WPFVersion.Enums;
using WPFVersion.Messages;

namespace WPFVersion.Model
{
    internal sealed class PathfindingVisualizationModel : PathfindingVisualization, IDisposable
    {
        public PathfindingVisualizationModel(IGraph graph) : base(graph)
        {
            messenger = DI.Container.Resolve<IMessenger>();
            messenger.Register<PathFoundMessage>(this, MessageTokens.VisualizationModel, PathFound);
            messenger.Register<SubscribeOnAlgorithmEventsMessage>(this, MessageTokens.VisualizationModel, Subscribe);
            messenger.Register<EndPointsChosenMessage>(this, MessageTokens.VisualizationModel, RegisterEndPointsForAlgorithm);
            messenger.Register<SubscribeOnCostChangedMessage>(this, MessageTokens.VisualizationModel, SubscribeOnCostChanged);
            messenger.Register<UnsubscribeFromCostChangedMessage>(this, MessageTokens.VisualizationModel, UnsubscribeFromCostChanged);
            messenger.Register<ReturnActualCostsMessage>(this, MessageTokens.VisualizationModel, ReturnActualCosts);
        }

        public void Dispose()
        {
            Clear();
            messenger.Unregister(this);
        }

        protected override async void OnVertexEnqueued(object sender, AlgorithmEventArgs e)
        {
            await Task.Run(() => base.OnVertexEnqueued(sender, e));
        }

        protected override async void OnVertexVisited(object sender, AlgorithmEventArgs e)
        {
            await Task.Run(() => base.OnVertexVisited(sender, e));
        }

        private void ReturnActualCosts(ReturnActualCostsMessage message)
        {
            ReturnActualCosts();
        }

        private void SubscribeOnCostChanged(SubscribeOnCostChangedMessage message)
        {
            SubscribeOnCostChanged(message.Notifier);
        }

        private void UnsubscribeFromCostChanged(UnsubscribeFromCostChangedMessage message)
        {
            UnsubscribeFromCostChanged(message.Notifier);
        }

        private void Subscribe(SubscribeOnAlgorithmEventsMessage message)
        {
            if (message.IsVisualizationRequired)
            {
                SubscribeOnAlgorithmEvents(message.Algorithm);
            }
            else
            {
                message.Algorithm.Started += OnAlgorithmStarted;
            }
        }

        private void RegisterEndPointsForAlgorithm(EndPointsChosenMessage message)
        {
            AddEndPoints(message.Algorithm, message.EndPoints);
        }

        private void PathFound(PathFoundMessage message)
        {
            AddPathVertices(message.Algorithm, message.Path);
        }

        private readonly IMessenger messenger;
    }
}