using GalaSoft.MvvmLight.Messaging;
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
            var currentHistory = history.GetFor(graph);
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
            var hist = history.GetFor(graph);
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
            var visited = history.GetFor(graph).Visited;
            visited.TryGetOrAddNew(algorithm.Id).AddRange(visitedVertices);
            visitedVertices.Clear();
        }

        private void OnStarted(object sender, EventArgs args)
        {
            var hist = history.GetFor(graph);
            hist.Algorithms.Add(algorithm.Id);
            var obstacles = hist.Obstacles.TryGetOrAddNew(algorithm.Id);
            obstacles.AddRange(graph.GetObstaclesCoordinates());
            var costs = hist.Costs.TryGetOrAddNew(algorithm.Id);
            costs.AddRange(graph.GetCosts());
            var ranges = hist.Ranges.TryGetOrAddNew(algorithm.Id);
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
            history.GetFor(graph).Statistics[algorithm.Id] = msg.Statistics;
        }

        private void OnPathFound(PathFoundMessage msg)
        {
            history.GetFor(graph).Paths.TryGetOrAddNew(algorithm.Id).AddRange(msg.Path);
        }

        private bool IsHistoryApplied() => isHistoryApplied;

        public void RegisterHanlders(IMessenger messenger)
        {
            var token = Tokens.History.Bind(IsHistoryApplied);
            messenger.RegisterGraph(this, Tokens.Common, SetGraph);
            messenger.Register<IsAppliedMessage>(this, Tokens.History, SetIsApplied);
            messenger.Register<AlgorithmKeyMessage>(this, token, VisualizeHistory);
            messenger.Register<AlgorithmMessage>(this, token, PrepareForPathfinding);
            messenger.Register<PathFoundMessage>(this, token, OnPathFound);
            messenger.Register<StatisticsMessage>(this, token, SetStatistics);
            messenger.Register<ClearHistoryMessage>(this, Tokens.History, ClearHistory);
        }
    }
}