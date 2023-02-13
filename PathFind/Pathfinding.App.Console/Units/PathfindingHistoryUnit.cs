using GalaSoft.MvvmLight.Messaging;
using Pathfinding.AlgorithmLib.Core.Events;
using Pathfinding.AlgorithmLib.History.Interface;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.Visualization.Extensions;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.Units
{
    internal sealed class PathfindingHistoryUnit : Unit
    {
        private readonly IMessenger messenger;
        private readonly IHistory<ConsoleColor> history;

        private Graph2D<Vertex> graph = Graph2D<Vertex>.Empty;
        private bool isHistoryApplied = false;

        public PathfindingHistoryUnit(IReadOnlyCollection<IMenuItem> menuItems,
            IReadOnlyCollection<IConditionedMenuItem> conditioned,
            IHistory<ConsoleColor> history,
            IMessenger messenger)
            : base(menuItems, conditioned)
        {
            this.messenger = messenger;
            this.history = history;
            this.messenger.Register<PathfindingRangeChosenMessage>(this, OnRangeChosen);
            this.messenger.Register<PathFoundMessage>(this, OnPathFound);
            this.messenger.Register<SubscribeOnHistoryMessage>(this, OnSubscribeOnHistory);
            this.messenger.Register<GraphCreatedMessage>(this, OnGraphCreated);
            this.messenger.Register<ApplyHistoryMessage>(this, OnHistoryApplied);
            this.messenger.Register<HistoryPageMessage>(this, OnHistoryPage);
            this.messenger.Register<ClearHistoryMessage>(this, _ => history.RemoveAll());
        }

        private void OnHistoryPage(HistoryPageMessage message)
        {
            graph.RestoreVerticesVisualState();
            var page = history.Get(message.PageKey);
            foreach (var (position, color) in page)
            {
                var vertex = graph.Get(position);
                vertex.Color = color;
                vertex.Display();
            }
        }

        private void OnHistoryApplied(ApplyHistoryMessage message)
        {
            isHistoryApplied = message.IsApplied;
        }

        private void OnGraphCreated(GraphCreatedMessage message)
        {
            graph = message.Graph;
            history.RemoveAll();
        }

        private void OnVertexVisited(object sender, PathfindingEventArgs e)
        {
            if (sender is IHistoryPageKey key)
            {
                var vertex = graph.Get(e.Current);
                history.Add(key.Id, vertex);
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
            OnMessageRecieved(() => history.AddRange(msg.Algorithm.Id, msg.Range));
        }

        private void OnPathFound(PathFoundMessage msg)
        {
            OnMessageRecieved(() => history.AddRange(msg.Algorithm.Id, msg.Path.Select(graph.Get)));
        }

        private void OnSubscribeOnHistory(SubscribeOnHistoryMessage msg)
        {
            OnMessageRecieved(() =>
            {
                history.AddRange(msg.Algorithm.Id, graph);
                msg.Algorithm.VertexVisited += OnVertexVisited;
            });
        }
    }
}