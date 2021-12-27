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
            messenger.Register<SubscribeOnAlgorithmEventsMessage>(this, MessageTokens.VisualizationModel, AlgorithmChosen);
            messenger.Register<EndPointsChosenMessage>(this, MessageTokens.VisualizationModel, RegisterEndPointsForAlgorithm);
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

        private void AlgorithmChosen(SubscribeOnAlgorithmEventsMessage message)
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