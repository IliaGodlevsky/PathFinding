using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Events;
using Pathfinding.App.Console.DataAccess;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Modules.Interface;
using Pathfinding.GraphLib.Core.Realizations;
using Pathfinding.Visualization.Extensions;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.Units
{
    internal sealed class PathfindingHistoryUnit : Unit, ICanRecieveMessage
    {
        private readonly GraphsPathfindingHistory history;
        private readonly IPathfindingRangeBuilder<Vertex> builder;
        private readonly List<ICoordinate> visitedVertices = new();

        private IGraph<Vertex> graph = Graph<Vertex>.Empty;
        private PathfindingProcess algorithm = PathfindingProcess.Idle;

        private bool isHistoryApplied = true;

        public PathfindingHistoryUnit(IReadOnlyCollection<IMenuItem> menuItems,
            IPathfindingRangeBuilder<Vertex> builder,
            GraphsPathfindingHistory history) : base(menuItems)
        {
            this.history = history;
            this.builder = builder;
        }

        private void VisualizeHistory(AlgorithmKeyMessage msg)
        {
            var key = msg.AlgorithmKey;
            graph.RestoreVerticesVisualState();
            var currentHistory = history.GetHistory(graph.GetHashCode());
            graph.ApplyCosts(currentHistory.Costs[key]);
            var obstacles = currentHistory.Obstacles[key].Select(graph.Get);
            obstacles.ForEach(vertex => vertex.VisualizeAsObstacle());
            var visited = currentHistory.Visited[key].Select(graph.Get);
            visited.ForEach(vertex => vertex.VisualizeAsVisited());
            currentHistory.Ranges[key].Select(graph.Get).Reverse().VisualizeAsRange();
            currentHistory.Paths[key].Select(graph.Get).VisualizeAsPath();
        }

        private void SetIsApplied(IsAppliedMessage msg)
        {
            isHistoryApplied = msg.IsApplied;
        }

        private void SetGraph(GraphMessage msg)
        {
            graph = msg.Graph;
        }

        private void ClearHistory(ClearHistoryMessage msg)
        {
            var hist = history.GetHistory(graph.GetHashCode());
            hist.Obstacles.Clear();
            hist.Paths.Clear();
            hist.Ranges.Clear();
            hist.Costs.Clear();
            hist.Visited.Clear();
            hist.Algorithms.Clear();
        }

        private void OnVertexVisited(object sender, PathfindingEventArgs args)
        {
            visitedVertices.Add(args.Current);
        }

        private void OnFinished(object sender, EventArgs args)
        {
            var visited = history.GetHistory(graph.GetHashCode()).Visited;
            visited.TryGetOrAddNew(algorithm.GetHashCode()).AddRange(visitedVertices);
            visitedVertices.Clear();
        }

        private void OnStarted(object sender, EventArgs args)
        {
            int algorithmId = algorithm.GetHashCode();
            var hist = history.GetHistory(graph.GetHashCode());
            hist.Algorithms.Add(algorithmId);
            var obstacles = hist.Obstacles.TryGetOrAddNew(algorithmId);
            obstacles.AddRange(graph.GetObstaclesCoordinates());
            var costs = hist.Costs.TryGetOrAddNew(algorithmId);
            costs.AddRange(graph.GetCosts());
            var ranges = hist.Ranges.TryGetOrAddNew(algorithmId);
            ranges.AddRange(builder.Range.GetCoordinates());
        }

        private void PrepareForPathfinding(AlgorithmMessage msg)
        {
            algorithm = msg.Algorithm;
            algorithm.Started += OnStarted;
            algorithm.VertexVisited += OnVertexVisited;
            algorithm.Finished += OnFinished;
        }

        private void SetStatistics(StatisticsMessage msg)
        {
            history.GetHistory(graph.GetHashCode())
                .Statistics[algorithm.GetHashCode()] = msg.Statistics;
        }

        private void OnPathFound(PathFoundMessage msg)
        {
            history.GetHistory(graph.GetHashCode()).Paths
                .TryGetOrAddNew(algorithm.GetHashCode()).AddRange(msg.Path);
        }

        private bool IsHistoryApplied() => isHistoryApplied;

        public void RegisterHanlders(IMessenger messenger)
        {
            var token = Tokens.History.Bind(IsHistoryApplied);
            messenger.RegisterGraph(this, Tokens.Common, SetGraph);
            messenger.Register<PathfindingHistoryUnit, IsAppliedMessage>(this, Tokens.History, SetIsApplied);
            messenger.Register<PathfindingHistoryUnit, AlgorithmKeyMessage>(this, token, VisualizeHistory);
            messenger.Register<PathfindingHistoryUnit, AlgorithmMessage>(this, token, PrepareForPathfinding);
            messenger.Register<PathfindingHistoryUnit, PathFoundMessage>(this, token, OnPathFound);
            messenger.Register<PathfindingHistoryUnit, StatisticsMessage>(this, token, SetStatistics);
            messenger.Register<PathfindingHistoryUnit, ClearHistoryMessage>(this, Tokens.History, ClearHistory);
        }
    }
}