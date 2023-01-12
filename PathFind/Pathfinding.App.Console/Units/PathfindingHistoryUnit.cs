using GalaSoft.MvvmLight.Messaging;
using Pathfinding.AlgorithmLib.Core.Events;
using Pathfinding.AlgorithmLib.History;
using Pathfinding.AlgorithmLib.History.Interface;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.Visualization.Extensions;
using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.Units
{
    internal sealed class PathfindingHistoryUnit : Unit
    {
        private readonly IMessenger messenger;
        private readonly History<PathfindingHistoryVolume> history = new();

        private Graph2D<Vertex> graph = Graph2D<Vertex>.Empty;
        private bool isHistoryApplied = false;

        public PathfindingHistoryUnit(IReadOnlyCollection<IMenuItem> menuItems,
            IReadOnlyCollection<IConditionedMenuItem> conditioned,
            IMessenger messenger)
            : base(menuItems, conditioned)
        {
            this.messenger = messenger;
            this.messenger.Register<PathfindingRangeChosenMessage>(this, OnRangeChosen);
            this.messenger.Register<PathFoundMessage>(this, OnPathFound);
            this.messenger.Register<SubscribeOnHistoryMessage>(this, OnSubscribeOnHistory);
            this.messenger.Register<GraphCreatedMessage>(this, OnGraphCreated);
            this.messenger.Register<ApplyHistoryMessage>(this, OnHistoryApplied);
            this.messenger.Register<HistoryPageMessage>(this, OnHistoryPage);
            this.messenger.Register<ClearHistoryMessage>(this, _ => history.Clear());
        }

        private void OnHistoryPage(HistoryPageMessage message)
        {
            graph.RestoreVerticesVisualState();
            history.VisualizeHistory(message.PageKey, graph);
        }

        private void OnHistoryApplied(ApplyHistoryMessage message)
        {
            isHistoryApplied = message.IsApplied;
        }

        private void OnGraphCreated(GraphCreatedMessage message)
        {
            graph = message.Graph;
            history.Clear();
        }

        private void OnVertexVisited(object sender, PathfindingEventArgs e)
        {
            if (sender is IHistoryPageKey key)
            {
                history.AddVisited(key.Id, e.Current);
            }
        }

        private void OnMessageRecieved(Action action)
        {
            if (isHistoryApplied)
            {
                action();
            }
        }

        private void OnRangeChosen(PathfindingRangeChosenMessage msg)
        {
            OnMessageRecieved(() => history.AddPathfindingRange(msg.Algorithm.Id, msg.Range));
        }

        private void OnPathFound(PathFoundMessage msg)
        {
            OnMessageRecieved(() => history.AddPath(msg.Algorithm.Id, msg.Path));
        }

        private void OnSubscribeOnHistory(SubscribeOnHistoryMessage msg)
        {
            OnMessageRecieved(() =>
            {
                history.AddRegulars(msg.Algorithm.Id, graph.GetNotObstaclesCoordinates());
                history.AddObstacles(msg.Algorithm.Id, graph.GetObstaclesCoordinates());
                msg.Algorithm.VertexVisited += OnVertexVisited;
            });
        }
    }
}