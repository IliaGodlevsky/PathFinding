using Autofac;
using GalaSoft.MvvmLight.Messaging;
using Pathfinding.AlgorithmLib.Core.Events;
using Pathfinding.AlgorithmLib.Visualization;
using Pathfinding.App.WPF._2D.Messages.ActionMessages;
using Pathfinding.App.WPF._2D.Messages.DataMessages;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using System;
using System.Threading.Tasks;
using WPFVersion.DependencyInjection;

namespace Pathfinding.App.WPF._2D.Model
{
    internal sealed class PathfindingVisualizationModel : PathfindingVisualization<Graph2D<Vertex>, Vertex>, IDisposable
    {
        private readonly IMessenger messenger;

        public PathfindingVisualizationModel(Graph2D<Vertex> graph) : base(graph)
        {
            messenger = DI.Container.Resolve<IMessenger>();
            messenger.Register<PathFoundMessage>(this, PathFound);
            messenger.Register<SubscribeOnAlgorithmEventsMessage>(this, Subscribe);
            messenger.Register<PathfindingRangeChosenMessage>(this, RegisterEndPointsForAlgorithm);
        }

        public void Dispose()
        {
            Clear();
            messenger.Unregister(this);
        }

        protected override async void OnVertexEnqueued(object sender, PathfindingEventArgs e)
        {
            await Task.Run(() => base.OnVertexEnqueued(sender, e)).ConfigureAwait(false);
        }

        protected override async void OnVertexVisited(object sender, PathfindingEventArgs e)
        {
            await Task.Run(() => base.OnVertexVisited(sender, e)).ConfigureAwait(false);
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

        private void RegisterEndPointsForAlgorithm(PathfindingRangeChosenMessage message)
        {
            Add(message.Algorithm, message.PathfindingRange);
        }

        private void PathFound(PathFoundMessage message)
        {
            Add(message.Algorithm, message.Path);
        }
    }
}