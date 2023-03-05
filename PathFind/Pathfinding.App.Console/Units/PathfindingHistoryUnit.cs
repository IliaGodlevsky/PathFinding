using GalaSoft.MvvmLight.Messaging;
using Pathfinding.AlgorithmLib.Core.Events;
using Pathfinding.AlgorithmLib.History;
using Pathfinding.AlgorithmLib.History.Interface;
using Pathfinding.App.Console.Extensions;
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
    internal sealed class PathfindingHistoryUnit : Unit, ICanRecieveMessage
    {
        private readonly IMessenger messenger;
        private readonly History<PathfindingHistoryVolume> history = new();
        private readonly Dictionary<Guid, IReadOnlyList<int>> pricesHistory = new();

        private Graph2D<Vertex> graph = Graph2D<Vertex>.Empty;
        private bool isHistoryApplied = false;

        public PathfindingHistoryUnit(IReadOnlyCollection<IMenuItem> menuItems,
            IReadOnlyCollection<IConditionedMenuItem> conditioned,
            IMessenger messenger)
            : base(menuItems, conditioned)
        {
            this.messenger = messenger;
        }

        private void OnHistoryPage(HistoryPageMessage message)
        {
            graph.RestoreVerticesVisualState();
            graph.ApplyCosts(pricesHistory[message.PageKey]);
            history.VisualizeHistory(message.PageKey, graph);
        }

        private void OnHistoryApplied(ApplyHistoryMessage message)
        {
            isHistoryApplied = message.IsApplied;
        }

        private void OnGraphCreated(GraphCreatedMessage message)
        {
            graph = message.Graph;
            ClearHistory();
        }

        private void ClearHistory()
        {
            history.Clear();
            pricesHistory.Clear();
        }

        private void OnVertexVisited(object sender, PathfindingEventArgs e)
        {
            if (sender is IHistoryPageKey key)
            {
                history.AddVisited(key.Id, e.Current);
            }
        }

        private bool IsHistoryApplied() => isHistoryApplied;

        private void OnRangeChosen(PathfindingRangeChosenMessage msg)
        {
            history.AddPathfindingRange(msg.Key, msg.Range);
        }

        private void OnPathFound(PathFoundMessage msg)
        {
            history.AddPath(msg.Algorithm.Id, msg.Path);
        }

        private void OnSubscribeOnHistory(SubscribeOnHistoryMessage msg)
        {
            history.AddObstacles(msg.Algorithm.Id, graph.GetObstaclesCoordinates());
            pricesHistory.Add(msg.Algorithm.Id, graph.GetCosts());
            msg.Algorithm.VertexVisited += OnVertexVisited;
        }

        public void RegisterHanlders(IMessenger messenger)
        {
            var token = ConditionToken.Create(IsHistoryApplied, MessageTokens.HistoryUnit);
            messenger.Register<PathfindingRangeChosenMessage>(this, token, OnRangeChosen);
            messenger.Register<PathFoundMessage>(this, token, OnPathFound);
            messenger.Register<SubscribeOnHistoryMessage>(this, token, OnSubscribeOnHistory);
            messenger.Register<GraphCreatedMessage>(this, OnGraphCreated);
            messenger.Register<ApplyHistoryMessage>(this, OnHistoryApplied);
            messenger.Register<HistoryPageMessage>(this, OnHistoryPage);
            messenger.Register<ClearHistoryMessage>(this, _ => ClearHistory());
        }
    }
}