using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Events;
using Pathfinding.AlgorithmLib.Core.Interface.Extensions;
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
using Pathfinding.Visualization.Extensions;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.Units
{
    internal sealed class PathfindingHistoryUnit : Unit, ICanRecieveMessage
    {
        private readonly IService service;
        private readonly IPathfindingRangeBuilder<Vertex> builder;
        private readonly List<(ICoordinate, IReadOnlyList<ICoordinate>)> visitedVertices = new();
        private readonly List<ICoordinate> path = new();

        private AlgorithmCreateDto history = new();
        private GraphReadDto Graph = GraphReadDto.Empty;
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
            var graph = Graph.Graph;
            var key = msg.AlgorithmKey;
            graph.RestoreVerticesVisualState();
            var currentHistory = service.GetGraphPathfindingHistory(Graph.Id)
                .ToDictionary(x => x.Id);
            graph.ApplyCosts(currentHistory[key].Costs);
            var obstacles = currentHistory[key].Obstacles.Select(graph.Get);
            obstacles.ForEach(vertex => vertex.VisualizeAsObstacle());
            var visited = currentHistory[key].Visited;
            var speed = currentHistory[key].Statistics.AlgorithmSpeed;
            currentHistory[key].Range.Select(graph.Get).Reverse().VisualizeAsRange();
            visited.ForEach(vertex =>
            {
                if (speed.HasValue)
                {
                    speed.Value.Wait();
                }
                var current = graph.Get(vertex.Item1);
                current.VisualizeAsVisited();
                foreach(var item in vertex.Item2)
                {
                    var enqueued = graph.Get(item);
                    enqueued.VisualizeAsEnqueued();
                }
            });
            currentHistory[key].Path.Select(graph.Get).VisualizeAsPath();
        }

        private void SetIsApplied(IsAppliedMessage msg)
        {
            isHistoryApplied = msg.IsApplied;
        }

        private void SetGraph(GraphMessage msg)
        {
            Graph = msg.Graph;
        }

        private void OnVertexEnqueued(object sender, VerticesEnqueuedEventArgs e)
        {
            visitedVertices.Add((e.Current, e.Enqueued));
        }

        private void OnStarted(object sender, EventArgs args)
        {
            var graph = Graph.Graph;
            history.Obstacles = graph.GetObstaclesCoordinates().ToReadOnly();
            history.Costs = graph.GetCosts();
            history.Range = builder.Range.GetCoordinates().ToReadOnly();
        }

        private void PrepareForPathfinding(AlgorithmMessage msg)
        {
            algorithm = msg.Algorithm;
            algorithm.Started += OnStarted;
            algorithm.VertexEnqueued += OnVertexEnqueued;
            algorithm.SubPathFound += OnSubPathFound;
        }

        private void OnSubPathFound(object sender, SubPathFoundEventArgs args)
        {
            path.AddRange(args.SubPath);
        }

        private void SetStatistics(StatisticsMessage msg)
        {
            history.Statistics = msg.Statistics;
        }

        private async void OnPathFound(PathFoundMessage msg)
        {
            var foundPath = msg.Path.IsEmpty()
                ? path.ToArray()
                : msg.Path.AsEnumerable();
            history.Path = foundPath.ToArray().ToReadOnly();
            history.Visited = visitedVertices.ToList().AsReadOnly();
            history.GraphId = Graph.Id;
            visitedVertices.Clear();
            path.Clear();
            algorithm = PathfindingProcess.Idle;
            var reference = history;
            history = new();
            await Task.Run(() => service.AddAlgorithm(reference));
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