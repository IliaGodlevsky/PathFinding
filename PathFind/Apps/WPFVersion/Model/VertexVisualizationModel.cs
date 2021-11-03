using Algorithm.Base;
using Algorithm.Infrastructure.EventArguments;
using Algorithm.Interfaces;
using Common.Extensions.EnumerableExtensions;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphViewModel;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using WPFVersion.Messages;

namespace WPFVersion.Model
{
    internal sealed class VertexVisualizationModel : VisualizationModel, IDisposable
    {
        public VertexVisualizationModel(IGraph graph) : base(graph)
        {
            Messenger.Default.Register<PathFoundMessage>(this, MessageTokens.VisualizationModel, PathFound);
            Messenger.Default.Register<SubscribeOnAlgorithmEventsMessage>(this, MessageTokens.VisualizationModel, AlgorithmChosen);
            Messenger.Default.Register<EndPointsChosenMessage>(this, MessageTokens.VisualizationModel, RegisterEndPointsForAlgorithm);
        }

        public override async void OnVertexEnqueued(object sender, AlgorithmEventArgs e)
        {
            await Task.Run(() => base.OnVertexEnqueued(sender, e));
        }

        public override async void OnVertexVisited(object sender, AlgorithmEventArgs e)
        {
            await Task.Run(() => base.OnVertexVisited(sender, e));
        }

        public void OnAlgorithmFinished(object sender, EventArgs e)
        {
            if (sender is IAlgorithm algorithm)
            {
                var visited = visitedVertices.GetOrEmpty(algorithm);
                var enqueued = enqueuedVertices.GetOrEmpty(algorithm);
                var except = enqueued.Values.Except(visited.Values).ToDictionary();
                var dictionary = new ConcurrentDictionary<ICoordinate, IVertex>(except);
                enqueuedVertices.TryAddOrUpdate(algorithm, dictionary);
            }
        }

        public override void SubscribeOnAlgorithmEvents(PathfindingAlgorithm algorithm)
        {
            base.SubscribeOnAlgorithmEvents(algorithm);
            algorithm.Finished += OnAlgorithmFinished;
        }

        private void AlgorithmChosen(SubscribeOnAlgorithmEventsMessage message)
        {
            SubscribeOnAlgorithmEvents(message.Algorithm);
        }

        private void RegisterEndPointsForAlgorithm(EndPointsChosenMessage message)
        {
            AddEndPoints(message.Algorithm, message.EndPoints);
        }

        private void PathFound(PathFoundMessage message)
        {
            AddPathVertices(message.Algorithm, message.Path);
        }

        public void Dispose()
        {
            Messenger.Default.Unregister(this);
        }
    }
}