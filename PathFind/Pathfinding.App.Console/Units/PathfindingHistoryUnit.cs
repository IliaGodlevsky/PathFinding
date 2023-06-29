using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.DataAccess;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.Visualization.Extensions;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.Units
{
    internal sealed class PathfindingHistoryUnit : Unit, ICanRecieveMessage
    {
        private readonly PathfindingHistory history;

        private Graph2D<Vertex> graph = Graph2D<Vertex>.Empty;
        private bool isHistoryApplied = true;

        public PathfindingHistoryUnit(IReadOnlyCollection<IMenuItem> menuItems,
            IReadOnlyCollection<IConditionedMenuItem> conditioned,
            PathfindingHistory history)
            : base(menuItems, conditioned)
        {
            this.history = history;
        }

        private void VisualizeHistory(Guid key)
        {
            graph.RestoreVerticesVisualState();
            var currentHistory = history.GetFor(graph);
            graph.ApplyCosts(currentHistory.Costs[key]);
            currentHistory.Obstacles[key].Select(graph.Get).ForEach(vertex => vertex.VisualizeAsObstacle());
            currentHistory.Visited[key].Select(graph.Get).ForEach(vertex => vertex.VisualizeAsVisited());
            currentHistory.Ranges[key].Select(graph.Get).Reverse().VisualizeAsRange();
            currentHistory.Paths[key].Select(graph.Get).VisualizeAsPath();
        }

        private void SetIsApplied(bool isApplied)
        {
            isHistoryApplied = isApplied;
        }

        private void SetGraph(Graph2D<Vertex> graph)
        {
            this.graph = graph;
        }

        private void ClearHistory(ClearHistoryMessage msg)
        {
            var hist = history.GetFor(graph);
            hist.Obstacles.Clear();
            hist.Paths.Clear();
            hist.Ranges.Clear();
            hist.Costs.Clear();
            hist.Visited.Clear();
            hist.Algorithms.Clear();
        }

        private bool IsHistoryApplied() => isHistoryApplied;

        public void RegisterHanlders(IMessenger messenger)
        {
            var token = Tokens.Bind(IsHistoryApplied, Tokens.History);
            messenger.RegisterGraph(this, Tokens.Common, SetGraph);
            messenger.RegisterData<bool>(this, Tokens.History, SetIsApplied);
            messenger.RegisterData<Guid>(this, token, VisualizeHistory);
            messenger.Register<ClearHistoryMessage>(this, Tokens.History, ClearHistory);
        }
    }
}