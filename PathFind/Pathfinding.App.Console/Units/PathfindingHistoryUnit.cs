using GalaSoft.MvvmLight.Messaging;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Events;
using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.App.Console.DataAccess;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Model.Notes;
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

        private IGraph<Vertex> graph = Graph<Vertex>.Empty;
        private bool isHistoryApplied = true;

        public PathfindingHistoryUnit(IReadOnlyCollection<IMenuItem> menuItems,
            IPathfindingRangeBuilder<Vertex> builder,
            GraphsPathfindingHistory history) : base(menuItems)
        {
            this.history = history;
            this.builder = builder;
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

        private void SetGraph(IGraph<Vertex> graph)
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

        private void OnVertexVisited(object sender, PathfindingEventArgs args)
        {
            if (sender is PathfindingProcess process)
            {
                var visited = history.GetFor(graph).Visited;
                visited.TryGetOrAddNew(process.Id).Add(args.Current);
            }
        }

        private void PrepareForPathfinding(PathfindingProcess process)
        {
            var hist = history.GetFor(graph);
            hist.Algorithms.Add(process.Id);
            hist.Obstacles.TryGetOrAddNew(process.Id).AddRange(graph.GetObstaclesCoordinates());
            hist.Costs.TryGetOrAddNew(process.Id).AddRange(graph.GetCosts());
            hist.Ranges.TryGetOrAddNew(process.Id).AddRange(builder.Range.GetCoordinates());
            process.VertexVisited += OnVertexVisited;
        }

        private void SetStatistics((PathfindingProcess Process, Statistics Note) value)
        {
            history.GetFor(graph).Statistics[value.Process.Id] = value.Note;
        }

        private void OnPathFound((PathfindingProcess Process, IGraphPath Path) value)
        {
            history.GetFor(graph).Paths.TryGetOrAddNew(value.Process.Id).AddRange(value.Path);
        }

        private bool IsHistoryApplied() => isHistoryApplied;

        public void RegisterHanlders(IMessenger messenger)
        {
            var token = Tokens.History.Bind(IsHistoryApplied);
            messenger.RegisterGraph(this, Tokens.Common, SetGraph);
            messenger.RegisterData<bool>(this, Tokens.History, SetIsApplied);
            messenger.RegisterData<Guid>(this, token, VisualizeHistory);
            messenger.RegisterData<PathfindingProcess>(this, token, PrepareForPathfinding);
            messenger.RegisterAlgorithmData<IGraphPath>(this, token, OnPathFound);
            messenger.RegisterAlgorithmData<Statistics>(this, token, SetStatistics);
            messenger.Register<ClearHistoryMessage>(this, Tokens.History, ClearHistory);
        }
    }
}