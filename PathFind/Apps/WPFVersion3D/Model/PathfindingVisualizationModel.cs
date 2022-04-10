using Algorithm.Infrastructure.EventArguments;
using Autofac;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Interfaces;
using System;
using System.Threading.Tasks;
using Visualization;
using WPFVersion3D.DependencyInjection;
using WPFVersion3D.Messages.PassValueMessages;

namespace WPFVersion3D.Model
{
    internal sealed class PathfindingVisualizationModel : PathfindingVisualization, IDisposable
    {
        private readonly IMessenger messenger;

        public PathfindingVisualizationModel(IGraph graph) : base(graph)
        {
            messenger = DI.Container.Resolve<IMessenger>();
            messenger.Register<PathFoundMessage>(this, PathFound);
            messenger.Register<SubscribeOnAlgorithmEventsMessage>(this, Subscribe);
            messenger.Register<EndPointsChosenMessage>(this, RegisterEndPointsForAlgorithm);
        }

        public void Dispose()
        {
            Clear();
            messenger.Unregister(this);
        }

        protected override async void OnVertexEnqueued(object sender, AlgorithmEventArgs e)
        {
            await Task.Run(() => base.OnVertexEnqueued(sender, e)).ConfigureAwait(false);
        }

        protected override async void OnVertexVisited(object sender, AlgorithmEventArgs e)
        {
            await Task.Run(() => base.OnVertexVisited(sender, e)).ConfigureAwait(false);
        }

        private void Subscribe(SubscribeOnAlgorithmEventsMessage message)
        {
            if (message.IsVisualizationRequired)
            {
                SubscribeOnAlgorithmEvents(message.Value);
            }
            else
            {
                message.Value.Started += OnAlgorithmStarted;
            }
        }

        private void RegisterEndPointsForAlgorithm(EndPointsChosenMessage message)
        {
            AddEndPoints(message.Algorithm, message.Value);
        }

        private void PathFound(PathFoundMessage message)
        {
            AddPathVertices(message.Algorithm, message.Value);
        }
    }
}