using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Events;
using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.DAL.Models.TransferObjects;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messaging;
using Pathfinding.App.Console.Messaging.Messages;
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
        private readonly IService service;
        private readonly IPathfindingRangeBuilder<Vertex> builder;
        private readonly HashSet<ICoordinate> visitedVertices = new();
        private AlgorithmCreateDto history = new();

        private int graphId;
        private IGraph<Vertex> graph = Graph<Vertex>.Empty;
        private PathfindingProcess algorithm = PathfindingProcess.Idle;

        private bool isHistoryApplied = true;

        public PathfindingHistoryUnit(IReadOnlyCollection<IMenuItem> menuItems,
            IPathfindingRangeBuilder<Vertex> builder,
            IService service) : base(menuItems)
        {
            this.service = service;
            this.builder = builder;
        }

        private void VisualizeHistory(AlgorithmKeyMessage msg)
        {
            var key = msg.AlgorithmKey;
            graph.RestoreVerticesVisualState();
            var currentHistory = service.GetGraphPathfindingHistory(graphId).ToDictionary(x => x.Id);
            graph.ApplyCosts(currentHistory[key].Costs);
            var obstacles = currentHistory[key].Obstacles.Select(graph.Get);
            obstacles.ForEach(vertex => vertex.VisualizeAsObstacle());
            var visited = currentHistory[key].Visited.Select(graph.Get);
            visited.ForEach(vertex => vertex.VisualizeAsVisited());
            currentHistory[key].Range.Select(graph.Get).Reverse().VisualizeAsRange();
            currentHistory[key].Path.Select(graph.Get).VisualizeAsPath();
        }

        private void SetIsApplied(IsAppliedMessage msg)
        {
            isHistoryApplied = msg.IsApplied;
        }

        private void SetGraph(GraphMessage msg)
        {
            graph = msg.Graph;
            graphId = msg.Id;
        }

        //private void ClearHistory(ClearHistoryMessage msg)
        //{
        //    var hist = history.GetHistory(graph.GetHashCode());
        //    hist.Obstacles.Clear();
        //    hist.Paths.Clear();
        //    hist.Ranges.Clear();
        //    hist.Costs.Clear();
        //    hist.Visited.Clear();
        //    hist.Algorithms.Clear();
        //}

        private void OnVertexVisited(object sender, PathfindingEventArgs args)
        {
            visitedVertices.Add(args.Current);
        }

        private void OnStarted(object sender, EventArgs args)
        {
            history.Obstacles = Array.AsReadOnly(graph.GetObstaclesCoordinates().ToArray());
            history.Costs = graph.GetCosts();
            history.Range = Array.AsReadOnly(builder.Range.GetCoordinates().ToArray());
        }

        private void PrepareForPathfinding(AlgorithmMessage msg)
        {
            algorithm = msg.Algorithm;
            algorithm.Started += OnStarted;
            algorithm.VertexVisited += OnVertexVisited;
        }

        private void SetStatistics(StatisticsMessage msg)
        {
            history.Statistics = msg.Statistics;
        }

        private void OnPathFound(PathFoundMessage msg)
        {
            history.Path = Array.AsReadOnly(msg.Path.ToArray());
            history.Visited = visitedVertices.ToList().AsReadOnly();
            history.GraphId = graphId;
            service.AddAlgorithm(history);
            history = new();
            visitedVertices.Clear();
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
            //messenger.Register<PathfindingHistoryUnit, ClearHistoryMessage>(this, Tokens.History, ClearHistory);
        }
    }
}